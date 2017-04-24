<template>
  <main class="container">
    <div>
      <h1>How much can I borrow?</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <div class="row">
      <div class="col-6">
        <div class="card">
          <form role="form" class="card-block" novalidate>
            <fieldset>
              <div class="row">
                <div class="form-group col">
                  <label for="grossIncome">
                  Your annual gross income<br/>
                  <small class="text-muted">How much you earn, including everybody who will be included on the mortgage, BEFORE tax</small>                
                  </label>
                  <div class="input-group">
                    <span class="input-group-addon">£</span>
                    <input type="number" class="form-control" id="grossIncome" name="grossIncome" v-model="grossIncome" placeholder="Gross income">
                    <span class="input-group-addon">.00</span>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="form-group col">
                  <label for="multiplier">Multiplier (use the default if unsure)</label>
                    <div>
                      <select v-model="multiplier" class="form-control" id="multiplier" name="multiplier" required>
                        <option disabled value="">Select a multiplier</option>
                        <option v-for="multiplier in multipliers" v-bind:value="multiplier">{{ multiplier }}</option>
                      </select>
                    </div>
                </div>
              </div>
              <div class="row mt-4" v-if="calculateAmountCanBorrow">
                <div class="col">
                  <p><strong>You could potentially borrow up to:</strong> £{{ calculateAmountCanBorrow }}.
                  </p>
                </div>
              </div>
            </fieldset>
          </form>
        </div>
      </div>
    </div>
  </main>
</template>

<script>

export default {
  name: 'howMuch',
  data () {
    return {
      grossIncome: 35000,
      multipliers: [3, 3.5, 4, 4.5, 5],
      multiplier: 3.5
    }
  },
  computed: {
    calculateAmountCanBorrow: function () {
      if (!this.grossIncome || !this.multiplier) {
        return false
      }

      return Number.parseFloat(this.grossIncome * this.multiplier).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')
    }
  }
}

</script>

<style lang="scss" scoped>

</style>
