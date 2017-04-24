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
                      <input type="number" class="form-control" id="pricePaid" name="pricePaid" v-model="pricePaid" placeholder="Amount borrowed">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="annualInterestRate">Interest rate (annual)</label>
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
                      <input type="number" class="form-control" id="anticipatedAnnualIncrease" name="anticipatedAnnualIncrease" step="0" v-model="anticipatedAnnualIncrease" placeholder="Anticipated Annual Increase">
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
                      <input type="number" class="form-control" id="agencyFee" name="agencyFee" v-model="agencyFee" placeholder="Agency fees">
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
                      <input type="number" class="form-control" id="maintenanceFees" name="maintenanceFees" v-model="maintenanceFees" placeholder="Maintenance fees">
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
                      <input type="number" class="form-control" id="buildingsInsurance" name="buildingsInsurance" v-model="buildingsInsurance" placeholder="Buildings Insurance">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="contentsInsurance">Contents insurance (per year)</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" class="form-control" id="contentsInsurance" name="contentsInsurance" v-model="contentsInsurance" placeholder="Contents Insurance">
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
                      <input type="number" class="form-control" id="contingency" name="contingency" v-model="contingency" placeholder="Contingency">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="form-group col">
                    <label for="otherCosts">Other costs</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" class="form-control" id="otherCosts" name="otherCosts" v-model="otherCosts" placeholder="Other costs">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="row mt-3" v-if="calculateResult">
                  <div class="col">
                    <p><strong>You could potentially borrow up to:</strong> £{{ calculateResult }}.</p>
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
        agencyFee: 51,
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
        return utils.calculateMonthlyPayment(this.annualInterestRate, this.pricePaid, this.mortgageLength)
      },
      getMonthlyIncome: function () {
        return this.rentalIncomeFrequency === 'Monthly' ? this.expectedRentalIncome * 12 : this.expectedRentalIncome
      },
      getAgencyFees: function () {
        return this.agencyFeeFrequency === 'Monthly' ? this.agencyFee * 12 : this.agencyFee
      },
      getMaintenanceFees: function () {
        return this.maintenanceFeeFrequency === 'Monthly' ? this.maintenanceFees * 12 : this.maintenanceFees
      },
      getPropertyFutureValue: function () {
        var future = this.pricePaid * Math.pow((1 + this.anticipatedAnnualIncrease / 100), this.mortgageLength)
        return Math.round(future * 100) / 100
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
      calculateResult: function () {
        // var monthlyIncome = this.getMonthlyIncome()
        // var agencyFees = this.getAgencyFees()
        // var maintenanceFees = this.getMaintenanceFees()
        // var insurance = this.buildingsInsurance + this.contentsInsurance
        // var taxBand = this.getTaxBand()

        // return Number.parseFloat(this.grossIncome * this.multiplier).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
