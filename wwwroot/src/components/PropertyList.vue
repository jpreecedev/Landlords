<template>
  <main class="container">
    <h2>Property List</h2>
    <hr>
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
