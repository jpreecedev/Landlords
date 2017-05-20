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
      <p v-if="!overview.checklists || !overview.checklists.length">You have not created any checklists yet.</p>
      <div class="col" v-for="checklist in overview.checklists">
        {{ checklist.name }}
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-6" v-if="overview.availableChecklists && overview.availableChecklists.length">
        <h2>Available checklists</h2>
        <select v-model="selectedChecklistId" class="form-control">
          <option v-for="checklist in overview.availableChecklists" v-bind:value="checklist.id">{{ checklist.origin }}: {{ checklist.name }}</option>
        </select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <button v-if="permissions.CL_Create" @click="createChecklistInstance(selectedChecklistId)" class="btn btn-primary pointer">Create checklist</button>
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
        selectedChecklistId: null,
        overview: {
          checklists: [],
          availableChecklists: []
        }
      }
    },
    created () {
      this.$http.get(`/api/checklists/`).then(response => {
        this.overview = response.data
        this.selectedChecklistId = this.overview.availableChecklists[0].id
      })
    },
    methods: {
      createChecklistInstance: function (selectedChecklistId) {
        this.$http.post(`/api/checklists/?checklistId=${selectedChecklistId}`).then(response => {
          if (response.data) {
            this.overview.checklists.push(response.data)
          }
        })
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
