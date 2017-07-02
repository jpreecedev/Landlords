<template>
  <div>
    <h1 class="display-2">Is this property a good investment?</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <form role="form" novalidate>
      <div class="row">
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary white--text">
              Basic details about your property
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="pricePaid"
                                :rules="[$validation.rules.required, $validation.rules.min_value(pricePaid, 10000)]"
                                label="Price paid"
                                step="1"
                                min="10000"
                                type="number"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="annualInterestRate"
                                :rules="[$validation.rules.required, $validation.rules.min_value(annualInterestRate, 0.1), $validation.rules.max_value(annualInterestRate, 20)]"
                                label="Interest rate (annual)"
                                step="0.1"
                                min="0.1"
                                max="20"
                                type="number"
                                suffix="%"
                                required>
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="mortgageLength"
                                :rules="[$validation.rules.required, $validation.rules.min_value(mortgageLength, 1), $validation.rules.max_value(mortgageLength, 50)]"
                                label="Mortgage length"
                                type="number"
                                step="1"
                                min="0"
                                max="50"
                                suffix="years"
                                required>
                  </v-text-field>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Income
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="expectedRentalIncome"
                                :rules="[$validation.rules.required, $validation.rules.min_value(expectedRentalIncome, 0), $validation.rules.max_value(expectedRentalIncome, 1000000)]"
                                label="Expected rental income"
                                type="number"
                                step="100"
                                min="0"
                                max="1000000"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-6">
                  <v-radio value="Monthly"
                           v-model="rentalIncomeFrequency"
                           label="Monthly"
                           checked>
                  </v-radio>
                </div>
                <div class="col-xs-6">
                  <v-radio value="Annual"
                           v-model="rentalIncomeFrequency"
                           label="Annual">
                  </v-radio>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Taxation
            </v-card-title>
            <v-card-text>
              <p>Which tax band are you in?</p>
              <v-card-row class="row">
                <div class="col-xs-12 col-md-4">
                  <v-radio value="Basic (20%)"
                           v-model="taxBand"
                           label="Basic (20%)">
                  </v-radio>
                </div>
                <div class="col-xs-12 col-md-4">
                  <v-radio value="Higher (40%)"
                           v-model="taxBand"
                           label="Higher (40%)"
                           checked>
                  </v-radio>
                </div>
                <div class="col-xs-12 col-md-4">
                  <v-radio value="Top (40%)"
                           v-model="taxBand"
                           label="Top (45%)">
                  </v-radio>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Other
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Contingency"
                                prefix="£"
                                v-model="contingency">
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Other costs"
                                prefix="£"
                                v-model="otherCosts">
                  </v-text-field>
                </div>
              </v-card-row>
            </v-card-text>
          </v-card>
        </div>
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary white--text">
              Year on year growth
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12 col-md-9">
                  <v-text-field type="number"
                                step="1"
                                min="0"
                                max="100"
                                label="Expected annual increase in value"
                                suffix="%"
                                v-model="anticipatedAnnualIncrease">
                  </v-text-field>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Agency Fees
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Agency fees"
                                prefix="£"
                                v-model="agencyFee">
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12 col-md-6">
                  <v-radio value="Monthly"
                           v-model="agencyFeeFrequency"
                           label="Monthly"
                           checked>
                  </v-radio>
                </div>
                <div class="col-xs-12 col-md-6">
                  <v-radio value="Annual"
                           v-model="agencyFeeFrequency"
                           label="Annual">
                  </v-radio>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Maintenance Fees
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Maintenance fees"
                                prefix="£"
                                v-model="maintenanceFees">
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12 col-md-6">
                  <v-radio value="Monthly"
                           v-model="maintenanceFeeFrequency"
                           label="Monthly"
                           checked>
                  </v-radio>
                </div>
                <div class="col-xs-12 col-md-6">
                  <v-radio value="Annual"
                           v-model="maintenanceFeeFrequency"
                           label="Annual">
                  </v-radio>
                </div>
              </v-card-row>
            </v-card-text>
            <v-card-title>
              Insurance
            </v-card-title>
            <v-card-text>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Buildings insurance (per year)"
                                prefix="£"
                                v-model="buildingsInsurance">
                  </v-text-field>
                </div>
              </v-card-row>
              <v-card-row class="row">
                <div class="col-xs-12">
                  <v-text-field type="number"
                                step="1"
                                label="Contents insurance (per year)"
                                prefix="£"
                                v-model="contentsInsurance">
                  </v-text-field>
                </div>
              </v-card-row>
            </v-card-text>
          </v-card>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-xs-12">
          <v-card>
            <v-card-title class="primary white--text">
              Your results
            </v-card-title>
            <v-card-text>
              <div v-if="!calculateScore">
                <p class="red--text">Please fix any validation errors to see your results</p>
              </div>
              <v-card-row class="row" v-if="calculateScore">
                <p>The score based on the information provided is: <strong>{{ calculateScore.scoreDisplay }}</strong>.</p>
                <p>The property may make <strong>&pound;{{ calculateScore.profit.formatWithSeparator() }}</strong> profit per year, and in <strong>{{ mortgageLength }}</strong> years will be worth around <strong>&pound;{{ getPropertyFutureValue().formatWithSeparator() }}</strong>.  You will pay around <strong>&pound;{{ getTotalMortgageInterest().formatWithSeparator() }}</strong> in interest on the mortgage (assuming the rate doesn't change).</p>
                <p>This means that over <strong>{{ mortgageLength }}</strong> years, you could pocket up to <strong>&pound;{{ getPocketAmount().formatWithSeparator()  }}</strong>.</p>
              </v-card-row>
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
        if (this.errorBag.errors.length !== 0) {
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
