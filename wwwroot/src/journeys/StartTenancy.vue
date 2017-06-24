<template>
  <section>
    <h1 class="display-1">Start a new tenancy</h1>
    <v-stepper v-model="currentStep">
      <v-stepper-header>
        <v-stepper-step step="1" :complete="newTenancy.step > 1">Before we start</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2" :complete="newTenancy.step > 2">Property</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3" :complete="newTenancy.step > 3">Tenants</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4" :complete="newTenancy.step > 4">Payment</v-stepper-step>
        <v-divider></v-divider>
      </v-stepper-header>
      <v-stepper-content step="1">
        <announcement></announcement>
      </v-stepper-content>
      <v-stepper-content step="2">
        <property :tenancyTypes="viewData.tenancyTypes"></property>
      </v-stepper-content>
      <v-stepper-content step="3">
        <tenants :countries="viewData.countries"></tenants>
      </v-stepper-content>
      <v-stepper-content step="4">
        <payments :rentalFrequencies="viewData.rentalFrequencies"></payments>
      </v-stepper-content>
    </v-stepper>

    <div class="row">
      <div class="col-xs-6">
        <v-btn flat dark @click.native="back()">Go back</v-btn>
        <v-btn flat dark>Cancel</v-btn>
      </div>
      <div class="col-xs-6 text-right">
        <v-btn primary @click.native="next()" light>{{ newTenancy.step === 4 ? 'Finished' : 'Continue' }}</v-btn>
      </div>
    </div>

  </section>
</template>

<script>
  import utils from 'utils'

  export default {
    name: 'startTenancy',
    data () {
      return {
        permissions: this.$store.state.permissions,
        newTenancy: this.$store.state.newTenancy,
        viewData: {
          countries: [],
          rentalFrequencies: [],
          tenancyTypes: []
        }
      }
    },
    methods: {
      next: function () {
        if (this.newTenancy.step === 4) {
          alert('done')
        } else {
          this.$store.commit('TENANCY_NEXT_STEP', this.newTenancy)
        }
      },
      back: function () {
        if (this.newTenancy.step !== 1) {
          this.$store.commit('TENANCY_PREVIOUS_STEP', this.newTenancy)
        }
      }
    },
    created () {
      if (this.permissions.J_StartTenancy) {
        this.$http.get(`/api/journeys/starttenancy`).then(response => {
          Object.assign(this.viewData, utils.mapEntity(response.data, null, true))
        })
      }
    },
    computed: {
      currentStep: {
        get () {
          return this.newTenancy.step
        }
      }
    }
  }
</script>

<style lang="scss" scoped>

  .md-tab {
    padding: 32px 16px;
  }

</style>
