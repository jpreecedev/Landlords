<template>
  <div>
    <h1 class="display-2">Monthly Payment Calculator</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <form role="form" novalidate>
          <div class="row">
            <div class="col-xs-12">
              <v-text-field v-model="mortgageAmount"
                            :rules="[$validation.rules.required, $validation.rules.min_value(mortgageAmount, 10000)]"
                            label="Total amount borrowed"
                            type="number"
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
                            required>
              </v-text-field>
            </div>
          </div>
          <div class="row mt-4" v-if="!calculateMonthlyPayment">
            <div class="col-xs-12">
              <p class="md-warn">Please fix any validation errors to see your results</p>
            </div>
          </div>
          <div class="row mt-4" v-if="calculateMonthlyPayment">
            <div class="col-xs-12">
              <p><strong>Total monthly payment:</strong> &pound;{{ calculateMonthlyPayment.toFloat().formatWithSeparator() }}</p>
            </div>
          </div>
        </form>
      </div>
    </div>
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
    calculateMonthlyPayment: function () {
      return utils.calculateMonthlyPayment(this.annualInterestRate, this.mortgageAmount, this.mortgageLength)
    }
  }
}

</script>
