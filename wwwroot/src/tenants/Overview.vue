<template>
  <main>
    <h1 class="md-display-2">Tenants</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <md-table-card>
      <md-table>
        <md-table-header class="thead-inverse">
          <md-table-row>
            <md-table-head>Name</md-table-head>
            <md-table-head>Contact Number</md-table-head>
            <md-table-head>Email Address</md-table-head>
          </md-table-row>
        </md-table-header>
        <md-table-body>
          <md-table-row v-for="tenant in data" :key="tenant">
            <md-table-cell>
              <router-link v-if="permissions.TE_GetById" :to="'/tenants/details/' + tenant.id">
                <span>{{ tenant.firstName + ' ' + tenant.lastName }}</span>
              </router-link>
              <span v-else>{{ tenant.firstName + ' ' + tenant.lastName }}</span>
            </md-table-cell>
            <md-table-cell>
              {{ tenant.mainContactNumber }} <span v-if="tenant.secondaryContactNumber">or {{ tenant.secondaryContactNumber }}</span>
            </md-table-cell>
            <md-table-cell>
              {{ tenant.emailAddress }}
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>

    <md-button v-if="permissions.TE_New" type="button" class="md-raised md-primary pointer mt-4" @click.native="addTenant()">Add a tenant</md-button>
  </main>
</template>

<script>
export default {
  name: 'tenants-overview',
  data () {
    return {
      permissions: this.$store.state.permissions,
      data: []
    }
  },
  created () {
    this.$http.get('/api/tenants').then(response => {
      this.data = response.data
    })
  },
  methods: {
    addTenant: function () {
      this.$http.post('/api/tenants/new').then(response => {
        this.$router.push({ name: 'tenants-details', params: { tenantId: response.data.id } })
      })
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
