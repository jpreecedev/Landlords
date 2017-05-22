<template>
  <main class="container">
    <div>
      <h1>Overview</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div v-if="!permissions.CL_Overview" class="alert alert-danger">
      Sorry this resource is currently unavailable
    </div>
    <div class="mt-5">
      <h2>Your checklists</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium sint, odio, optio, expedita alias dolorum dicta iusto aliquam eos doloremque fuga iste fugiat quam sed eius corporis suscipit. Voluptatum, assumenda.</p>
      <p v-if="!overview.checklists || !overview.checklists.length">You have not created any checklists yet.</p>

      <div class="card-deck">
        <div class="card col-3 pl-0 pr-0" v-for="checklist in overview.checklists">
          <img class="card-img-top" src="../assets/images/checklist.png" v-bind:alt="checklist.name">
          <div class="card-block">
            <h4 class="card-title">{{ checklist.name }}</h4>
            <p v-if="checklist.description" class="card-text">{{ checklist.description }}</p>
            <router-link :to="{name: 'editor', params: {checklistId: checklist.id}}" class="pointer btn btn-secondary">View or Edit</router-link>
          </div>
        </div>
      </div>
      
    </div>
    <div class="row mt-5">
      <div class="col-6" v-if="overview.availableChecklists && overview.availableChecklists.length">
        <h2>Available checklists</h2>
        <select v-model="selectedChecklist" class="form-control">
          <option v-for="checklist in overview.availableChecklists" v-bind:value="checklist">{{ checklist.origin }}: {{ checklist.name }}</option>
        </select>
      </div>
    </div>
    <div class="row mt-5" v-if="selectedChecklist && selectedChecklist.isPropertyMandatory">
      <div class="col-6">
        <h3>Select a property from your portfolio</h3>
        <select v-model="selectedProperty" class="form-control">
          <option v-for="property in portfolioProperties" v-bind:value="property">{{ property.propertyReference }}<span v-if="property.propertyStreetAddress"> ({{property.propertyStreetAddress}})</span></option>
        </select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <button v-if="permissions.CL_Create" @click="createChecklistInstance(selectedChecklist)" class="btn btn-primary pointer">Create checklist</button>
      </div>
    </div>
  </main>
</template>

<script>
  export default {
    name: 'overview',
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
      this.$http.get(`/api/checklists/`).then(response => {
        this.overview = response.data
        this.selectedChecklist = this.overview.availableChecklists[0]
      })
      this.$http.get(`/api/propertydetails/basicdetails`).then(response => {
        if (response.data) {
          this.portfolioProperties = response.data
          this.selectedProperty = this.portfolioProperties[0]
        }
      })
    },
    methods: {
      createChecklistInstance: function (selectedChecklist) {
        this.$http.post(`/api/checklists/?checklistId=${selectedChecklist.id}`).then(response => {
          this.$router.push({ name: 'editor', params: { checklistId: response.data.id } })
        })
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
