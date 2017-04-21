<template>
  <main class="container">
    <div>
      <h1>Monthly Payment Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="card col-6">
        <form role="form" class="card-block" novalidate>
          <fieldset>
            <div class="row">
              <div class="form-group col">
                <label for="mortgageAmount">Total amount borrowed (the mortgage amount)</label>
                <div class="input-group currency">
                  <span class="input-group-addon">£</span>
                  <input type="number" class="form-control" id="mortgageAmount" name="mortgageAmount" v-model="mortgageAmount" placeholder="Amount borrowed">
                  <span class="input-group-addon">.00</span>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="form-group col">
                <label for="annualInterestRate">Interest rate % (annual)</label>
                <div class="input-group currency">
                  <input type="number" class="form-control" id="annualInterestRate" name="annualInterestRate" step="0" v-model="annualInterestRate" placeholder="Interest Rate">
                  <span class="input-group-addon">%</span>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="form-group col">
                <label for="mortgageLength">Mortgage length</label>
                <div class="input-group currency">
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
  </main>
</template>

<script>

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
      if (!this.annualInterestRate || !this.mortgageAmount || !this.mortgageLength) {
        return false
      }

      var p = this.mortgageAmount
      var n = this.mortgageLength * 12
      var i = this.annualInterestRate / 100 / 12
      return Number.parseFloat(p * i * (Math.pow(1 + i, n)) / (Math.pow(1 + i, n) - 1)).toFixed(2)
    }
  }
}

</script>

<style lang="scss" scoped>

</style>
