<template>
  <div>
    <h1 class="display-2">Monthly Payment Calculator</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <form role="form" novalidate>
          <div class="row">
            <div class="col-xs-12">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('mortgageAmount') }">
                <label for="mortgageAmount">Total amount borrowed</label>
                <md-input type="number" id="mortgageAmount" min="0" name="mortgageAmount" v-model="mortgageAmount" data-vv-name="mortgageAmount" v-validate="'required|min_value:10000'" data-vv-validate-on="change" required />
                <span v-if="errorBag.has('mortgageAmount:required')" class="md-error">Enter the amount borrowed</span>
                <span v-else-if="errorBag.has('mortgageAmount:min_value')" class="md-error">Enter at least &pound;10,000</span>
              </md-input-container>
            </div>
          </div>
          <div class="row">
            <div class="col-xs-12 col-md-6">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('annualInterestRate') }">
                <label for="annualInterestRate">Interest rate % (annual)</label>
                <md-input type="number" id="annualInterestRate" name="annualInterestRate" min="0.1" step="0.1" v-model="annualInterestRate" data-vv-name="annualInterestRate" v-validate="'required|min_value:0.1'" data-vv-validate-on="change" required />
                <span v-if="errorBag.has('annualInterestRate:required')" class="md-error">Enter the amount borrowed</span>
                <span v-else-if="errorBag.has('annualInterestRate:min_value')" class="md-error">Enter at least 0.1%</span>
              </md-input-container>
            </div>
          </div>
          <div class="row">
            <div class="col-xs-12 col-md-6">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('mortgageLength') }">
                <label for="mortgageLength">Mortgage length</label>
                <md-input type="number" id="mortgageLength" name="mortgageLength" step="1" min="1" max="50" v-model="mortgageLength" data-vv-name="mortgageLength" v-validate="'required|min_value:1|max_value:50'" data-vv-validate-on="change" required />
                <span v-if="errorBag.has('mortgageLength:required')" class="md-error">Enter the mortgage length</span>
                <span v-else-if="errorBag.has('mortgageLength:min_value')" class="md-error">Enter at least 1 year</span>
                <span v-else-if="errorBag.has('mortgageLength:max_value')" class="md-error">Enter up to 50 years</span>
              </md-input-container>
            </div>
          </div>
          <div class="row mt-4" v-if="!calculateMonthlyPayment">
            <div class="col">
              <p class="md-warn">Please fix any validation errors to see your results</p>
            </div>
          </div>
          <div class="row mt-4" v-if="calculateMonthlyPayment">
            <div class="col">
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
