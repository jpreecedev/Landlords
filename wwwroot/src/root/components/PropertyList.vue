<template>
  <main>
    <div> 
      <h1 class="md-display-2">Property List</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <md-table-card>
      <md-table>
        <md-table-header>
          <md-table-row>
            <md-table-head>&nbsp;</md-table-head>
            <md-table-head>Reference</md-table-head>
            <md-table-head>Street Address</md-table-head>
          </md-table-row>
        </md-table-header>
        <md-table-body>
          <md-table-row v-for="entry in data" :key="entry">
            <md-table-cell>
              <img class="img-thumbnail" v-if="entry.leadImage" v-bind:src="'/static/uploads/' + entry.portfolioId + '/' + entry.leadImage.fileName" v-bind:alt="entry.leadImage.fileName">
            </md-table-cell>
            <md-table-cell>
              <router-link v-if="permissions.PD_View" :to="'/manager/property-details/' + entry.id">{{ entry.reference }}</router-link>
              <span v-else>{{ entry.reference }}</span>
            </md-table-cell>
            <md-table-cell>
              {{ entry.propertyStreetAddress }}
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>
    
    <md-button v-if="permissions.PD_New" type="button" class="md-raised md-primary mt-4" @click.native="addProperty()">Add a property</md-button>            

  </main>
</template>

<script>
export default {
  name: 'propertylist',
  data () {
    return {
      permissions: this.$store.state.permissions,
      data: []
    }
  },
  created () {
    this.$http.get('/api/propertydetails').then(response => {
      this.data = response.data
    })
  },
  methods: {
    addProperty: function () {
      this.$http.post('/api/propertydetails/new').then(response => {
        this.$router.push(`/manager/property-details/${response.data.id}`)
      })
    }
  }
}
</script>
  
<style lang="scss" scoped>

thead > tr > th:first-of-type {
  width: 175px;
}
  
</style>
