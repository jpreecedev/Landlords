<template>
  <div>
    <header>
      <h1 class="headline primary--text">Return on investment (ROI) calculator</h1>
      <p class="display-2 grey--text text--darken-1">Work out the potential Return on investment (ROI) so you can compare with other investment opportunities</p>
      <p class="subheading">Complete this form to see your personalised results.</p>
    </header>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <div class="row">
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary title">
              Purchase financials
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.reference"
                              :rules="[$validation.rules.required]"
                              label="Reference"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.address"
                              :rules="[$validation.rules.required]"
                              label="First line of address"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.pricePaid"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.pricePaid, 10000)]"
                              label="Purchase price"
                              step="1"
                              min="10000"
                              type="number"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.deposit"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.deposit, 1000)]"
                              label="Deposit"
                              step="1"
                              min="1000"
                              type="number"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.fees"
                              :rules="[$validation.rules.required]"
                              label="Refurbishment, fees, furnishings and other costs"
                              step="1"
                              type="number"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              Rental
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.lettableUnits"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.lettableUnits, 1), $validation.rules.max_value(shortlistedProperty.lettableUnits, 1000)]"
                              label="Lettable units"
                              type="number"
                              step="1"
                              min="0"
                              max="1000"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.expectedRentalIncome"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.expectedRentalIncome, 0)]"
                              label="Rental income per unit"
                              type="number"
                              step="100"
                              min="0"
                              max="1000000"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              On-going costs
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.mortgageInterestRate"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.mortgageInterestRate, 0.1), $validation.rules.max_value(shortlistedProperty.mortgageInterestRate, 20)]"
                              label="Mortgage interest rate"
                              step="0.1"
                              min="0.1"
                              max="20"
                              type="number"
                              suffix="%"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.managementCost"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.managementCost, 0), $validation.rules.max_value(shortlistedProperty.managementCost, 100)]"
                              label="Management cost (Monthly)"
                              step="1"
                              min="0"
                              max="100"
                              type="number"
                              suffix="%"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.repairsContingency"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.repairsContingency, 0), $validation.rules.max_value(shortlistedProperty.repairsContingency, 100)]"
                              label="Repairs contingency"
                              step="1"
                              min="0"
                              max="100"
                              type="number"
                              suffix="%"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.serviceCharge"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.serviceCharge, 0)]"
                              label="Service charge and ground rent (Annual)"
                              type="number"
                              step="100"
                              min="0"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="shortlistedProperty.insurance"
                              :rules="[$validation.rules.required, $validation.rules.min_value(shortlistedProperty.insurance, 0)]"
                              label="Insurance (Annual)"
                              type="number"
                              step="100"
                              min="0"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </div>
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="title primary">
              Your results
            </v-card-title>
            <v-card-text>
              <div v-if="!score">
                <p class="red--text">Please fix any validation errors to see your results</p>
              </div>
              <div v-if="score">
                <p>
                  Based on the information provided above, use the following calculations to decide if the return on investment meets or exceeds your expections.
                </p>
                <p>
                  In our opinion, this is <strong>{{scoreInWords}}</strong> investment.
                </p>
                <p>
                  Monthly profit: <strong>&pound;{{score.monthlyProfit.formatWithSeparator()}}</strong>
                </p>
                <p>
                  Annual profit: <strong>&pound;{{score.annualProfit.formatWithSeparator()}}</strong>
                </p>
                <p>
                  Net yield: <strong>{{(score.netYield * 100).toFixed(2)}}%</strong>
                </p>
                <p>
                  Return on investment: <strong>{{(score.roi * 100).toFixed(2)}}%</strong>
                </p>
              </div>
            </v-card-text>
          </v-card>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-xs-12">
          <v-btn v-if="permissions.SP_Promote"
                 @click="promoteShortlistedProperty()"
                 :loading="isPromoting"
                 class="no-left-margin"
                 type="button"
                 color="primary">
            Promote
          </v-btn>
          <v-btn v-if="permissions.SP_Update"
                 :loading="isSaving"
                 type="submit"
                 color="primary">
            {{ this.shortlistedProperty.shortlistedPropertyId === null ? 'Add to shortlist' : 'Save changes' }}
          </v-btn>
          <v-btn color="error" flat v-if="permissions.SP_Delete && this.shortlistedProperty.shortlistedPropertyId" type="button" :disabled="isDeleting" @click="deleteShortlistedProperty()">Delete</v-btn>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'roi',
  data () {
    return {
      isSaving: false,
      isDeleting: false,
      isPromoting: false,
      shortlistedProperty: {
        shortlistedPropertyId: null,
        reference: '3 bedroom house',
        address: '1 new property lane',
        pricePaid: 60000,
        deposit: 15000,
        fees: 15000,
        lettableUnits: 1,
        expectedRentalIncome: 500,
        mortgageInterestRate: 5,
        managementCost: 0,
        repairsContingency: 10,
        serviceCharge: 0,
        insurance: 300
      }
    }
  },
  mounted () {
    if (this.permissions.SP_GetById && this.$route.params.shortlistedPropertyId) {
      this.$http.get(`/api/shortlistedproperties/${this.$route.params.shortlistedPropertyId}`)
        .then(response => {
          if (response.data) {
            this.shortlistedProperty = response.data
            this.$validation.commit(this.$children)
          }
        })
    }
  },
  methods: {
    validateBeforeSubmit () {
      this.$validation.validate(this.$children)
        .then(() => {
          this.isSaving = true
          this.$http.post(`/api/shortlistedproperties`, { ...this.shortlistedProperty })
            .then(() => {
              this.$router.push({ name: 'watchlist' })
            })
            .catch(response => {
              let validationResult = utils.getFormValidationErrors(response)
              validationResult.errors.forEach(validationError => {
                console.log('ERROR', validationError.key, validationError.messages[0], 'required')
              })
            })
            .finally(() => {
              this.isSaving = false
            })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
        })
    },
    deleteShortlistedProperty () {
      this.isDeleting = true
      this.$http.delete(`/api/shortlistedproperties?shortlistedPropertyId=${this.shortlistedProperty.shortlistedPropertyId}`)
        .then(() => {
          this.$router.push({ name: 'watchlist' })
        })
        .finally(() => {
          this.isDeleting = false
        })
    },
    promoteShortlistedProperty () {
      this.isPromoting = true
      this.$http.post(`/api/shortlistedproperties/promote`, { ...this.shortlistedProperty })
        .then(response => {
          debugger
          this.$router.push({ name: 'propertyDetails', params: { propertyId: response.data } })
        })
        .finally(() => {
          this.isPromoting = false
        })
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    }),
    score () {
      if (!this.shortlistedProperty) {
        return false
      }

      return utils.getReturnOnInvestment(this.shortlistedProperty)
    },
    scoreInWords () {
      let roi = this.score.roi * 100
      if (roi < 3) {
        return 'a poor'
      } else if (roi >= 3 && roi < 10) {
        return 'a good'
      } else if (roi > 10) {
        return 'an excellent'
      }
    }
  }
}
</script>
