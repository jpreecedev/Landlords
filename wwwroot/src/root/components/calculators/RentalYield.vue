<template>
  <div>
    <header>
      <h1 class="headline primary--text">Rental yield calculator</h1>
      <p class="display-2 grey--text text--darken-1">See how much gross (before tax) income your property might generate</p>
      <p class="subheading">Complete this form to see your personalised results.</p>
    </header>
    <v-card>
      <v-card-text>
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <form role="form" novalidate>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                v-model="rentalyield.purchasePrice"
                                :rules="[$validation.rules.required, $validation.rules.min_value(rentalyield.purchasePrice, 10000)]"
                                min="10000"
                                prefix="£"
                                label="Total cost of property"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                    <v-text-field type="number"
                                  min="1"
                                  step="1"
                                  label="How much is the rent?"
                                  prefix="£"
                                  v-model="rentalyield.rentalValue"
                                  :rules="[$validation.rules.required, $validation.rules.min_value(rentalyield.rentalValue, 1)]"
                                  required>
                    </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <p class="mb-0">Rental income period</p>
                  <div class="row">
                    <div class="col-xs-6">
                      <v-radio value="Monthly"
                               color="primary"
                               v-model="rentalyield.frequency"
                               label="Monthly"
                               checked>
                      </v-radio>
                    </div>
                    <div class="col-xs-6">
                      <v-radio value="Annual"
                               color="primary"
                               v-model="rentalyield.frequency"
                               label="Annual">
                      </v-radio>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row" v-if="!calculateRentalYield">
                <div class="col-xs-12">
                  <p class="red--text">Please fix any validation errors to see your results</p>
                </div>
              </div>
              <div class="row" v-if="calculateRentalYield">
                <div class="col-xs-12">
                  <p class="headline">Results</p>
                  <p><strong>Gross rental yield:</strong> {{ calculateRentalYield }}%.<br>
                    <span v-if="calculateRentalYield > 9.9">This is an EXCELLENT gross yield.</span>
                    <span v-if="calculateRentalYield > 6.9 && calculateRentalYield <= 9.9">This is a GOOD gross yield.</span>
                    <span v-if="calculateRentalYield >= 5 && calculateRentalYield <= 6.9">This is an 'OK' gross yield.</span>
                    <span v-if="calculateRentalYield < 5">The gross yield is LOW.</span>
                  </p>
                </div>
              </div>
            </form>
          </div>
        </div>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
export default {
  name: 'rentalyield',
  data () {
    return {
      rentalyield: {
        purchasePrice: 120000,
        rentalValue: 550,
        frequency: 'Monthly'
      }
    }
  },
  computed: {
    calculateRentalYield () {
      if (!this.rentalyield.rentalValue || !this.rentalyield.purchasePrice) {
        return false
      }

      let multiplier = this.rentalyield.frequency === 'Monthly' ? 12 : 1
      return Number.parseFloat(((this.rentalyield.rentalValue * multiplier) / this.rentalyield.purchasePrice) * 100).toFixed(2)
    }
  }
}
</script>
