<template>
  <main>
    <div> 
      <h1 class="md-display-2">Rental Yield Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="col-xs-12 col-md-6">        
        <form role="form" novalidate>
          <fieldset>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container class="input-group">
                  <md-icon>&pound;</md-icon>
                  <label for="purchasePrice" class="form-control-label">Total cost of property</label>          
                  <md-input type="number" id="purchasePrice" name="purchasePrice" v-model="rentalyield.purchasePrice" min="10000" />
                </md-input-container>
                <span v-show="errors.has('purchasePrice:required')" v-bind:title="errors.first('purchasePrice')" class="form-control-feedback">Enter the amount paid for the property</span>
                <span v-show="errors.has('purchasePrice:min_value')" v-bind:title="errors.first('purchasePrice')" class="form-control-feedback">Enter at least &pound;10,000</span>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container>
                  <md-icon>&pound;</md-icon>
                  <label for="rentalValue">Expected rental income</label>
                  <md-input type="number" id="rentalValue" name="rentalValue" min="1" step="1" v-model="rentalyield.rentalValue" />
                </md-input-container>
                <span v-show="errors.has('rentalValue:required')" v-bind:title="errors.first('rentalValue')" class="form-control-feedback">Enter the expected rental income</span>
                <span v-show="errors.has('rentalValue:min_value')" v-bind:title="errors.first('rentalValue')" class="form-control-feedback">Enter at least &pound;1</span>
              </div>
            </div>
            <div class="row">
              <div class="col">
                <p class="mb-0">Rental income period</p>
                
                  <label class="form-check-label">
                    <md-radio name="inlineRadioOptions" id="Monthly" md-value="Monthly" v-model="rentalyield.frequency" checked>Monthly</md-radio>
                  </label>
                
                  <label class="form-check-label">
                    <md-radio name="inlineRadioOptions" id="Annual" md-value="Annual" v-model="rentalyield.frequency">Annual</md-radio>
                  </label>
                
              </div>
            </div>
            <div class="row mt-4" v-if="!calculateRentalYield">
              <div class="col">
                <p class="text-danger">Please fix any validation errors highlighted in red to see your results</p>
              </div>
            </div>
            <div class="row mt-4" v-if="calculateRentalYield">
              <div class="col">                
                <p><strong>Gross rental yield:</strong> {{ calculateRentalYield }}%.<br/>
                  <span v-if="calculateRentalYield >= 10">This is an EXCELLENT gross yield.</span>
                  <span v-if="calculateRentalYield >= 7 && calculateRentalYield <= 9.9">This is a GOOD gross yield.</span>
                  <span v-if="calculateRentalYield >= 5 && calculateRentalYield <= 6.9">This is an 'OK' gross yield.</span>
                  <span v-if="calculateRentalYield < 5">The gross yield is LOW.</span>
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
  name: 'rentalyield',
  data () {
    return {
      rentalyield: {
        purchasePrice: 120000,
        rentalValue: 550,
        frequency: 'Monthly'
      }
    }
  },
  computed: {
    calculateRentalYield: function () {
      if (!this.rentalyield.rentalValue || !this.rentalyield.purchasePrice || this.errors.errors.length !== 0) {
        return false
      }

      var multiplier = this.rentalyield.frequency === 'Monthly' ? 12 : 1
      return Number.parseFloat(((this.rentalyield.rentalValue * multiplier) / this.rentalyield.purchasePrice) * 100).toFixed(2)
    }
  }
}

</script>
