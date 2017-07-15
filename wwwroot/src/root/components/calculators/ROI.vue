<template>
  <div>
    <h1 class="display-2">Return on investment (ROI) calculator</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Itaque consequatur quisquam vel officiis, asperiores velit, quaerat iusto sed, beatae cupiditate aperiam mollitia nesciunt. Aliquam, velit eos. Ad fuga numquam neque.</p>
    <form role="form" novalidate>
      <div class="row">
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary title">
              Purchase financials
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="pricePaid"
                                :rules="[$validation.rules.required, $validation.rules.min_value(pricePaid, 10000)]"
                                label="Purchase price"
                                step="1"
                                min="10000"
                                type="number"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="deposit"
                                :rules="[$validation.rules.required, $validation.rules.min_value(deposit, 1000)]"
                                label="Deposit"
                                step="1"
                                min="1000"
                                type="number"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="fees"
                                :rules="[$validation.rules.required]"
                                label="Refurbishment, fees, furnishings and other costs"
                                step="1"
                                type="number"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              Rental
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="lettableUnits"
                                :rules="[$validation.rules.required, $validation.rules.min_value(lettableUnits, 1)]"
                                label="Lettable units"
                                type="number"
                                step="1"
                                min="0"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="expectedRentalIncome"
                                :rules="[$validation.rules.required, $validation.rules.min_value(expectedRentalIncome, 0)]"
                                label="Rental income per unit"
                                type="number"
                                step="100"
                                min="0"
                                max="1000000"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
            </v-card-text>
            <v-card-title class="title">
              On-going costs
            </v-card-title>
            <v-card-text>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="mortgageInterestRate"
                                :rules="[$validation.rules.required, $validation.rules.min_value(mortgageInterestRate, 0.1), $validation.rules.max_value(mortgageInterestRate, 20)]"
                                label="Mortgage interest rate"
                                step="0.1"
                                min="0.1"
                                max="20"
                                type="number"
                                suffix="%"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="managementCost"
                                :rules="[$validation.rules.required, $validation.rules.min_value(managementCost, 0), $validation.rules.max_value(managementCost, 100)]"
                                label="Management cost (Monthly)"
                                step="1"
                                min="0"
                                max="100"
                                type="number"
                                suffix="%"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="repairsContingency"
                                :rules="[$validation.rules.required, $validation.rules.min_value(repairsContingency, 0), $validation.rules.max_value(repairsContingency, 100)]"
                                label="Repairs contingency"
                                step="1"
                                min="0"
                                max="100"
                                type="number"
                                suffix="%"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="serviceCharge"
                                :rules="[$validation.rules.required, $validation.rules.min_value(serviceCharge, 0)]"
                                label="Service charge and ground rent (Annual)"
                                type="number"
                                step="100"
                                min="0"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12">
                  <v-text-field v-model="insurance"
                                :rules="[$validation.rules.required, $validation.rules.min_value(insurance, 0)]"
                                label="Insurance (Annual)"
                                type="number"
                                step="100"
                                min="0"
                                prefix="£"
                                required>
                  </v-text-field>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </div>
        <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="title primary">
              Your results
            </v-card-title>
            <v-card-text>
              <div v-if="!score">
                <p class="red--text">Please fix any validation errors to see your results</p>
              </div>
              <div v-if="score">
                <p>
                  Based on the information provided above, use the following calculations to decide if the return on investment meets or exceeds your expections.
                </p>
                <p>
                  In our opinion, this is <strong>{{scoreInWords}}</strong> investment.
                </p>
                <p>
                  Monthly profit: <strong>&pound;{{score.monthlyProfit.formatWithSeparator()}}</strong>
                </p>
                <p>
                  Annual profit: <strong>&pound;{{score.annualProfit.formatWithSeparator()}}</strong>
                </p>
                <p>
                  Net yield: <strong>{{score.netYield * 100}}%</strong>
                </p>
                <p>
                  Return on investment: <strong>{{score.roi * 100}}%</strong>
                </p>
              </div>
            </v-card-text>
          </v-card>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
  export default {
    name: 'roi',
    data () {
      return {
        pricePaid: 60000,
        deposit: 15000,
        fees: 15000,
        lettableUnits: 1,
        expectedRentalIncome: 500,
        mortgageInterestRate: 5,
        managementCost: 0,
        repairsContingency: 10,
        serviceCharge: 0,
        insurance: 300
      }
    },
    computed: {
      score () {
        let monthlyRent = this.lettableUnits.toFloat() * this.expectedRentalIncome.toFloat()
        let annualIncome = monthlyRent * 12

        let annualInterest = (this.pricePaid.toFloat() - this.deposit.toFloat()) * (this.mortgageInterestRate.toFloat() / 100)
        let annualManagement = annualIncome * (this.managementCost.toFloat() / 100)
        let annualRepairs = annualIncome * (this.repairsContingency.toFloat() / 100)

        let annualProfit = annualIncome - ((annualInterest + annualManagement) + annualRepairs + this.serviceCharge.toFloat() + this.insurance.toFloat())
        let monthlyProfit = annualProfit / 12
        let netYield = annualProfit / this.pricePaid.toFloat()
        let roi = annualProfit / (this.deposit.toFloat() + this.fees.toFloat())

        return {
          annualProfit,
          monthlyProfit,
          netYield,
          roi
        }
      },
      scoreInWords () {
        let roi = this.score.roi * 100
        if (roi < 3) {
          return 'a poor'
        } else if (roi >= 3 && roi < 10) {
          return 'a good'
        } else if (roi > 10) {
          return 'an excellent'
        }
      }
    }
  }
</script>
