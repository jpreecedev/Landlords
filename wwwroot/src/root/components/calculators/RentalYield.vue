<template>
  <div>
    <h1 class="display-2">Rental Yield Calculator</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
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
                           v-model="rentalyield.frequency"
                           label="Monthly"
                           checked>
                  </v-radio>
                </div>
                <div class="col-xs-6">
                  <v-radio value="Annual"
                           v-model="rentalyield.frequency"
                          label="Annual">
                  </v-radio>
                </div>
              </div>
            </div>
          </div>
          <div class="row mt-4" v-if="!calculateRentalYield">
            <div class="col-xs-12">
              <p class="red--text">Please fix any validation errors to see your results</p>
            </div>
          </div>
          <div class="row mt-4" v-if="calculateRentalYield">
            <div class="col-xs-12">
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
    calculateRentalYield: function () {
      if (!this.rentalyield.rentalValue || !this.rentalyield.purchasePrice || this.errorBag.errors.length !== 0) {
        return false
      }

      var multiplier = this.rentalyield.frequency === 'Monthly' ? 12 : 1
      return Number.parseFloat(((this.rentalyield.rentalValue * multiplier) / this.rentalyield.purchasePrice) * 100).toFixed(2)
    }
  }
}
</script>
