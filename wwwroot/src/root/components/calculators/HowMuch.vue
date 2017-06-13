<template>
  <main>
    <h1 class="md-display-2">How much can I borrow?</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div class="row">
      <div class="col-xs-12">
        <form role="form" novalidate>
          <fieldset>
            <div class="row">
              <div class="col-xs-12 col-md-6">
                <md-input-container :class="{ 'md-input-invalid': errorBag.has('grossIncome') }">
                  <label for="grossIncome">Your annual gross income</label>
                  <md-input type="number" id="grossIncome" name="grossIncome" min="1000" step="1" v-model="grossIncome" data-vv-name="grossIncome" v-validate="'required|min_value:1000'" data-vv-validate-on="change" required />
                  <span v-if="errorBag.has('grossIncome:required')" class="md-error">Enter your annual gross income</span>
                  <span v-else-if="errorBag.has('grossIncome:min_value')" class="md-error">Enter at least &pound;1,000</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12 col-md-6">
                <md-input-container>
                  <label for="multiplier">Multiplier (use the default if unsure)</label>
                  <md-select v-model="multiplier" id="multiplier" name="multiplier">
                    <md-option disabled>Select a multiplier</md-option>
                    <md-option v-for="multiplier in multipliers" :key="multiplier" v-bind:value="multiplier">{{ multiplier }}</md-option>
                  </md-select>
                </md-input-container>
              </div>
            </div>
            <div class="row mt-4" v-if="!calculateAmountCanBorrow">
              <div class="col-xs-12">
                <p class="md-warn">Please fix any validation errors to see your results</p>
              </div>
            </div>
            <div class="row mt-4" v-if="calculateAmountCanBorrow">
              <div class="col-xs-12">
                <p><strong>You could potentially borrow up to:</strong> £{{ calculateAmountCanBorrow }}.
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

      return Number.parseFloat(this.grossIncome * this.multiplier).formatWithSeparator()
    }
  }
}

</script>
