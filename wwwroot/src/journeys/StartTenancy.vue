<template>
  <section>
    <h1 class="md-display-1">Start a new tenancy</h1>
    <v-stepper v-model="e1">
      <v-stepper-header>
        <v-stepper-step step="1" :complete="e1 > 1">Before we start</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2" :complete="e1 > 2">Property</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3" :complete="e1 > 3">Tenants</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4" :complete="e1 > 4">Payment</v-stepper-step>
        <v-divider></v-divider>
      </v-stepper-header>
      <v-stepper-content step="1">
        <announcement />
        <v-btn primary @click.native="e1 = 2" light>Continue</v-btn>
        <v-btn flat dark>Cancel</v-btn>
      </v-stepper-content>
      <v-stepper-content step="2">
        <starttenancy-home :viewdata="viewData" />
        <v-btn primary @click.native="e1 = 3" light>Continue</v-btn>
        <v-btn flat dark>Cancel</v-btn>
      </v-stepper-content>
      <v-stepper-content step="3">
        <tenants :viewdata="viewData" />
        <v-btn primary @click.native="e1 = 4" light>Continue</v-btn>
        <v-btn flat dark>Cancel</v-btn>
      </v-stepper-content>
      <v-stepper-content step="4">
        <payments :viewdata="viewData" />
        <v-btn flat dark>Cancel</v-btn>
      </v-stepper-content>
    </v-stepper>
  </section>
</template>

<script>
  import utils from 'utils'

  export default {
    name: 'startTenancy',
    data () {
      return {
        e1: 0,
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
