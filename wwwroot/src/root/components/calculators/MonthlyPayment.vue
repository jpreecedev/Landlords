<template>
  <main class="container">
    <div>
      <h1>Monthly Payment Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="col-6">
        <div class="card">
          <form role="form" class="card-block" novalidate>
            <fieldset>
              <div class="row">
                <div class="form-group col" :class="{ 'has-danger': errors.has('mortgageAmount') }">
                  <label for="mortgageAmount" class="form-control-label">Total amount borrowed (the mortgage amount)</label>
                  <div class="input-group">
                    <span class="input-group-addon">&pound;</span>
                    <input type="number" class="form-control" id="mortgageAmount" min="0" name="mortgageAmount" v-model="mortgageAmount" v-validate="'required|min_value:1'" data-vv-validate-on="change" placeholder="Amount borrowed">
                    <span class="input-group-addon">.00</span>
                 </div>
                 <span v-show="errors.has('mortgageAmount:required')" v-bind:title="errors.first('mortgageAmount')" class="form-control-feedback">Enter the amount borrowed</span>
                 <span v-show="errors.has('mortgageAmount:min_value')" v-bind:title="errors.first('mortgageAmount')" class="form-control-feedback">Enter at least &pound;10,000</span>
                </div>
              </div>
              <div class="row">
                <div class="form-group col" :class="{ 'has-danger': errors.has('annualInterestRate') }">
                  <label for="annualInterestRate" class="form-control-label">Interest rate % (annual)</label>
                  <div class="input-group">
                    <input type="number" class="form-control" id="annualInterestRate" name="annualInterestRate" min="0.1" step="0.1" v-model="annualInterestRate" v-validate="'required|min_value:0.1'" data-vv-validate-on="change" placeholder="Interest Rate">
                    <span class="input-group-addon">%</span>
                  </div>
                  <span v-show="errors.has('annualInterestRate:required')" v-bind:title="errors.first('annualInterestRate')" class="form-control-feedback">Enter the amount borrowed</span>
                  <span v-show="errors.has('annualInterestRate:min_value')" v-bind:title="errors.first('annualInterestRate')" class="form-control-feedback">Enter at least 0.1%</span>
                </div>
              </div>
              <div class="row">
                <div class="form-group col" :class="{ 'has-danger': errors.has('mortgageLength') }">
                  <label for="mortgageLength" class="form-control-label">Mortgage length</label>
                  <div class="input-group">
                    <input type="number" class="form-control" id="mortgageLength" name="mortgageLength" step="1" min="1" max="50" v-model="mortgageLength" v-validate="'required|min_value:1|max_value:50'" data-vv-validate-on="change" placeholder="Mortgage length">
                    <span class="input-group-addon">years</span>
                  </div>
                  <span v-show="errors.has('mortgageLength:required')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter the mortgage length</span>
                  <span v-show="errors.has('mortgageLength:min_value')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter at least 1 year</span>
                  <span v-show="errors.has('mortgageLength:max_value')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter up to 50 years</span>
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
