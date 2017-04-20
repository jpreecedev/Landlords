<template>
  <main class="container">
    <div> 
      <h1>Property List</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <grid :data="data" :columns="columns" v-on:rowclick="rowClicked" />
  </main>
</template>

<script>
import Grid from '@/components/common/Grid'

export default {
  name: 'propertylist',
  components: { Grid },
  data () {
    return {
      columns: [
        { key: 'id' },
        { key: 'reference', display: 'Reference' },
        { key: 'propertyStreetAddress', display: 'Street Address' }
      ],
      data: []
    }
  },
  created () {
    this.$http.get('/api/propertydetails').then(response => {
      this.data = response.data
    })
  },
  methods: {
    rowClicked: function (entry) {
      this.$router.push({ name: 'propertyDetails', params: { propertyId: entry.id } })
    }
  }
}
</script>
  
<style scoped>
  
</style>
