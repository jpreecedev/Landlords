<template>
  <main class="container">
    <div> 
      <h1 class="md-display-2">Landlord List</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <table class="table table-striped table-hover">
      <thead class="thead-inverse">
        <tr>
          <th>Landlord Name</th>
          <th>Phone Number</th>
          <th># of Properties</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="entry in data">
          <td>
            {{ entry.landlordName }}
          </td>
          <td>
            {{ entry.mainPhoneNumber }} <span v-if="entry.secondaryPhoneNumber">or {{ entry.secondaryPhoneNumber }}</span>
          </td>
          <td>
            {{ entry.properties }}
          </td>
        </tr>
      </tbody>
    </table>
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
