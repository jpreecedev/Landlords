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
                <div class="form-group col">
                  <label for="mortgageAmount">Total amount borrowed (the mortgage amount)</label>
                  <div class="input-group">
                    <span class="input-group-addon">£</span>
                    <input type="number" class="form-control" id="mortgageAmount" name="mortgageAmount" v-model="mortgageAmount" placeholder="Amount borrowed">
                    <span class="input-group-addon">.00</span>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="form-group col">
                  <label for="annualInterestRate">Interest rate % (annual)</label>
                  <div class="input-group">
                    <input type="number" class="form-control" id="annualInterestRate" name="annualInterestRate" step="0" v-model="annualInterestRate" placeholder="Interest Rate">
                    <span class="input-group-addon">%</span>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="form-group col">
                  <label for="mortgageLength">Mortgage length</label>
                  <div class="input-group">
                    <input type="number" class="form-control" id="mortgageLength" name="mortgageLength" step="0" v-model="mortgageLength" placeholder="Mortgage length">
                    <span class="input-group-addon">years</span>
                  </div>
                </div>
              </div>
              <div class="row mt-4" v-if="calculateMonthlyPayment">
                <div class="col">
                  <p><strong>Total monthly payment:</strong> £{{ calculateMonthlyPayment }}.
                  </p>
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

import utils from '@/utils'

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

<style lang="scss" scoped>

</style>
