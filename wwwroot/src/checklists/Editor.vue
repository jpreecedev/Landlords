<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="checklist">
      <header>
        <h1 class="headline primary--text">{{checklist.name}}</h1>
        <p class="display-2 grey--text text--darken-1">
          <span v-if="checklist.propertyReference">{{checklist.propertyReference}}</span>
          <span v-if="checklist.propertyStreetAddress">({{ checklist.propertyStreetAddress }})</span>
        </p>
      </header>

      <p class="text-muted">There are {{ outstandingActions }} outstanding actions.</p>
      <accordion :checklist="checklist"
                 v-on:toggleCompleted="toggleCompleted"
                 v-on:deleteChecklistItem="deleteChecklistItem"
                 v-on:editChecklistItem="editChecklistItem">
      </accordion>

      <div class="row mt-5" v-if="permissions.CL_DeleteById">
        <div class="col-xs-12 col-md-4 col-md-offset-8 text-center">
          <h5 class="red--text">Danger Zone!!</h5>
          <p class="red--text">You can delete this checklist.<br/>Deleting is permanent and cannot be undone!</p>
          <v-btn error @click="deleteChecklist()" :loading="isDeleting">Delete Checklist</v-btn>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import accordion from './components/Accordion'

export default {
  name: 'newTenantMoveIn',
  components: { accordion },
  data () {
    return {
      isLoading: false,
      isDeleting: false,
      checklistId: this.$route.params.checklistId,
      checklist: null
    }
  },
  created () {
    this.isLoading = true
    this.$http.get(`/api/checklists/${this.checklistId}`)
      .then(response => {
        this.checklist = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    }),
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
      this.isDeleting = true
      this.$http.delete(`/api/checklists/${this.checklistId}`)
        .then(response => {
          this.isDeleting = false
          this.$router.push({ name: 'overview' })
        })
        .catch(() => {
          this.isDeleting = false
        })
    },
    deleteChecklistItem (checklistItem) {
      this.$http.delete(`/api/checklistitem?checklistId=${this.checklist.id}&checklistItemId=${checklistItem.id}`)
        .then(() => {
          this.checklist.checklistItems.splice(this.checklist.checklistItems.indexOf(checklistItem), 1)
        })
        .catch(() => {
          alert('Unable to delete checklist item at this time')
        })
    },
    editChecklistItem (checklistItem) {
      this.$http.post(`/api/checklistItem/update?checklistId=${this.checklistId}&checklistItemId=${checklistItem.id}`, JSON.stringify(checklistItem.displayText))
    },
    toggleCompleted (checklistItem) {
      this.$http.post(`/api/checklistitem/completed?checklistId=${this.checklistId}&checklistItemId=${checklistItem.id}&completed=${checklistItem.isCompleted}`)
    }
  }
}
</script>
