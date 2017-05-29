<template>
  <main class="container">
    <div>
      <h1>New tenant</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div>
      <p class="text-muted">There are {{ outstandingActions }} outstanding actions.</p>
      <form>
        <fieldset>
          <accordion :checklist="checklist" v-on:collapseAll="collapseAll()" v-on:toggleCompleted="toggleCompleted" v-on:toggleExpanded="toggleExpanded" v-on:move="move" />
        </fieldset>
      </form>
    </div>
    <div class="row mt-5">
      <div class="col-3 offset-9">
        <div class="alert alert-danger text-center">
          <p>Danger Zone!! Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
          <button class="btn btn-danger pointer" @click="deleteChecklist()">Delete Checklist</button>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
  import accordion from './components/Accordion'

  export default {
    name: 'newTenantMoveIn',
    components: { accordion },
    data () {
      return {
        permissions: this.$store.state.permissions,
        checklistId: this.$route.params.checklistId,
        checklist: null
      }
    },
    created () {
      this.$http.get(`/api/checklists/${this.checklistId}`).then(response => {
        this.checklist = response.data
      })
    },
    computed: {
      outstandingActions: function () {
        if (!this.checklist) {
          return 0
        }

        var count = 0
        this.checklist.checklistItems.forEach(item => {
          if (!item.isCompleted) {
            count += 1
          }
        })
        return count
      }
    },
    methods: {
      deleteChecklist: function () {
        this.$http.delete(`/api/checklists/${this.checklistId}`).then(response => {
          this.$router.push({ name: 'overview' })
        })
      },
      collapseAll: function () {
        this.checklist.checklistItems.forEach(item => {
          item.isExpanded = false
        })
      },
      toggleCompleted: function (item) {
        this.$http.post(`/api/checklistitem/completed?checklistId=${this.checklistId}&checklistItemId=${item.id}&completed=${item.isCompleted}`)
      },
      toggleExpanded: function (item) {
        if (this.permissions.CI_ToggleExpanded) {
          this.$http.post(`/api/checklistitem/expanded?checklistId=${this.checklistId}&checklistItemId=${item.id}&expanded=${item.isExpanded}`)
        }
      },
      move: function (data) {
        this.$http.post(`/api/checklistitem/move?checklistId=${this.checklistId}&checklistItemId=${data.item.id}&direction=${data.direction}`).then(() => {
          var index = this.checklist.checklistItems.indexOf(data.item)
          var poppedItem = this.checklist.checklistItems[index]
          this.checklist.checklistItems.splice(index, 1)

          switch (data.direction) {
            case 'up':
              this.checklist.checklistItems.splice(index - 1, 0, poppedItem)
              break
            case 'down':
              this.checklist.checklistItems.splice(index + 1, 0, poppedItem)
              break
          }

          for (var i = 0; i < this.checklist.checklistItems.length; i++) {
            this.checklist.checklistItems[i].order = i + 1
          }
        })
      }
    }
  }
</script>
