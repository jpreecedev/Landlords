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
      <p v-if="!overview.userChecklists || !overview.userChecklists.length">You have not created any checklists yet.</p>
      <div class="col" v-for="checklist in overview.userChecklists">
        {{ checklist.name }}
      </div>
    </div>
    <div class="row mt-5">
    <div class="col" v-if="overview.agencyChecklists && overview.agencyChecklists.length">
      <h2>Agency checklists</h2>
      <select class="form-control">
        <option v-for="checklist in overview.agencyChecklists" v-bind:value="checklist.id">{{ checklist.name }}</option>
      </select>
    </div>
      <div class="col-6" v-if="overview.adminChecklists && overview.adminChecklists.length">
        <h2>Admin checklists</h2>
        <select class="form-control">
          <option v-for="checklist in overview.adminChecklists" v-bind:value="checklist.id">{{ checklist.name }}</option>
        </select>
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
        overview: {
          userChecklists: [],
          agencyChecklists: [],
          adminChecklists: []
        }
      }
    },
    created () {
      this.$http.get(`/api/checklists/`).then(response => {
        this.overview = response.data
      })
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
