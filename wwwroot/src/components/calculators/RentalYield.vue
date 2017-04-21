<template>
  <main class="container">
    <div> 
      <h1>Rental Yield Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="card col-6">
        <form role="form" class="card-block" novalidate>
          <fieldset>
            <div class="row">
              <div class="form-group col">
                <label for="purchasePrice">Total cost of property</label>          
                <div class="input-group currency">
                  <span class="input-group-addon">£</span>
                  <input type="number" class="form-control" id="purchasePrice" name="purchasePrice" v-model="rentalyield.purchasePrice" placeholder="Purchase price">
                  <span class="input-group-addon">.00</span>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="form-group col">
                <label for="rentalValue">Expected rental income</label>
                <div class="input-group currency">
                  <span class="input-group-addon">£</span>
                  <input type="number" class="form-control" id="rentalValue" name="rentalValue" step="0" v-model="rentalyield.rentalValue" placeholder="Rental value">
                  <span class="input-group-addon">.00</span>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="form-group col">
                <p class="mb-2">Rental income period</p>
                <div class="form-check form-check-inline">
                  <label class="form-check-label">
                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="Monthly" value="Monthly" v-model="rentalyield.frequency" checked> Monthly
                  </label>
                </div>
                <div class="form-check form-check-inline">
                  <label class="form-check-label">
                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="Annual" value="Annual" v-model="rentalyield.frequency"> Annual
                  </label>
                </div>
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
      if (!this.rentalyield.rentalValue || !this.rentalyield.purchasePrice) {
        return false
      }

      var multiplier = this.rentalyield.frequency === 'Monthly' ? 12 : 1
      return Number.parseFloat(((this.rentalyield.rentalValue * multiplier) / this.rentalyield.purchasePrice) * 100).toFixed(2)
    }
  }
}

</script>
  
<style lang="scss" scoped>
  
</style>
