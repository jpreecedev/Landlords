<template>
  <main>
    <h1 class="md-display-2">Landlord List</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

    <md-table-card>
      <md-table>
        <md-table-header>
          <md-table-row>
            <md-table-head>Landlord Name</md-table-head>
            <md-table-head>Phone Number</md-table-head>
            <md-table-head># of Properties</md-table-head>
          </md-table-row>
        </md-table-header>
        <md-table-body>
          <md-table-row v-for="entry in data" :key="entry">
            <md-table-cell>
              {{ entry.landlordName }}
            </md-table-cell>
            <md-table-cell>
              {{ entry.mainPhoneNumber }} <span v-if="entry.secondaryPhoneNumber">or {{ entry.secondaryPhoneNumber }}</span>
            </md-table-cell>
            <md-table-cell>
              {{ entry.properties }}
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>
  </main>
</template>

<script>
export default {
  name: 'landlordlist',
  data () {
    return {
      data: []
    }
  },
  created () {
    this.$http.get('/api/landlord').then(response => {
      this.data = response.data
    })
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
