<template>
  <main class="container">
    <div>
      <h1 class="md-display-2">Overview</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <permissions-warning :permission="permissions.CL_Overview" />
    <div class="mt-5">
      <h2 class="md-display-1">Your checklists</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium sint, odio, optio, expedita alias dolorum dicta iusto aliquam eos doloremque fuga iste fugiat quam sed eius corporis suscipit. Voluptatum, assumenda.</p>
     
      <template v-if="!overview.checklists || !overview.checklists.length">
        <p>You have not created any checklists yet.</p>
      </template>

      <div class="card-deck">
        <div class="card col-3 pl-0 pr-0" v-for="checklist in overview.checklists">
          <img class="card-img-top" src="../assets/images/checklist.png" v-bind:alt="checklist.name">
          <div class="card-block">
            <h4 class="card-title">{{ checklist.name }}</h4>
            <p v-if="checklist.description" class="card-text">{{ checklist.description }}</p>
            <md-button v-if="permissions.CL_GetById" @click.native="$router.push({name: 'editor', params: {checklistId: checklist.id}})" class="pointer md-raised md-default">View</md-button>
            <md-button v-if="permissions.CL_Archive && !checklist.isArchived" @click.native="archive(checklist)" class="pointer md-raised md-default">Archive</md-button>
          </div>
          <div class="card-footer text-muted">
            <span v-if="!checklist.propertyReference && !checklist.propertyStreetAddress">General</span>
            <span v-if="checklist.propertyReference">{{ checklist.propertyReference }}</span>
          </div>
        </div>

      </div>
      <div class="mt-3" v-if="permissions.CL_Archived && !hasArchivedLists">
        <md-button @click.native="getArchived()" class="pointer md-raised md-default">View Archived</md-button>
      </div>
    </div>
    <div class="row mt-5" v-if="permissions.CL_Create">
      <div class="col-6" v-if="overview.availableChecklists && overview.availableChecklists.length">
        <h2 class="md-display-1">Available checklist templates</h2>   
        <md-input-container>       
          <md-select v-model="selectedChecklist">
            <md-option v-for="checklist in overview.availableChecklists" :key="checklist.name" v-bind:value="checklist.name" v-bind:disabled="checklist.isPropertyMandatory && portfolioProperties.length < 1">{{ checklist.origin }}: {{ checklist.name }}</md-option>
          </md-select>
        </md-input-container>
      </div>
    </div>
    <div class="row mt-5" v-if="permissions.CL_Create && portfolioProperties && portfolioProperties.length">
      <div class="col-6">
        <h3 class="md-headline">Select a property from your portfolio</h3>
        <md-input-container>       
          <md-select v-model="selectedProperty" v-bind:disabled="!selectedChecklist">
            <md-option v-for="property in portfolioProperties" :key="property.id" v-bind:value="property.id">{{ property.propertyReference }}<span v-if="property.propertyStreetAddress"> ({{property.propertyStreetAddress}})</span></md-option>
          </md-select>
        </md-input-container>
      </div>
    </div>
    <div class="row mt-3" v-if="permissions.CL_Create">
      <div class="col">
        <md-button @click.native="createChecklistInstance(selectedChecklist, selectedProperty)" class="md-raised md-primary pointer">Create checklist</md-button>
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
      .then(() => {
        var index = 0
        if (!this.portfolioProperties || !this.portfolioProperties.length) {
          var first = this.overview.availableChecklists.filter(c => !c.isPropertyMandatory)[0]
          index = this.overview.availableChecklists.indexOf(first)
        }
        this.selectedChecklist = this.overview.availableChecklists[index].name
      })
    },
    methods: {
      createChecklistInstance: function (selectedChecklist, selectedProperty) {
        this.$http.post(`/api/checklists/?checklistId=${selectedChecklist.id}${selectedProperty ? `&portfolioId=${selectedProperty.portfolioId}&propertyDetailsId=` + selectedProperty.id : ''}`).then(response => {
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
      }
    },
    watch: {
      selectedChecklist: function (value) {
        if (!value || !value.isPropertyMandatory) {
          this.selectedProperty = null
        } else {
          this.selectedProperty = this.portfolioProperties[0].id
        }
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
