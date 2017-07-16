<template>
  <div>
    <header>
      <h1 class="headline primary--text">Monthly payment calculator</h1>
      <p class="display-2 grey--text text--darken-1">How much will your mortgage cost you each month?</p>
      <p class="subheading">Complete this form to see your personalised results.</p>
    </header>
    <v-card>
      <v-card-text>
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <form role="form" novalidate>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="mortgageAmount"
                                :rules="[$validation.rules.required, $validation.rules.min_value(mortgageAmount, 10000)]"
                                label="Total amount borrowed"
                                type="number"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12 col-md-6">
                  <v-text-field v-model="annualInterestRate"
                                :rules="[$validation.rules.required, $validation.rules.min_value(annualInterestRate, 0.1)]"
                                label="Interest rate % (Annual)"
                                type="number"
                                min="0.1"
                                step="0.1"
                                suffix="%"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12 col-md-6">
                  <v-text-field v-model="mortgageLength"
                                :rules="[$validation.rules.required, $validation.rules.min_value(mortgageLength, 1), $validation.rules.max_value(mortgageLength, 50)]"
                                label="Mortgage length"
                                type="number"
                                min="1"
                                step="1"
                                max="50"
                                suffix="years"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row" v-if="!calculateMonthlyPayment">
                <div class="col-xs-12">
                  <p class="red--text">Please fix any validation errors to see your results</p>
                </div>
              </div>
              <div class="row" v-if="calculateMonthlyPayment">
                <div class="col-xs-12">
                  <p class="headline">Results</p>
                  <p><strong>Total monthly payment:</strong> &pound;{{ calculateMonthlyPayment.toFloat().formatWithSeparator() }}</p>
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
import utils from 'utils'

export default {
  name: 'monthlyPayment',
  data () {
    return {
      mortgageAmount: 100000,
      annualInterestRate: 5,
      mortgageLength: 25
    }
  },
  computed: {
    calculateMonthlyPayment () {
      return utils.calculateMonthlyPayment(this.annualInterestRate, this.mortgageAmount, this.mortgageLength)
    }
  }
}
</script>
