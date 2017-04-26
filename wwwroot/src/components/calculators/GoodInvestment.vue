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
                  <div class="form-group col">
                    <label for="pricePaid">Price paid<br/>
                    </label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="pricePaid" name="pricePaid" v-model="pricePaid" placeholder="Amount borrowed">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="annualInterestRate">Interest rate (annual)</label>
                    <div class="input-group">
                      <input type="number" step="any" class="form-control" id="annualInterestRate" name="annualInterestRate" v-model="annualInterestRate" placeholder="Interest Rate">
                      <span class="input-group-addon">%</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="mortgageLength">Mortgage length</label>
                    <div class="input-group">
                      <input type="number" step="any" class="form-control" id="mortgageLength" name="mortgageLength" v-model="mortgageLength" placeholder="Mortgage length">
                      <span class="input-group-addon">years</span>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Income</h4>
                <div class="row">
                  <div class="form-group col">
                    <label for="expectedRentalIncome">Expected rental income</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" class="form-control" id="expectedRentalIncome" name="expectedRentalIncome" v-model="expectedRentalIncome" placeholder="Expected rental income">
                      <span class="input-group-addon">.00</span>
                    </div>
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
                      <input type="number" step="any" class="form-control" id="anticipatedAnnualIncrease" name="anticipatedAnnualIncrease" v-model="anticipatedAnnualIncrease" placeholder="Anticipated Annual Increase">
                      <span class="input-group-addon">%</span>
                    </div>
                  </div>
                </div>
                <h4 class="card-title mt-3">Outgoings</h4>
                <div class="row">
                  <div class="form-group col">
                    <label for="agencyFee">Agency fees</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="agencyFee" name="agencyFee" v-model="agencyFee" placeholder="Agency fees">
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
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="maintenanceFees" name="maintenanceFees" v-model="maintenanceFees" placeholder="Maintenance fees">
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
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="buildingsInsurance" name="buildingsInsurance" v-model="buildingsInsurance" placeholder="Buildings Insurance">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="contentsInsurance">Contents insurance (per year)</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="contentsInsurance" name="contentsInsurance" v-model="contentsInsurance" placeholder="Contents Insurance">
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
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="contingency" name="contingency" v-model="contingency" placeholder="Contingency">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="otherCosts">Other costs</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" step="any" class="form-control" id="otherCosts" name="otherCosts" v-model="otherCosts" placeholder="Other costs">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row mt-3" v-if="calculateScore">
                  <div class="col">
                    <p>The score based on the information provided is: <strong>{{ calculateScore.scoreDisplay }}</strong>.</p>
                    <p>The property may make <strong>&pound;{{ this.calculateScore.profit.formatWithSeparator() }}</strong> profit per year, and in <strong>{{ this.mortgageLength }}</strong> years will be worth around <strong>&pound;{{ this.getPropertyFutureValue().formatWithSeparator() }}</strong>.  You will pay around <strong>&pound;{{ ((this.getMonthlyPayment() * 12 * this.mortgageLength) - this.pricePaid).formatWithSeparator() }}</strong> in interest on the mortgage (assuming the rate doesn't change).</p>
                    <p>This means that over <strong>{{ this.mortgageLength }}</strong> years, you could pocket up to <strong>&pound;{{ ((this.getPropertyFutureValue() - this.pricePaid) + (this.calculateScore.profit * this.mortgageLength )).formatWithSeparator()  }}</strong> profit.</p>
                    <p><strong>Note:</strong> This figure has not been adjusted for inflation.  Your actual return will vary and has been provided as a guideline only.</p>
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
  import utils from '@/utils'
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
        otherCosts: 100
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
      }
    },
    computed: {
      annualYield: function () {
        return Number.parseFloat((this.getMonthlyIncome() / this.pricePaid.toFloat() * 100).toFixed(2))
      },
      totalIncome: function () {
        return this.getMonthlyIncome() * this.getTaxBand()
      },
      totalOutgoings: function () {
        return this.getMonthlyPayment() + this.getAgencyFees() + this.getMaintenanceFees() + this.getInsuranceCosts() + this.otherCosts.toFloat() + this.contingency.toFloat()
      },
      calculateScore: function () {
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
