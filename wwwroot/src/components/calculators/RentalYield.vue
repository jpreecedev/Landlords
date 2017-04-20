<template>
  <main class="container">
    <div> 
      <h1>Rental Yield Calculator</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <form role="form" novalidate>
      <fieldset>
        <div class="row">
          <div class="form-group col-6">
            <label for="purchasePrice">Total cost of property (including tax, renovation costs, agency fees etc.)</label>          
            <input type="number" class="form-control" id="purchasePrice" name="purchasePrice" v-model="rentalyield.purchasePrice" placeholder="Purchase price">
          </div>
          <div class="form-group col-6">
            <label for="rentalValue">Expected annual rent (Gross monthly rent multipled by 12)</label>
            <input type="number" class="form-control" id="rentalValue" name="rentalValue" v-model="rentalyield.rentalValue" placeholder="Rental value">
          </div>
        </div>
      </fieldset>
    </form>    

    <p>Rental yield: {{ Number.parseFloat((rentalyield.rentalValue / rentalyield.purchasePrice) * 100).toFixed(2) }}%</p>

  </main>
</template>

<script>

export default {
  name: 'rentalyield',
  data () {
    return {
      rentalyield: {
        purchasePrice: 161500,
        rentalValue: 15000
      }
    }
  }
}

</script>
  
<style lang="scss" scoped>
  
</style>
