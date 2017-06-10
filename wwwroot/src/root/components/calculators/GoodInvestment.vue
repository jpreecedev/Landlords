<template>
  <main>
    <h1 class="md-display-2">Is this property a good investment?</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <form role="form" novalidate>
      <fieldset>
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-header>
                <div class="md-title">Basic details about your property</div>              
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('pricePaid') }">
                      <label for="pricePaid" class="form-control-label">Price paid</label>
                      <md-input type="number" step="1" class="form-control" min="10000" id="pricePaid" name="pricePaid" v-model="pricePaid" data-vv-name="pricePaid" v-validate="'required|min_value:10000'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('pricePaid:required')" class="md-error">Enter the amount paid</span>
                      <span v-else-if="errors.has('pricePaid:min_value')" class="md-error">Enter at least &pound;10,000</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('annualInterestRate') }">
                      <label for="annualInterestRate">Interest rate (annual)</label>
                      <md-input type="number" step="1" min="0" max="20" id="annualInterestRate" name="annualInterestRate" v-model="annualInterestRate" data-vv-name="annualInterestRate" v-validate="'required|min_value:0.1|max_value:20'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('annualInterestRate:required')" class="md-error">Enter the interest rate</span>
                      <span v-else-if="errors.has('annualInterestRate:min_value')" class="md-error">Enter at least 0.1%</span>
                      <span v-else-if="errors.has('annualInterestRate:max_value')" class="md-error">Enter up to 20%</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('mortgageLength') }">
                      <label for="mortgageLength">Mortgage length</label>
                      <md-input type="number" step="1" min="0" max="50" id="mortgageLength" name="mortgageLength" v-model="mortgageLength" data-vv-name="mortgageLength" v-validate="'required|min_value:1|max_value:50'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('mortgageLength:required')" class="md-error">Enter the mortgage length</span>
                      <span v-else-if="errors.has('mortgageLength:min_value')" class="md-error">Enter at least 1 year</span>
                      <span v-else-if="errors.has('mortgageLength:max_value')" class="md-error">Enter up to 50 years</span>
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Income</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('expectedRentalIncome') }">
                      <label for="expectedRentalIncome">Expected rental income</label>
                      <md-input type="number" step="100" min="0" max="1000000" id="expectedRentalIncome" name="expectedRentalIncome" v-model="expectedRentalIncome" data-vv-name="expectedRentalIncome" v-validate="'required|min_value:1|max_value:1000000'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('expectedRentalIncome:required')" class="md-error">Enter the rental income value</span>
                      <span v-else-if="errors.has('expectedRentalIncome:min_value')" class="md-error">Enter at least &pound;1</span>
                      <span v-else-if="errors.has('expectedRentalIncome:max_value')" class="md-error">Enter up to &pound;1,000,000</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">                    
                    <md-radio name="inlineRadioOptions" id="Monthly" md-value="Monthly" v-model="rentalIncomeFrequency" checked>Monthly</md-radio>                    
                    <md-radio name="inlineRadioOptions" id="Annual" md-value="Annual" v-model="rentalIncomeFrequency">Annual</md-radio>
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Taxation</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <p>Which tax band are you in?</p>                
                    <label>
                      <md-radio name="taxBandRadioGroup" id="basicRate" md-value="BasicRate" v-model="taxBand">Basic Rate (20%)</md-radio>
                    </label>              
                    <label>
                      <md-radio name="taxBandRadioGroup" id="higher" md-value="Higher" v-model="taxBand" checked>Higher (40%)</md-radio>
                    </label>              
                    <label>
                      <md-radio name="taxBandRadioGroup" id="top" md-value="Top" v-model="taxBand">Top (45%)</md-radio>
                    </label>                
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Other</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="contingency">Contingency</label>
                      <md-input type="number" step="1" id="contingency" name="contingency" v-model="contingency" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="otherCosts">Other costs</label>
                      <md-input type="number" step="1" id="otherCosts" name="otherCosts" v-model="otherCosts" />
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-header>
                <div class="md-title">Year on year growth</div>              
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12 col-md-9">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('anticipatedAnnualIncrease') }">
                      <label for="anticipatedAnnualIncrease">Expected increase in property value (per year)</label>
                      <md-input type="number" step="1" min="0" max="100" id="anticipatedAnnualIncrease" name="anticipatedAnnualIncrease" v-model="anticipatedAnnualIncrease" data-vv-name="anticipatedAnnualIncrease" v-validate="'required|min_value:0.1|max_value:100'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('anticipatedAnnualIncrease:required')" class="md-error">Enter the growth rate</span>
                      <span v-else-if="errors.has('anticipatedAnnualIncrease:min_value')" class="md-error">Enter at least 0.1%</span>
                      <span v-else-if="errors.has('anticipatedAnnualIncrease:max_value')" class="md-error">Enter up to 100%</span>
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Agency Fees</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="agencyFee">Agency fees</label>
                      <md-input type="number" step="1" id="agencyFee" name="agencyFee" v-model="agencyFee" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">              
                  <label class="form-check-label">
                    <md-radio name="agencyFeesPeriodGroup" id="Monthly" md-value="Monthly" v-model="agencyFeeFrequency" checked>Monthly</md-radio>
                  </label>              
                  <label class="form-check-label">
                    <md-radio name="agencyFeesPeriodGroup" id="Annual" md-value="Annual" v-model="agencyFeeFrequency">Annual</md-radio>
                  </label>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Maintenance Fees</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="maintenanceFees">Maintenance fees</label>
                      <md-input type="number" step="1" id="maintenanceFees" name="maintenanceFees" v-model="maintenanceFees" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <label class="form-check-label">
                      <md-radio name="maintenanceFeesPeriodGroup" id="Monthly" md-value="Monthly" v-model="maintenanceFeeFrequency" checked>Monthly</md-radio>
                    </label>
                    <label class="form-check-label">
                      <md-radio name="maintenanceFeesPeriodGroup" id="Annual" md-value="Annual" v-model="maintenanceFeeFrequency">Annual</md-radio>
                    </label>
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Insurance</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="buildingsInsurance">Buildings insurance (per year)</label>
                      <md-input type="number" step="1" class="form-control" id="buildingsInsurance" name="buildingsInsurance" v-model="buildingsInsurance" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="contentsInsurance">Contents insurance (per year)</label>
                      <md-input type="number" step="1" class="form-control" id="contentsInsurance" name="contentsInsurance" v-model="contentsInsurance" />
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <md-card>
              <md-card-header>
                <div class="md-title">Your results</div>
              </md-card-header>
              <md-card-content>
                <div v-if="!calculateScore">
                  <p class="md-warn">Please fix any validation errors to see your results</p>
                </div>
                <div class="row" v-if="calculateScore">                  
                  <p>The score based on the information provided is: <strong>{{ calculateScore.scoreDisplay }}</strong>.</p>
                  <p>The property may make <strong>&pound;{{ calculateScore.profit.formatWithSeparator() }}</strong> profit per year, and in <strong>{{ mortgageLength }}</strong> years will be worth around <strong>&pound;{{ getPropertyFutureValue().formatWithSeparator() }}</strong>.  You will pay around <strong>&pound;{{ getTotalMortgageInterest().formatWithSeparator() }}</strong> in interest on the mortgage (assuming the rate doesn't change).</p>
                  <p>This means that over <strong>{{ mortgageLength }}</strong> years, you could pocket up to <strong>&pound;{{ getPocketAmount().formatWithSeparator()  }}</strong>.</p>                  
                </div>
              </md-card-content>
            </md-card>
            <md-card v-if="calculateScore">
              <md-card-content>
                <p>This is calculated by taking the potential gross profit of <strong>&pound;{{ getGrossProfit().formatWithSeparator() }}</strong>, subtracting your tax free allowance of <strong>&pound;{{ taxFreeAllowance.formatWithSeparator() }}</strong>, and then subtracting capital gains tax of approximately <strong>&pound;{{ getCapitalGainsTax().formatWithSeparator() }}</strong>.</p>
                <p><strong>Note:</strong> These figures have not been adjusted for inflation.  Your actual return will vary and has been provided as a guideline only.</p>
              </md-card-content>
            </md-card>
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

  .md-card-content > .row {
    margin-left: 0;
    margin-right: 0;
  }

</style>
