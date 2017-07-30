<template>
  <div>
    <header>
      <h1 class="headline primary--text">Checklists</h1>
      <p class="display-2 grey--text text--darken-1">Using checklists will help streamline your business and reduce mistakes</p>
      <p class="subheading">Create a new checklist using the form below.</p>
    </header>

    <loader :loading="isLoading"></loader>

    <p v-if="!isLoading && overview.checklists.length === 0">You have not created any checklists yet.</p>

    <div class="row">
      <div class="col-xs-12 col-md-4 mt-3" v-for="(checklist, index) in overview.checklists" :key="index">
        <v-card>
          <div class="row">
            <div class="col-xs-4">
              <img src="../assets/images/checklist.png" class="card__row__image" :alt="checklist.name">
            </div>
            <div class="col-xs-8">
              <div>
                <v-card-text>
                  <strong>{{ checklist.name }}</strong><br>
                  <small class="description">
                    <span v-if="!checklist.propertyReference && !checklist.propertyStreetAddress">General</span>
                    <span v-if="checklist.propertyReference">{{ checklist.propertyReference }}</span>
                    <span v-if="checklist.propertyStreetAddress"> ({{ checklist.propertyStreetAddress }})</span>
                  </small>
                </v-card-text>
              </div>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn primary flat v-if="permissions.CL_GetById" @click="$router.push({name: 'editor', params: {checklistId: checklist.id}})">View</v-btn>
                <v-btn flat v-if="permissions.CL_Archive && !checklist.isArchived" @click="archive(checklist)">Archive</v-btn>
              </v-card-actions>
            </div>
          </div>
        </v-card>
      </div>
    </div>

    <div class="mt-3" v-if="permissions.CL_Archived && !hasArchivedLists">
      <v-btn primary @click="getArchived()" :loading="isLoadingArchived" class="no-left-margin">Show Archived</v-btn>
    </div>

    <div class="row mt-5" v-if="permissions.CL_Create && overview.availableChecklists && overview.availableChecklists.length">
      <div class="col-xs-6">
        <v-card>
          <v-card-title class="primary title">
          Create a checklist
          </v-card-title>
          <v-card-text>
            <div class="row">
              <div class="col-xs-12">
                <select-list :items="overview.availableChecklists"
                             :disabled="portfolioProperties.length < 1"
                             v-model="selectedChecklist"
                             @input="selectedChecklistChanged($event)"
                             label="Select a checklist template"
                             :rules="[$validation.rules.required]">
                </select-list>
              </div>
            </div>
            <div class="row" v-if="selectedChecklist && isPropertyMandatory && portfolioProperties && portfolioProperties.length">
              <div class="col-xs-12">
                <select-list :items="portfolioProperties"
                             v-model="selectedProperty"
                             item-value="id"
                             label="Select a property from your portfolio">
                </select-list>
              </div>
            </div>
          </v-card-text>
          <v-card-actions v-if="permissions.CL_Create">
            <v-spacer></v-spacer>
            <v-btn primary flat @click="createChecklistInstance(selectedChecklist, selectedProperty)">Create checklist</v-btn>
          </v-card-actions>
        </v-card>
      </div>
    </div>

  </div>
</template>

<script>
  import PermissionsWarning from 'common/PermissionsWarning'

  export default {
    name: 'overview',
    components: { PermissionsWarning },
    data () {
      return {
        isLoading: false,
        isLoadingArchived: false,
        permissions: this.$store.state.permissions,
        selectedChecklist: null,
        selectedProperty: null,
        portfolioProperties: [],
        hasArchivedLists: false,
        isPropertyMandatory: false,
        overview: {
          checklists: [],
          availableChecklists: []
        }
      }
    },
    created () {
      this.isLoading = true
      this.$http.get(`/api/checklists/overview`)
        .then(response => {
          this.overview = response.data
        })
        .then(() => {
          if (this.permissions.PD_GetBasic) {
            return this.$http.get(`/api/propertydetails/basicdetails`).then(response => {
              if (response.data) {
                this.portfolioProperties = response.data
                this.selectedProperty = this.portfolioProperties[0].id
              }
            })
          }
        })
        .finally(() => {
          this.isLoading = false
        })
    },
    methods: {
      createChecklistInstance (selectedChecklist, selectedProperty) {
        this.$http.post(`/api/checklists/?checklistId=${selectedChecklist.id}${selectedProperty ? '&propertyDetailsId=' + selectedProperty : ''}`).then(response => {
          this.$router.push({ name: 'editor', params: { checklistId: response.data.id } })
        })
      },
      archive (selectedChecklist) {
        this.$http.post(`/api/checklists/archive/?checklistId=${selectedChecklist.id}`).then(response => {
          let index = this.overview.checklists.indexOf(selectedChecklist)
          if (index > -1) {
            this.overview.checklists.splice(index, 1)
          }
        })
      },
      getArchived () {
        this.isLoadingArchived = true
        this.$http.get(`/api/checklists/archived`)
          .then(response => {
            if (response.data) {
              this.overview.checklists.push(...response.data)
              this.hasArchivedLists = true
            }
          })
          .finally(() => {
            this.isLoadingArchived = false
          })
      },
      selectedChecklistChanged (selectedChecklist) {
        let checklist = this.overview.availableChecklists.find(item => item.id === selectedChecklist.id)
        if (checklist) {
          this.isPropertyMandatory = checklist.isPropertyMandatory
        }
        if (!this.isPropertyMandatory) {
          this.selectedProperty = null
        }
      }
    }
  }
</script>

<style lang="scss" scoped>

  .card__row {
    margin-top: initial;
    &__image {
      padding: 1rem;
      max-width: 100%;
      height: auto;
    }
  }

  .description {
    display: block;
    min-height: 33px;
  }

</style>
