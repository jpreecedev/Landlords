<template>
  <div>
    <h1 class="display-2">Overview</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <permissions-warning :permission="permissions.CL_Overview"></permissions-warning>
    <div class="mt-5">
      <h2 class="display-1">Your checklists</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium sint, odio, optio, expedita alias dolorum dicta iusto aliquam eos doloremque fuga iste fugiat quam sed eius corporis suscipit. Voluptatum, assumenda.</p>
      <p v-if="!overview.checklists || !overview.checklists.length">You have not created any checklists yet.</p>

      <div class="row">
        <div class="col-xs-12 col-md-4 mt-3" v-for="checklist in overview.checklists" :key="checklist">
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
                  <v-btn primary flat v-if="permissions.CL_GetById" @click.native="$router.push({name: 'editor', params: {checklistId: checklist.id}})">View</v-btn>
                  <v-btn flat v-if="permissions.CL_Archive && !checklist.isArchived" @click.native="archive(checklist)">Archive</v-btn>
                </v-card-actions>
              </div>
            </div>
          </v-card>
        </div>
      </div>
    </div>

    <div class="mt-3" v-if="permissions.CL_Archived && !hasArchivedLists">
      <v-btn primary @click.native="getArchived()">View Archived</v-btn>
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
                <v-select :items="overview.availableChecklists"
                          :disabled="portfolioProperties.length < 1"
                          v-model="selectedChecklist"
                          item-value="id"
                          @input="selectedChecklistChanged($event)"
                          label="Select a checklist template"
                          required>
                </v-select>
              </div>
            </div>
            <div class="row" v-if="selectedChecklist && isPropertyMandatory && portfolioProperties && portfolioProperties.length">
              <div class="col-xs-12">
                <v-select :items="portfolioProperties"
                          v-model="selectedProperty"
                          item-value="id"
                          label="Select a property from your portfolio">
                </v-select>
              </div>
            </div>
          </v-card-text>
          <v-card-actions v-if="permissions.CL_Create">
            <v-spacer></v-spacer>
            <v-btn primary flat @click.native="createChecklistInstance(selectedChecklist, selectedProperty)">Create checklist</v-btn>
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
      this.$http.get(`/api/checklists/overview`).then(response => {
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
    },
    methods: {
      createChecklistInstance: function (selectedChecklist, selectedProperty) {
        this.$http.post(`/api/checklists/?checklistId=${selectedChecklist}&propertyDetailsId=${selectedProperty}`).then(response => {
          this.$router.push({ name: 'editor', params: { checklistId: response.data.id } })
        })
      },
      archive: function (selectedChecklist) {
        this.$http.post(`/api/checklists/archive/?checklistId=${selectedChecklist.id}`).then(response => {
          var index = this.overview.checklists.indexOf(selectedChecklist)
          if (index > -1) {
            this.overview.checklists.splice(index, 1)
          }
        })
      },
      getArchived: function () {
        this.$http.get(`/api/checklists/archived`).then(response => {
          if (response.data) {
            this.overview.checklists.push(...response.data)
            this.hasArchivedLists = true
          }
        })
      },
      selectedChecklistChanged: function (checklistId) {
        var checklist = this.overview.availableChecklists.find(item => item.id === checklistId)
        if (checklist) {
          this.isPropertyMandatory = checklist.isPropertyMandatory
        }
        if (!this.isPropertyMandatory) {
          this.selectedProperty = null
        }
      },
      portfolioPropertySelected: function (value) {
        this.selectedProperty = value
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
