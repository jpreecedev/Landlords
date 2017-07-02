<template>
  <div>
    <h1 class="display-2">How much can I borrow?</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div class="row">
      <div class="col-xs-12">
        <form role="form" novalidate>
          <div class="row">
            <div class="col-xs-12 col-md-6">
              <v-text-field v-model="grossIncome"
                            :rules="[$validation.rules.required, $validation.rules.min_value(grossIncome, 1000)]"
                            label="Your annual gross income"
                            type="number"
                            min="1000"
                            step="1"
                            prefix="£"
                            required>
              </v-text-field>
            </div>
          </div>
          <div class="row">
            <div class="col-xs-12 col-md-6">
              <v-select :items="multipliers"
                        v-model="multiplier"
                        label="Multiplier (use the default if unsure)"
                        dark>
              </v-select>
            </div>
          </div>
          <div class="row mt-4" v-if="!calculateAmountCanBorrow">
            <div class="col-xs-12">
              <p class="red--text">Please fix any validation errors to see your results</p>
            </div>
          </div>
          <div class="row mt-4" v-if="calculateAmountCanBorrow">
            <div class="col-xs-12">
              <p><strong>You could potentially borrow up to:</strong> £{{ calculateAmountCanBorrow }}.
              </p>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
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
