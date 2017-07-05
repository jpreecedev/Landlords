<template>
  <div>
    <h1 class="display-2">New tenant</h1>
    <h2 class="display-1" v-if="checklist">
      <span v-if="checklist.propertyReference">{{checklist.propertyReference}}</span>
      <span v-if="checklist.propertyStreetAddress">({{ checklist.propertyStreetAddress }})</span>
    </h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div>
      <p class="text-muted">There are {{ outstandingActions }} outstanding actions.</p>
      <accordion :checklist="checklist" v-on:toggleCompleted="toggleCompleted" v-on:move="move">
      </accordion>
    </div>
    <div class="row mt-5" v-if="permissions.CL_DeleteById">
      <div class="col-xs-12 col-md-3 col-md-offset-9 text-center">
        <p>Danger Zone!! Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
        <v-btn error @click.native="deleteChecklist()">Delete Checklist</v-btn>
      </div>
    </div>
  </div>
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
    toggleCompleted: function (item) {
      this.$http.post(`/api/checklistitem/completed?checklistId=${this.checklistId}&checklistItemId=${item.id}&completed=${item.isCompleted}`)
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
