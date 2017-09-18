<template>
  <div>
    <loader :loading="isLoading"></loader>
    <transition appear name="fade">
      <div v-if="!isLoading">
        <header>
          <h1 class="headline primary--text">Maintenance Requests</h1>
          <p class="display-2 grey--text text--darken-1">Help with your tenants maintenance requests</p>
          <p v-if="maintenanceRequests && maintenanceRequests.length"
            class="subheading">
            Click on a maintenance request for more details.
          </p>
        </header>
        <v-card v-if="maintenanceRequests && maintenanceRequests.length">
          <div class="container">
            <div class="row">
                <div class="col-xs-12 col-md-6">
                  <select-list v-model="selectedMaintenanceRequest"
                              :items="maintenanceRequests"
                              :return-object="true"
                              itemValue="id"
                              label="Select a maintenance request">
                  </select-list>
                </div>
              <div v-if="selectedMaintenanceRequest"
                  class="col-xs-12">
                <timeline :events="selectedMaintenanceRequest.entries"></timeline>
              </div>
            </div>
          </div>
        </v-card>

        <p v-if="!maintenanceRequests || !maintenanceRequests.length">
          There are no outstanding maintenance requests.
          <template v-if="!permissions.MR_New">
            <br/>
            It looks like you don't have the correct access to create a maintenance request.  Please contact your landlord or customer support.
          </template>
        </p>

        <maintenance-dialog title="New maintenance request"
                            subTitle="Log a new maintenance request with your landlord"
                            v-on:closed="closeMaintenanceDialog"
                            :isOpen="isMaintenanceDialogOpen"
                            :severities="maintenanceSeverities"
                            :properties="properties">
        </maintenance-dialog>
        <add-event-dialog v-on:closed="closeAddEventDialog"
                          :isOpen="isAddEventDialogOpen"
                          :statuses="maintenanceStatuses">
        </add-event-dialog>
      </div>
    </transition>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'management-overview',
  data () {
    return {
      isLoading: false,
      isSaving: false,
      isAddingMaintenanceEntry: false,
      isMaintenanceDialogOpen: false,
      isAddEventDialogOpen: false,
      maintenanceSeverities: [],
      maintenanceRequests: [],
      maintenanceStatuses: [],
      properties: [],
      selectedMaintenanceRequest: null
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  mounted () {
    this.isLoading = true

    this.$http.get('/api/maintenancerequests/viewdata')
      .then(viewdata => {
        Object.assign(this, utils.mapEntity(viewdata.data, null, true))
        this.properties = viewdata.data.properties
      })
      .finally(() => {
        this.$http.get('/api/maintenancerequests')
          .then(response => {
            this.maintenanceRequests = response.data
            if (this.maintenanceRequests && this.maintenanceRequests.length > 0) {
              this.selectedMaintenanceRequest = this.maintenanceRequests[0]
            }
          })
          .finally(() => {
            this.isLoading = false
          })
      })
  },
  methods: {
    addMaintenanceEntry () {
      this.isAddEventDialogOpen = true
    },
    closeMaintenanceDialog (newMaintenaceRequest) {
      this.isMaintenanceDialogOpen = false

      if (newMaintenaceRequest) {
        this.isSaving = true
        this.$http.post('/api/maintenancerequests/', newMaintenaceRequest)
          .then(response => {
            this.maintenanceRequests.push(response.data)
          })
          .finally(() => {
            this.isSaving = false
            this.selectedMaintenanceRequest = this.maintenanceRequests[this.maintenanceRequests.length - 1]
          })
      }
    },
    closeAddEventDialog (newEvent) {
      this.isAddEventDialogOpen = false
      this.isAddingMaintenanceEntry = true

      if (newEvent) {
        newEvent.maintenanceRequestId = this.selectedMaintenanceRequest.id

        this.$http.post('/api/maintenancerequests/newentry', newEvent)
          .then(response => {
            if (!this.selectedMaintenanceRequest.entries) {
              this.selectedMaintenanceRequest.entries = []
            }
            console.log(response.data)
            this.selectedMaintenanceRequest.entries.splice(0, 0, response.data)
          })
          .finally(() => {
            this.isAddingMaintenanceEntry = false
          })
      }
    }
  }
}
</script>
