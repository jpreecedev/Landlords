<template>
  <section>
    <h1 class="display-1">Start a new tenancy</h1>
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step step="1" :complete="step > 1">Before we start</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2" :complete="step > 2">Property</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3" :complete="step > 3">Tenants</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4" :complete="step > 4">Payment</v-stepper-step>
        <v-divider></v-divider>
      </v-stepper-header>
      <v-stepper-content step="1">
        <announcement />
      </v-stepper-content>
      <v-stepper-content step="2">
        <property :viewdata="viewData" />
      </v-stepper-content>
      <v-stepper-content step="3">
        <tenants :viewdata="viewData" />
      </v-stepper-content>
      <v-stepper-content step="4">
        <payments :viewdata="viewData" />
      </v-stepper-content>
    </v-stepper>

    <v-btn primary @click.native="next()" light>{{ step === 4 ? 'Finished' : 'Continue' }}</v-btn>
    <v-btn flat dark @click.native="back()">Go back</v-btn>
    <v-btn flat dark>Cancel</v-btn>

  </section>
</template>

<script>
  import utils from 'utils'

  export default {
    name: 'startTenancy',
    data () {
      return {
        step: 0,
        permissions: this.$store.state.permissions,
        viewData: {}
      }
    },
    methods: {
      next: function () {
        if (this.step === 4) {
          alert('done')
        } else {
          this.step = Number(this.step) + 1
        }
      },
      back: function () {
        this.step = Number(this.step) - 1
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
