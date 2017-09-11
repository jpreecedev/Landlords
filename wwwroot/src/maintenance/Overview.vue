<template>
  <div>
    <loader :loading="isLoading"></loader>
    <transition appear name="fade">
      <div v-if="!isLoading">
        <header>
          <h1 class="headline primary--text">Maintenance Requests</h1>
          <p class="display-2 grey--text text--darken-1">Log maintenance and repair requests with your landlord</p>
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
                <v-btn primary
                      class="mt-4 no-left-margin"
                      v-if="permissions.MR_NewEntry"
                      :loading="isAddingMaintenanceEntry"
                      @click.stop="addMaintenanceEntry()">
                  Update Status
                </v-btn>
              </div>
            </div>
          </div>
          <v-btn @click.stop="addMaintenanceRequest()"
                v-if="permissions.MR_New"
                :loading="isSaving"
                class="blue darken-2 action-button"
                absolute dark fab top right>
            <v-icon>add</v-icon>
          </v-btn>
        </v-card>

        <p v-if="!maintenanceRequests || !maintenanceRequests.length">
          You have not created any maintenance requests yet.
          <template v-if="!permissions.MR_New">
            <br/>
            It looks like you don't have the correct access to create a maintenance request.  Please contact your landlord or customer support.
          </template>
        </p>

        <v-btn primary
              class="mt-4 no-left-margin"
              v-if="permissions.MR_New"
              :loading="isSaving"
              @click.stop="addMaintenanceRequest()">Open New Maintenance Request</v-btn>

        <maintenance-dialog title="New maintenance request"
                            subTitle="Log a new maintenance request with your landlord"
                            v-on:closed="closeMaintenanceDialog"
                            :isOpen="isMaintenanceDialogOpen"
                            :severities="maintenanceSeverities">
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
  name: 'maintenance-request-overview',
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
    addMaintenanceRequest () {
      this.isMaintenanceDialogOpen = true
    },
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
            this.selectedMaintenanceRequest.entries.push(response.data)
          })
          .finally(() => {
            this.isAddingMaintenanceEntry = false
          })
      }
    }
  }
}
</script>

<style lang="scss" scoped>

  td a {
    text-decoration: none;
  }

</style>
