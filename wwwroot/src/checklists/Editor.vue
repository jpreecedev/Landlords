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
          <accordion :checklist="checklist" />
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
      }
    }
  }
</script>
