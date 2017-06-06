<template>
  <main>
    <div>
      <h1 class="md-display-2">Monthly Payment Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="col-xs-12 col-md-6">
        <form role="form" novalidate>
          <fieldset>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container>
                  <md-icon>&pound;</md-icon>
                  <label for="mortgageAmount">Total amount borrowed</label>
                  <md-input type="number" id="mortgageAmount" min="0" name="mortgageAmount" v-model="mortgageAmount" />
                </md-input-container>
                <span v-show="errors.has('mortgageAmount:required')" v-bind:title="errors.first('mortgageAmount')" class="form-control-feedback">Enter the amount borrowed</span>
                <span v-show="errors.has('mortgageAmount:min_value')" v-bind:title="errors.first('mortgageAmount')" class="form-control-feedback">Enter at least &pound;10,000</span>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12 col-md-6">
                <md-input-container>
                  <md-icon>%</md-icon>
                  <label for="annualInterestRate">Interest rate % (annual)</label>
                  <md-input type="number" id="annualInterestRate" name="annualInterestRate" min="0.1" step="0.1" v-model="annualInterestRate" />
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12 col-md-6">              
                <md-input-container>
                  <md-icon>alarm</md-icon>
                  <label for="mortgageLength">Mortgage length</label>
                  <md-input type="number" id="mortgageLength" name="mortgageLength" step="1" min="1" max="50" v-model="mortgageLength" />
                  <md-icon>yrs</md-icon>
                </md-input-container>
              </div>
            </div>
            <div class="row mt-4" v-if="!calculateMonthlyPayment">
              <div class="col">
                <p class="text-danger">Please fix any validation errors highlighted in red to see your results</p>
              </div>
            </div>
            <div class="row mt-4" v-if="calculateMonthlyPayment">
              <div class="col">
                <p><strong>Total monthly payment:</strong> &pound;{{ calculateMonthlyPayment.toFloat().formatWithSeparator() }}</p>
              </div>
            </div>
          </fieldset>
        </form>
      </div>
    </div>
  </main>
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
