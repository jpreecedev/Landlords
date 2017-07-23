<template>
  <section>
    <h1 class="display-1">Start a new tenancy</h1>
    <v-stepper v-if="newTenancy && newTenancy.step" v-model="newTenancy.step">
      <v-stepper-header>
        <v-divider></v-divider>
        <v-stepper-step step="1" :complete="newTenancy.step > 1">Before we start</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2" :complete="newTenancy.step > 2">Property</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3" :complete="newTenancy.step > 3">Tenants</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4" :complete="newTenancy.step > 4">Referencing</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="5" :complete="newTenancy.step > 5">Payment</v-stepper-step>
        <v-divider></v-divider>
      </v-stepper-header>
      <v-stepper-content ref="content1" step="1">
        <announcement></announcement>
      </v-stepper-content>
      <v-stepper-content ref="content2" step="2">
        <property :tenancyTypes="viewData.tenancyTypes"></property>
      </v-stepper-content>
      <v-stepper-content ref="content3" step="3">
        <tenants :titles="viewData.titles" :counties="viewData.counties" :countries="viewData.countries"></tenants>
      </v-stepper-content>
      <v-stepper-content ref="content4" step="4">
        <referencing :contactTypes="viewData.tenantContactTypes" :employmentTypes="viewData.employmentTypes"></referencing>
      </v-stepper-content>
      <v-stepper-content ref="content5" step="5">
        <payments :rentalFrequencies="viewData.rentalFrequencies"></payments>
      </v-stepper-content>
    </v-stepper>

    <div class="row">
      <div class="col-xs-6">
        <v-btn flat @click.native="back()" v-if="newTenancy.step !== 1">Go back</v-btn>
      </div>
      <div class="col-xs-6 text-right">
        <v-btn primary @click.native="next()">{{ newTenancy.step === 5 ? 'Finished' : 'Continue' }}</v-btn>
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
          counties: [],
          countries: [],
          rentalFrequencies: [],
          tenancyTypes: []
        }
      }
    },
    methods: {
      next () {
        this.$validation.validate(this.$refs['content' + this.newTenancy.step].$children)
          .then(() => {
            if (this.newTenancy.step === 5) {
              this.$http.post('/api/journeys/starttenancy', { ...this.newTenancy })
                .then(response => {
                  alert('done')
                })
                .catch(response => {
                  let validationResult = utils.getFormValidationErrors(response)
                  validationResult.errors.forEach(validationError => {
                    console.log('ERROR', validationError.key, validationError.messages[0], 'required')
                  })
                  debugger
                })
            } else {
              this.$store.commit('TENANCY_NEXT_STEP', this.newTenancy)
            }
          })
      },
      back () {
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
    }
  }
</script>
