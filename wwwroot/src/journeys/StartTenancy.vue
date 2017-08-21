<template>
  <section>
    </notification>
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
        <v-stepper-step step="6" :complete="newTenancy.step > 6">Finished</v-stepper-step>
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
      <v-stepper-content ref="content6" step="6">
        <finished></finished>
      </v-stepper-content>
    </v-stepper>

    <div class="row">
      <div class="col-xs-6">
        <v-btn flat @click="back()" v-if="newTenancy.step !== 1 && newTenancy.step !== 6">Go back</v-btn>
      </div>
      <div class="col-xs-6 text-right">
        <v-btn primary @click="next()" v-if="newTenancy.step !== 6">{{ newTenancy.step === 5 ? 'Finished' : 'Continue' }}</v-btn>
      </div>
    </div>

  </section>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'startTenancy',
  data () {
    return {
      isSaving: false,
      viewData: {
        counties: [],
        countries: [],
        rentalFrequencies: [],
        tenancyTypes: []
      }
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions,
      newTenancy: state => state.newTenancy
    })
  },
  methods: {
    next () {
      this.$validation.validate(this.$refs['content' + this.newTenancy.step].$children)
        .then(() => {
          if (this.newTenancy.step === 5) {
            this.isSaving = true
            this.$http.post('/api/journeys/starttenancy', { ...this.newTenancy })
              .then(response => {
                this.$store.commit('TENANCY_NEXT_STEP', this.newTenancy)
              })
              .catch(response => {
                let validationResult = utils.getFormValidationErrors(response)
                validationResult.errors.forEach(validationError => {
                  this.$bus.$emit('SHOW_NOTIFICATION', {
                    message: validationError.messages[0],
                    context: 'error',
                    timeout: 10000
                  })
                })
              })
              .finally(() => {
                this.isSaving = false
              })
          } else {
            this.$store.commit('TENANCY_NEXT_STEP', this.newTenancy)
          }
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
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
      this.$http.get(`/api/journeys/starttenancy`)
        .then(response => {
          Object.assign(this.viewData, utils.mapEntity(response.data, null, true))
        })
    }
  }
}
</script>
