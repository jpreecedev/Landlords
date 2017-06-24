<template>
  <div>
    <h1 class="display-2">Rental Yield Calculator</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <form role="form" novalidate>
          <div class="row">
            <div class="col-xs-12">
              <v-text-field type="number"
                            v-model="rentalyield.purchasePrice"
                            :rules="errors.purchasePrice"
                            min="10000"
                            label="Total cost of property"
                            required />
              <!--<span v-if="errorBag.has('purchasePrice:required')" class="md-error">Enter the amount paid for the property</span>
              <span v-else-if="errorBag.has('purchasePrice:min_value')" class="md-error">Enter at least &pound;10,000</span>-->
            </div>
          </div>
          <div class="row">
            <div class="col-xs-12">
                <v-text-field type="number"
                              min="1"
                              step="1"
                              v-model="rentalyield.rentalValue"
                              :rules="errors.rentalValue"
                              required />
                <!--<span v-if="errorBag.has('rentalValue:required')" class="md-error">Enter the expected rental income</span>
                <span v-else-if="errorBag.has('rentalValue:min_value')" class="md-error">Enter at least &pound;1</span>-->
              </md-input-container>
            </div>
          </div>
          <div class="row">
            <div class="col-xs-12">
              <p class="mb-0">Rental income period</p>
              <div class="row">
                <div class="col-xs-6">
                  <v-radio value="Monthly" v-model="rentalyield.frequency" label="Monthly" checked />
                </div>
                <div class="col-xs-6">
                  <v-radio value="Annual" v-model="rentalyield.frequency" label="Annual" />
                </div>
              </div>
            </div>
          </div>
          <div class="row mt-4" v-if="!calculateRentalYield">
            <div class="col-xs-12">
              <p class="md-warn">Please fix any validation errors to see your results</p>
            </div>
          </div>
          <div class="row mt-4" v-if="calculateRentalYield">
            <div class="col-xs-12">
              <p><strong>Gross rental yield:</strong> {{ calculateRentalYield }}%.<br/>
                <span v-if="calculateRentalYield >= 10">This is an EXCELLENT gross yield.</span>
                <span v-if="calculateRentalYield >= 7 && calculateRentalYield <= 9.9">This is a GOOD gross yield.</span>
                <span v-if="calculateRentalYield >= 5 && calculateRentalYield <= 6.9">This is an 'OK' gross yield.</span>
                <span v-if="calculateRentalYield < 5">The gross yield is LOW.</span>
              </p>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>

import { Validator } from 'vee-validate'

export default {
  name: 'rentalyield',
  created () {
    this.validator = new Validator(this.validationRules)
  },
  data () {
    return {
      validator: null,
      validationRules: {
        purchasePrice: 'required|min_value:10000',
        rentalValue: 'required|min_value:1'
      },
      rentalyield: {
        purchasePrice: 120000,
        rentalValue: 550,
        frequency: 'Monthly'
      }
    }
  },
  computed: {
    calculateRentalYield: function () {
      if (!this.rentalyield.rentalValue || !this.rentalyield.purchasePrice || this.errorBag.errors.length !== 0) {
        return false
      }

      var multiplier = this.rentalyield.frequency === 'Monthly' ? 12 : 1
      return Number.parseFloat(((this.rentalyield.rentalValue * multiplier) / this.rentalyield.purchasePrice) * 100).toFixed(2)
    },
    errors () {
      let errors = {}
      Object.keys(this.validationRules).forEach(key => {
        if (!errors[key]) {
          errors[key] = []
        }
        this.validator.validate(key, this[key]).catch(() => {})
      })

      this.validator.getErrors().errors.forEach(error => {
        errors[error.field].push(error.msg)
      })
      return errors
    }
  }
}

</script>
