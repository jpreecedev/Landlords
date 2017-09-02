<template>
  <div>
    <header>
      <h1 class="headline primary--text">Good investment</h1>
      <p class="display-2 grey--text text--darken-1">All things considered, over the long term, will your property make a profit?</p>
      <p class="subheading">Complete this form to see your personalised results.</p>
    </header>
    <form role="form" novalidate>
      <div class="row">
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="title primary">
              Basic details about your property
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="pricePaid"
                              :rules="[$validation.rules.required, $validation.rules.min_value(pricePaid, 10000)]"
                              label="Price paid"
                              step="1"
                              min="10000"
                              type="number"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="annualInterestRate"
                              :rules="[$validation.rules.required, $validation.rules.min_value(annualInterestRate, 0.1), $validation.rules.max_value(annualInterestRate, 20)]"
                              label="Interest rate (annual)"
                              step="0.1"
                              min="0.1"
                              max="20"
                              type="number"
                              suffix="%"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="mortgageLength"
                              :rules="[$validation.rules.required, $validation.rules.min_value(mortgageLength, 1), $validation.rules.max_value(mortgageLength, 50)]"
                              label="Mortgage length"
                              type="number"
                              step="1"
                              min="0"
                              max="50"
                              suffix="years"
                              required>
                  </text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              Income
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field v-model="expectedRentalIncome"
                              :rules="[$validation.rules.required, $validation.rules.min_value(expectedRentalIncome, 0)]"
                              label="Expected rental income"
                              type="number"
                              step="100"
                              min="0"
                              prefix="£"
                              required>
                  </text-field>
                </div>
              </div>
              <v-radio-group v-model="rentalIncomeFrequency"
                              :mandatory="true">
                <div class="row">
                    <div class="col-xs-6">
                      <v-radio value="Monthly"
                              color="primary"
                              label="Monthly"
                              checked>
                      </v-radio>
                    </div>
                    <div class="col-xs-6">
                      <v-radio value="Annual"
                              color="primary"
                              label="Annual">
                      </v-radio>
                    </div>
                </div>
              </v-radio-group>
            </v-card-text>
            <v-card-title class="title">
              Taxation
            </v-card-title>
            <v-card-text>
              <p>Which tax band are you in?</p>
              <v-radio-group v-model="taxBand"
                             :mandatory="true">
                <div class="row">
                  <div class="col-xs-12 col-md-4">
                    <v-radio value="BasicRate"
                            color="primary"
                            label="Basic (20%)">
                    </v-radio>
                  </div>
                  <div class="col-xs-12 col-md-4">
                    <v-radio value="Higher"
                            color="primary"
                            label="Higher (40%)"
                            checked>
                    </v-radio>
                  </div>
                  <div class="col-xs-12 col-md-4">
                    <v-radio value="Top"
                            color="primary"
                            label="Top (45%)">
                    </v-radio>
                  </div>
                </div>
              </v-radio-group>
            </v-card-text>
            <v-card-title class="title">
              Other
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              label="Contingency"
                              prefix="£"
                              v-model="contingency">
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              min="0"
                              label="Other costs"
                              prefix="£"
                              v-model="otherCosts">
                  </text-field>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </div>
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="title primary">
              Year on year growth
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12 col-md-9">
                  <text-field type="number"
                              step="1"
                              min="0"
                              max="100"
                              label="Expected annual increase in value"
                              suffix="%"
                              v-model="anticipatedAnnualIncrease">
                  </text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              Agency Fees
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              min="0"
                              label="Agency fees"
                              prefix="£"
                              v-model="agencyFee">
                  </text-field>
                </div>
              </div>
              <v-radio-group v-model="agencyFeeFrequency"
                             :mandatory="true">
                <div class="row">
                  <div class="col-xs-12 col-md-6">
                    <v-radio value="Monthly"
                            color="primary"
                            label="Monthly"
                            checked>
                    </v-radio>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-radio value="Annual"
                            color="primary"
                            label="Annual">
                    </v-radio>
                  </div>
                </div>
              </v-radio-group>
            </v-card-text>
            <v-card-title class="title">
              Maintenance Fees
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              min="0"
                              label="Maintenance fees"
                              prefix="£"
                              v-model="maintenanceFees">
                  </text-field>
                </div>
              </div>
              <v-radio-group v-model="maintenanceFeeFrequency"
                             :mandatory="true">
                <div class="row">
                  <div class="col-xs-12 col-md-6">
                    <v-radio value="Monthly"
                             color="primary"
                             label="Monthly"
                             checked>
                    </v-radio>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-radio value="Annual"
                             color="primary"
                             label="Annual">
                    </v-radio>
                  </div>
                </div>
              </v-radio-group>
            </v-card-text>
            <v-card-title class="title">
              Insurance
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              min="0"
                              label="Buildings insurance (per year)"
                              prefix="£"
                              v-model="buildingsInsurance">
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <text-field type="number"
                              step="1"
                              min="0"
                              label="Contents insurance (per year)"
                              prefix="£"
                              v-model="contentsInsurance">
                  </text-field>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-xs-12">
          <v-card>
            <v-card-title class="title primary">
              Results
            </v-card-title>
            <v-card-text>
              <div v-if="!calculateScore">
                <p class="red--text">Please fix any validation errors to see your results</p>
              </div>
              <div>
                <p>The score based on the information provided is: <strong>{{ calculateScore.scoreDisplay }}</strong>.</p>
                <p>The property may make <strong>&pound;{{ calculateScore.profit.formatWithSeparator() }}</strong> profit per year, and in <strong>{{ mortgageLength }}</strong> years will be worth around <strong>&pound;{{ getPropertyFutureValue().formatWithSeparator() }}</strong>.  You will pay around <strong>&pound;{{ getTotalMortgageInterest().formatWithSeparator() }}</strong> in interest on the mortgage (assuming the rate doesn't change).</p>
                <p>This means that over <strong>{{ mortgageLength }}</strong> years, you could pocket up to <strong>&pound;{{ getPocketAmount().formatWithSeparator()  }}</strong>.</p>
              </div>
            </v-card-text>
            <v-card-text v-if="calculateScore">
              <p>This is calculated by taking the potential gross profit of <strong>&pound;{{ getGrossProfit().formatWithSeparator() }}</strong>, subtracting your tax free allowance of <strong>&pound;{{ taxFreeAllowance.formatWithSeparator() }}</strong>, and then subtracting capital gains tax of approximately <strong>&pound;{{ getCapitalGainsTax().formatWithSeparator() }}</strong>.</p>
              <p><strong>Note:</strong> These figures have not been adjusted for inflation.  Your actual return will vary and has been provided as a guideline only.</p>
            </v-card-text>
          </v-card>
        </div>
      </div>
    </form>
  </div>
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
      getMonthlyPayment () {
        return utils.calculateMonthlyPayment(this.annualInterestRate.toFloat(), this.pricePaid.toFloat(), this.mortgageLength.toFloat()).toFloat()
      },
      getMonthlyIncome () {
        return this.rentalIncomeFrequency === 'Monthly' ? this.expectedRentalIncome.toFloat() * 12 : this.expectedRentalIncome.toFloat()
      },
      getAgencyFees () {
        return this.agencyFeeFrequency === 'Monthly' ? this.agencyFee.toFloat() * 12 : this.agencyFee.toFloat()
      },
      getMaintenanceFees () {
        return this.maintenanceFeeFrequency === 'Monthly' ? this.maintenanceFees.toFloat() * 12 : this.maintenanceFees.toFloat()
      },
      getPropertyFutureValue () {
        let future = this.pricePaid.toFloat() * Math.pow((1 + this.anticipatedAnnualIncrease.toFloat() / 100), this.mortgageLength.toFloat())
        return Math.round(future * 100) / 100
      },
      getInsuranceCosts () {
        return this.buildingsInsurance.toFloat() + this.contentsInsurance.toFloat()
      },
      getTaxBand () {
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
      getTaxBandForCapitalGains () {
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
      getTotalMortgageInterest () {
        return (this.getMonthlyPayment() * 12 * this.mortgageLength) - this.pricePaid
      },
      getPocketAmount () {
        return (((this.getPropertyFutureValue() - this.pricePaid) + (this.calculateScore.profit * this.mortgageLength)) - this.taxFreeAllowance) * this.getTaxBandForCapitalGains()
      },
      getGrossProfit () {
        return (this.getPropertyFutureValue() - this.pricePaid) + (this.calculateScore.profit * this.mortgageLength)
      },
      getCapitalGainsTax () {
        return this.getGrossProfit() * (1 - this.getTaxBandForCapitalGains())
      }
    },
    computed: {
      annualYield () {
        return (this.getMonthlyIncome() / this.pricePaid.toFloat() * 100).toFloat().toFixed(2)
      },
      totalIncome () {
        return this.getMonthlyIncome() * this.getTaxBand()
      },
      totalOutgoings () {
        return this.getMonthlyPayment() + this.getAgencyFees() + this.getMaintenanceFees() + this.getInsuranceCosts() + this.otherCosts.toFloat() + this.contingency.toFloat()
      },
      calculateScore () {
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
