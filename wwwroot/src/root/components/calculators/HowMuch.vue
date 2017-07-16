<template>
  <div>
    <header>
      <h1 class="headline primary--text">How much can I borrow?</h1>
      <p class="display-2 grey--text text--darken-1">How much money, based on your grouss annual income, can you potentially borrow?</p>
      <p class="subheading">Complete this form to see your personalised results.</p>
    </header>
    <v-card>
      <v-card-text>
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
                            label="Multiplier (use the default if unsure)">
                  </v-select>
                </div>
              </div>
              <div class="row" v-if="!calculateAmountCanBorrow">
                <div class="col-xs-12">
                  <p class="red--text">Please fix any validation errors to see your results</p>
                </div>
              </div>
              <div class="row" v-if="calculateAmountCanBorrow">
                <div class="col-xs-12">
                  <p class="headline">Results</p>
                  <p><strong>You could potentially borrow up to:</strong> £{{ calculateAmountCanBorrow }}.
                  </p>
                </div>
              </div>
            </form>
          </div>
        </div>
      </v-card-text>
    </v-card>
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
    calculateAmountCanBorrow () {
      if (!this.grossIncome || !this.multiplier) {
        return false
      }

      return Number.parseFloat(this.grossIncome * this.multiplier).formatWithSeparator()
    }
  }
}
</script>
