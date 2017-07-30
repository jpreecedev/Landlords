<template>
  <div>
    <h2 class="title">Now, how about payment?</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <text-field v-model="tenancy.rentalAmount"
                    :value="tenancy.rentalAmount"
                    :rules="[$validation.rules.required, $validation.rules.min_value(tenancy.rentalAmount, 1), $validation.rules.max_value(tenancy.rentalAmount, 50000)]"
                    @input="updateRentalAmount"
                    prefix="£"
                    label="How much is the rent?"
                    type="number"
                    step="1"
                    min="1"
                    max="50000"
                    required>
        </text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <select-list :items="rentalFrequencies"
                     :value="tenancy.rentalFrequency"
                     :rules="[$validation.rules.required]"
                     @input="updateRentalFrequency"
                     label="How often will the rent be paid?"
                     required>
         </select-list>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-xs-12 col-md-6">
        <text-field v-model="tenancy.rentalPaymentReference"
                    :value="tenancy.rentalPaymentReference"
                    :rules="[$validation.rules.required, $validation.rules.min_length(tenancy.rentalPaymentReference, 2)]"
                    @input="updateRentalPaymentReference"
                    label="Payment reference"
                    required>
        </text-field>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'payments',
  props: {
    rentalFrequencies: {
      type: Array,
      default: () => []
    }
  },
  computed: {
    ...mapState({
      tenancy: state => state.newTenancy.tenancy,
      permissions: state => state.permissions
    })
  },
  methods: {
    updateRentalAmount (rentalAmount) {
      this.$store.commit('TENANCY_UPDATE_RENTAL_AMOUNT', rentalAmount)
    },
    updateRentalFrequency (rentalFrequency) {
      this.$store.commit('TENANCY_UPDATE_RENTAL_FREQUENCY', rentalFrequency)
    },
    updateRentalPaymentReference (rentalPaymentReference) {
      this.$store.commit('TENANCY_UPDATE_RENTAL_PAYMENT_REFERENCE', rentalPaymentReference)
    }
  }
}
</script>
