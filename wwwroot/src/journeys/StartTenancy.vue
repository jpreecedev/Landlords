<template>
  <section>
    <h1 class="md-display-1">Start a new tenancy</h1>
    <md-tabs class="md-transparent" :md-fixed="true" :md-centered="true">

      <announcement />
      <starttenancy-home :viewdata="viewData" />
      <tenants :viewdata="viewData" />
      <payments :viewdata="viewData" />

    </md-tabs>
  </section>
</template>

<script>
  import utils from 'utils'

  export default {
    name: 'startTenancy',
    data () {
      return {
        permissions: this.$store.state.permissions,
        viewData: {}
      }
    },
    created () {
      if (this.permissions.J_StartTenancy) {
        this.$http.get(`/api/journeys/starttenancy`).then(response => {
          Object.assign(this.viewData, utils.mapEntity(response.data, null, true))
        })
      }
    }
  }
</script>

<style lang="scss" scoped>

  .md-tab {
    padding: 32px 16px;
  }

</style>
