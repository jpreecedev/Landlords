<template>
  <main class="container">
    <div> 
      <h1>Property List</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <table class="table table-striped table-hover">
      <thead class="thead-inverse">
        <tr>
          <th>&nbsp;</th>
          <th>Reference</th>
          <th>Street Address</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="entry in data">
          <td>
            <img class="img-thumbnail" v-if="entry.leadImage" v-bind:src="'/static/uploads/' + entry.userId + '/' + entry.leadImage.fileName" v-bind:alt="entry.leadImage.fileName">
          </td>
          <td>
            <router-link :to="'/manager/property-details/' + entry.id">{{ entry.reference }}</router-link>
          </td>
          <td>
            {{ entry.propertyStreetAddress }}
          </td>
        </tr>
      </tbody>
    </table>
  </main>
</template>

<script>
export default {
  name: 'propertylist',
  data () {
    return {
      data: []
    }
  },
  created () {
    this.$http.get('/api/propertydetails').then(response => {
      this.data = response.data
    })
  }
}
</script>
  
<style lang="scss" scoped>

thead > tr > th:first-of-type {
  width: 175px;
}
  
</style>
