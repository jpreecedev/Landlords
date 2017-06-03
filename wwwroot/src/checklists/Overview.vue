<template>
  <main class="container">
    <div>
      <h1>Overview</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <permissions-warning :permission="permissions.CL_Overview" />
    <div class="mt-5">
      <h2>Your checklists</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium sint, odio, optio, expedita alias dolorum dicta iusto aliquam eos doloremque fuga iste fugiat quam sed eius corporis suscipit. Voluptatum, assumenda.</p>
     
      <template v-if="!overview.checklists || !overview.checklists.length">
        <p>You have not created any checklists yet.</p>
        <a v-if="permissions.CL_Archived" href="#" @click="getArchived()" class="pointer btn btn-secondary">View Archived</a>        
      </template>

      <div class="card-deck">
        <div class="card col-3 pl-0 pr-0" v-for="checklist in overview.checklists">
          <img class="card-img-top" src="../assets/images/checklist.png" v-bind:alt="checklist.name">
          <div class="card-block">
            <h4 class="card-title">{{ checklist.name }}</h4>
            <p v-if="checklist.description" class="card-text">{{ checklist.description }}</p>
            <router-link v-if="permissions.CL_GetById" :to="{name: 'editor', params: {checklistId: checklist.id}}" class="pointer btn btn-secondary">View</router-link>
            <a v-if="permissions.CL_Archive && !checklist.isArchived" href="#" @click="archive(checklist)" class="pointer btn btn-secondary">Archive</a>
          </div>
          <div class="card-footer text-muted">
            <span v-if="!checklist.propertyReference && !checklist.propertyStreetAddress">General</span>
            <span v-if="checklist.propertyReference">{{ checklist.propertyReference }}</span>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5" v-if="permissions.CL_Create">
      <div class="col-6" v-if="overview.availableChecklists && overview.availableChecklists.length">
        <h2>Available checklist templates</h2>          
        <select v-model="selectedChecklist" class="form-control">
          <option v-for="checklist in overview.availableChecklists" v-bind:value="checklist" v-bind:disabled="checklist.isPropertyMandatory && portfolioProperties.length < 1">{{ checklist.origin }}: {{ checklist.name }}</option>
        </select>  
      </div>
    </div>
    <div class="row mt-5" v-if="permissions.CL_Create && portfolioProperties && portfolioProperties.length">
      <div class="col-6">
        <h3>Select a property from your portfolio</h3>
        <select v-model="selectedProperty" v-bind:disabled="!selectedChecklist || !selectedChecklist.isPropertyMandatory" class="form-control">
          <option v-for="property in portfolioProperties" v-bind:value="property">{{ property.propertyReference }}<span v-if="property.propertyStreetAddress"> ({{property.propertyStreetAddress}})</span></option>
        </select>
      </div>
    </div>
    <div class="row mt-3" v-if="permissions.CL_Create">
      <div class="col">
        <button @click="createChecklistInstance(selectedChecklist, selectedProperty)" class="btn btn-primary pointer">Create checklist</button>
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
              this.selectedProperty = this.portfolioProperties[0]
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
        this.selectedChecklist = this.overview.availableChecklists[index]
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
          }
        })
      }
    },
    watch: {
      selectedChecklist: function (value) {
        if (!value || !value.isPropertyMandatory) {
          this.selectedProperty = null
        } else {
          this.selectedProperty = this.portfolioProperties[0]
        }
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
