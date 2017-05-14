<template>
  <main class="container">
    <div>
      <h1>Is this property a good investment?</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <form role="form" novalidate>
      <fieldset>
        <div class="row">
          <div class="col-6">
            <div class="card">
              <div class="card-block">
                <h4 class="card-title">Basic details about your property</h4>
                <div class="row">
                  <div class="form-group col" :class="{ 'has-danger': errors.has('pricePaid') }">
                    <label for="pricePaid" class="form-control-label">Price paid</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" min="10000" id="pricePaid" name="pricePaid" v-model="pricePaid" v-validate="'required|min_value:10000'" data-vv-validate-on="change" placeholder="Amount borrowed">
                      <span class="input-group-addon">.00</span>
                    </div>
                    <span v-show="errors.has('pricePaid:required')" v-bind:title="errors.first('pricePaid')" class="form-control-feedback">Enter the amount paid for the property</span>
                    <span v-show="errors.has('pricePaid:min_value')" v-bind:title="errors.first('pricePaid')" class="form-control-feedback">Enter at least &pound;10,000</span>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col" :class="{ 'has-danger': errors.has('annualInterestRate') }">
                    <label for="annualInterestRate" class="form-control-label">Interest rate (annual)</label>
                    <div class="input-group">
                      <input type="number" step="1" class="form-control" id="annualInterestRate" name="annualInterestRate" v-model="annualInterestRate" v-validate="'required|min_value:0.1|max_value:20'" data-vv-validate-on="change" placeholder="Interest Rate">
                      <span class="input-group-addon">%</span>
                    </div>
                    <span v-show="errors.has('annualInterestRate:required')" v-bind:title="errors.first('annualInterestRate')" class="form-control-feedback">Enter the interest rate</span>
                    <span v-show="errors.has('annualInterestRate:min_value')" v-bind:title="errors.first('annualInterestRate')" class="form-control-feedback">Enter at least 0.1%</span>
                    <span v-show="errors.has('annualInterestRate:max_value')" v-bind:title="errors.first('annualInterestRate')" class="form-control-feedback">Enter up to 20%</span>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col" :class="{ 'has-danger': errors.has('mortgageLength') }">
                    <label for="mortgageLength" class="form-control-label">Mortgage length</label>
                    <div class="input-group">
                      <input type="number" step="1" class="form-control" id="mortgageLength" name="mortgageLength" v-model="mortgageLength" v-validate="'required|min_value:1|max_value:50'" data-vv-validate-on="change" placeholder="Mortgage length">
                      <span class="input-group-addon">years</span>
                    </div>
                    <span v-show="errors.has('mortgageLength:required')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter the mortgage length</span>
                    <span v-show="errors.has('mortgageLength:min_value')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter at least 1 year</span>
                    <span v-show="errors.has('mortgageLength:max_value')" v-bind:title="errors.first('mortgageLength')" class="form-control-feedback">Enter up to 50 years</span>
                  </div>
                </div>
                <h4 class="card-title mt-3">Income</h4>
                <div class="row">
                  <div class="form-group col" :class="{ 'has-danger': errors.has('expectedRentalIncome') }">
                    <label for="expectedRentalIncome" class="form-control-label">Expected rental income</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" class="form-control" id="expectedRentalIncome" name="expectedRentalIncome" v-model="expectedRentalIncome" v-validate="'required|min_value:1|max_value:1000000'" data-vv-validate-on="change" placeholder="Expected rental income">
                      <span class="input-group-addon">.00</span>
                    </div>
                    <span v-show="errors.has('expectedRentalIncome:required')" v-bind:title="errors.first('expectedRentalIncome')" class="form-control-feedback">Enter the rentval income value</span>
                    <span v-show="errors.has('expectedRentalIncome:min_value')" v-bind:title="errors.first('expectedRentalIncome')" class="form-control-feedback">Enter at least &pound;1</span>
                    <span v-show="errors.has('expectedRentalIncome:max_value')" v-bind:title="errors.first('expectedRentalIncome')" class="form-control-feedback">Enter up to &pound;1,000,000</span>
                  </div>
                </div>
                <div class="row mt-2">
                  <div class="form-group col">
                    <p class="mb-2">Rental income period</p>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="inlineRadioOptions" id="Monthly" value="Monthly" v-model="rentalIncomeFrequency" checked> Monthly
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="inlineRadioOptions" id="Annual" value="Annual" v-model="rentalIncomeFrequency"> Annual
                      </label>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Capital Gains</h4>
                <div class="row">
                  <div class="form-group col">
                    <label for="anticipatedAnnualIncrease">Anticipated increase in property value (annual)</label>
                    <div class="input-group">
                      <input type="number" step="1" class="form-control" id="anticipatedAnnualIncrease" name="anticipatedAnnualIncrease" v-model="anticipatedAnnualIncrease" placeholder="Anticipated Annual Increase">
                      <span class="input-group-addon">%</span>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Outgoings</h4>
                <div class="row">
                  <div class="form-group col">
                    <label for="agencyFee">Agency fees</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="agencyFee" name="agencyFee" v-model="agencyFee" placeholder="Agency fees">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row mt-2">
                  <div class="form-group col">
                    <p class="mb-2">Agency fees period</p>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="agencyFeesPeriodGroup" id="Monthly" value="Monthly" v-model="agencyFeeFrequency" checked> Monthly
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="agencyFeesPeriodGroup" id="Annual" value="Annual" v-model="agencyFeeFrequency"> Annual
                      </label>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="maintenanceFees">Maintenance fees</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="maintenanceFees" name="maintenanceFees" v-model="maintenanceFees" placeholder="Maintenance fees">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row mt-2">
                  <div class="form-group col">
                    <p class="mb-2">Maintenance fees period</p>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="maintenanceFeesPeriodGroup" id="Monthly" value="Monthly" v-model="maintenanceFeeFrequency" checked> Monthly
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="maintenanceFeesPeriodGroup" id="Annual" value="Annual" v-model="maintenanceFeeFrequency"> Annual
                      </label>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="buildingsInsurance">Buildings insurance (per year)</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="buildingsInsurance" name="buildingsInsurance" v-model="buildingsInsurance" placeholder="Buildings Insurance">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="contentsInsurance">Contents insurance (per year)</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="contentsInsurance" name="contentsInsurance" v-model="contentsInsurance" placeholder="Contents Insurance">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Taxation</h4>
                <div class="row mt-2">
                  <div class="form-group col">
                    <p class="mb-2">Which tax band are you in?</p>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="taxBandRadioGroup" id="basicRate" value="BasicRate" v-model="taxBand"> Basic Rate (20%)
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="taxBandRadioGroup" id="higher" value="Higher" v-model="taxBand" checked> Higher (40%)
                      </label>
                    </div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="taxBandRadioGroup" id="top" value="Top" v-model="taxBand" checked> Top (45%)
                      </label>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Other</h4>
                <div class="row">
                  <div class="form-group col">
                    <label for="contingency">Contingency</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="contingency" name="contingency" v-model="contingency" placeholder="Contingency">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="otherCosts">Other costs</label>
                    <div class="input-group">
                      <span class="input-group-addon">&pound;</span>
                      <input type="number" step="1" class="form-control" id="otherCosts" name="otherCosts" v-model="otherCosts" placeholder="Other costs">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-6">          
            <div class="card">
              <div class="card-block">
                <h4 class="card-title">Your results</h4>
                <div class="row mt-4" v-if="!calculateScore">
                  <div class="col">
                    <p class="text-danger">Please fix any validation errors highlighted in red to see your results</p>
                  </div>
                </div>
                <div class="row mt-4" v-if="calculateScore">
                  <div class="col">
                    <p>The score based on the information provided is: <strong>{{ calculateScore.scoreDisplay }}</strong>.</p>
                    <p>The property may make <strong>&pound;{{ calculateScore.profit.formatWithSeparator() }}</strong> profit per year, and in <strong>{{ mortgageLength }}</strong> years will be worth around <strong>&pound;{{ getPropertyFutureValue().formatWithSeparator() }}</strong>.  You will pay around <strong>&pound;{{ getTotalMortgageInterest().formatWithSeparator() }}</strong> in interest on the mortgage (assuming the rate doesn't change).</p>
                    <p>This means that over <strong>{{ mortgageLength }}</strong> years, you could pocket up to <strong>&pound;{{ getPocketAmount().formatWithSeparator()  }}</strong>.</p>
                    <hr>
                    <p>This is calculated by taking the potential gross profit of <strong>&pound;{{ getGrossProfit().formatWithSeparator() }}</strong>, subtracting your tax free allowance of <strong>&pound;{{ taxFreeAllowance.formatWithSeparator() }}</strong>, and then subtracting capital gains tax of approximately <strong>&pound;{{ getCapitalGainsTax().formatWithSeparator() }}</strong>.</p>
                    <p><strong>Note:</strong> These figures have not been adjusted for inflation.  Your actual return will vary and has been provided as a guideline only.</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
  import utils from 'utils'
  export default {
    name: 'goodInvestment',
    data () {
      return {
        pricePaid: 120000,
        annualInterestRate: 5,
        mortgageLength: 25,
        expectedRentalIncome: 425,
        rentalIncomeFrequency: 'Monthly',
        anticipatedAnnualIncrease: 5,
        agencyFee: 50,
        agencyFeeFrequency: 'Monthly',
        maintenanceFees: 500,
        maintenanceFeeFrequency: 'Annual',
        buildingsInsurance: 250,
        contentsInsurance: 75,
        taxBand: 'Higher',
        contingency: 500,
        otherCosts: 100,
        taxFreeAllowance: 11000
      }
    },
    methods: {
      getMonthlyPayment: function () {
        return utils.calculateMonthlyPayment(this.annualInterestRate.toFloat(), this.pricePaid.toFloat(), this.mortgageLength.toFloat()).toFloat()
      },
      getMonthlyIncome: function () {
        return this.rentalIncomeFrequency === 'Monthly' ? this.expectedRentalIncome.toFloat() * 12 : this.expectedRentalIncome.toFloat()
      },
      getAgencyFees: function () {
        return this.agencyFeeFrequency === 'Monthly' ? this.agencyFee.toFloat() * 12 : this.agencyFee.toFloat()
      },
      getMaintenanceFees: function () {
        return this.maintenanceFeeFrequency === 'Monthly' ? this.maintenanceFees.toFloat() * 12 : this.maintenanceFees.toFloat()
      },
      getPropertyFutureValue: function () {
        var future = this.pricePaid.toFloat() * Math.pow((1 + this.anticipatedAnnualIncrease.toFloat() / 100), this.mortgageLength.toFloat())
        return Math.round(future * 100) / 100
      },
      getInsuranceCosts: function () {
        return this.buildingsInsurance.toFloat() + this.contentsInsurance.toFloat()
      },
      getTaxBand: function () {
        switch (this.taxBand) {
          case 'BasicRate':
            return 0.8
          case 'Higher':
            return 0.6
          case 'Top':
            return 0.55
          default:
            return false
        }
      },
      getTaxBandForCapitalGains: function () {
        switch (this.taxBand) {
          case 'BasicRate':
            return 0.8
          case 'Higher':
          case 'Top':
            return 0.72
          default:
            return false
        }
      },
      getTotalMortgageInterest: function () {
        return (this.getMonthlyPayment() * 12 * this.mortgageLength) - this.pricePaid
      },
      getPocketAmount: function () {
        return (((this.getPropertyFutureValue() - this.pricePaid) + (this.calculateScore.profit * this.mortgageLength)) - this.taxFreeAllowance) * this.getTaxBandForCapitalGains()
      },
      getGrossProfit: function () {
        return (this.getPropertyFutureValue() - this.pricePaid) + (this.calculateScore.profit * this.mortgageLength)
      },
      getCapitalGainsTax: function () {
        return this.getGrossProfit() * (1 - this.getTaxBandForCapitalGains())
      }
    },
    computed: {
      annualYield: function () {
        return (this.getMonthlyIncome() / this.pricePaid.toFloat() * 100).toFloat().toFixed(2)
      },
      totalIncome: function () {
        return this.getMonthlyIncome() * this.getTaxBand()
      },
      totalOutgoings: function () {
        return this.getMonthlyPayment() + this.getAgencyFees() + this.getMaintenanceFees() + this.getInsuranceCosts() + this.otherCosts.toFloat() + this.contingency.toFloat()
      },
      calculateScore: function () {
        if (this.errors.errors.length !== 0) {
          return false
        }
        return utils.calculateScore({
          annualYield: this.annualYield,
          growth: this.anticipatedAnnualIncrease,
          income: this.totalIncome,
          outgoings: this.totalOutgoings
        })
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
