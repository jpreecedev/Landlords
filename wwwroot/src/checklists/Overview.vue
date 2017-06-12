<template>
  <main>
    <h1 class="md-display-2">Overview</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <permissions-warning :permission="permissions.CL_Overview" />
    <div class="mt-5">
      <h2 class="md-display-1">Your checklists</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium sint, odio, optio, expedita alias dolorum dicta iusto aliquam eos doloremque fuga iste fugiat quam sed eius corporis suscipit. Voluptatum, assumenda.</p>
      <p v-if="!overview.checklists || !overview.checklists.length">You have not created any checklists yet.</p>
      
      <md-layout>
        <md-card v-for="checklist in overview.checklists" :key="checklist">
          <md-card-header>
            <div class="md-title">{{ checklist.name }}</div>
            <div class="md-subhead">
              <span v-if="!checklist.propertyReference && !checklist.propertyStreetAddress">General</span>
              <span v-if="checklist.propertyReference">{{ checklist.propertyReference }}</span>
              <span v-if="checklist.propertyStreetAddress"> ({{ checklist.propertyStreetAddress }})</span>
            </div>
          </md-card-header>
          <md-card-media>
            <img class="card-img-top" src="../assets/images/checklist.png" v-bind:alt="checklist.name">
          </md-card-media>
          <md-card-actions>
            <md-button v-if="permissions.CL_GetById" @click.native="$router.push({name: 'editor', params: {checklistId: checklist.id}})" class="md-primary">View</md-button>
            <md-button v-if="permissions.CL_Archive && !checklist.isArchived" @click.native="archive(checklist)" class="md-default">Archive</md-button>
          </md-card-actions>
        </md-card>
      </md-layout>
      </div>

      <div class="mt-3" v-if="permissions.CL_Archived && !hasArchivedLists">
        <md-button @click.native="getArchived()" class="pointer md-raised md-primary">View Archived</md-button>
      </div>
    </div>
    <div class="row mt-5" v-if="permissions.CL_Create && overview.availableChecklists && overview.availableChecklists.length">
      <div class="col-xs-6">
        <md-card>
          <md-card-header>
            <div class="md-title">Create a checklist</div>
            <div class="md-subhead">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quam quibusdam incidunt, dolor laudantium delectus mollitia error perspiciatis dicta vero expedita aliquam adipisci tempora blanditiis quo, possimus, animi quisquam, in quae.</div>
          </md-card-header>
          <md-card-content>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container>       
                  <label for="availableChecklists">Select a checklist template</label>
                  <md-select v-model="selectedChecklist" name="availableChecklists" @change="selectedChecklistChanged($event)">
                    <md-option v-for="checklist in overview.availableChecklists" :key="checklist.name" v-bind:value="checklist.id" v-bind:disabled="checklist.isPropertyMandatory && portfolioProperties.length < 1">{{ checklist.origin }}: {{ checklist.name }}</md-option>
                  </md-select>
                </md-input-container>
              </div>
            </div>
            <div class="row" v-if="selectedChecklist && isPropertyMandatory && portfolioProperties && portfolioProperties.length">
              <div class="col-xs-12">
                <md-input-container>       
                  <label for="portfolio">Select a property from your portfolio (this can be optional)</label>
                  <md-select v-model="selectedProperty" v-bind:disabled="!selectedChecklist" name="portfolio">
                    <md-option v-for="property in portfolioProperties" :key="property.id" v-bind:value="property.id">{{ property.propertyReference }}<span v-if="property.propertyStreetAddress"> ({{property.propertyStreetAddress}})</span></md-option>
                  </md-select>
                </md-input-container>
              </div>
            </div>
          </md-card-content>
          <md-card-actions>
            <md-button @click.native="createChecklistInstance(selectedChecklist, selectedProperty)" class="md-primary">Create checklist</md-button>
          </md-card-actions>
        </md-card>
      </div>
    </div>
    <div class="row mt-3" v-if="permissions.CL_Create">
      <div class="col">
      </div>
    </div>
  </main>
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
        debugger
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
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
  .md-card {
    margin: 0 4px 16px 4px;

    .md-card-media {
      img {
        width: auto;
        max-height: 180px;
      }
    }
  }

</style>
