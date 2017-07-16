<template>
  <div v-if="checklist">
    <header>
      <h1 class="headline primary--text">{{checklist.name}}</h1>
      <p class="display-2 grey--text text--darken-1">
        <span v-if="checklist.propertyReference">{{checklist.propertyReference}}</span>
        <span v-if="checklist.propertyStreetAddress">({{ checklist.propertyStreetAddress }})</span>
      </p>
    </header>
    <div>
      <p class="text-muted">There are {{ outstandingActions }} outstanding actions.</p>
      <accordion :checklist="checklist" v-on:toggleCompleted="toggleCompleted">
      </accordion>
    </div>
    <div class="row mt-5" v-if="permissions.CL_DeleteById">
      <div class="col-xs-12 col-md-4 col-md-offset-8 text-center">
        <p class="red--text">Danger Zone!! You can delete this checklist.<br/>Deleting is permanent and cannot be undone!</p>
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
    outstandingActions () {
      if (!this.checklist) {
        return 0
      }

      let count = 0
      this.checklist.checklistItems.forEach(item => {
        if (!item.isCompleted) {
          count += 1
        }
      })
      return count
    }
  },
  methods: {
    deleteChecklist () {
      this.$http.delete(`/api/checklists/${this.checklistId}`).then(response => {
        this.$router.push({ name: 'overview' })
      })
    },
    toggleCompleted (item) {
      this.$http.post(`/api/checklistitem/completed?checklistId=${this.checklistId}&checklistItemId=${item.id}&completed=${item.isCompleted}`)
    }
  }
}
</script>
