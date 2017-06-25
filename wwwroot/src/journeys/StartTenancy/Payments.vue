<template>
  <div>
    <h2 class="title">Now, how about payment?</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-text-field v-model="tenancy.rentalAmount"
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
        </v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <v-select :items="rentalFrequencies"
                  :value="tenancy.rentalFrequency"
                  :rules="[$validation.rules.required]"
                  @input="updateRentalFrequency"
                  item-value="id"
                  label="How often will the rent be paid?"
                  dark single-line auto required>
         </v-select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-xs-12 col-md-6">
        <v-text-field v-model="tenancy.rentalPaymentReference"
                      :value="tenancy.rentalPaymentReference"
                      :rules="[$validation.rules.required, $validation.rules.min_length(tenancy.rentalPaymentReference, 2)]"
                      @input="updateRentalPaymentReference"
                      label="Payment reference"
                      required>
        </v-text-field>
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
        default: []
      }
    },
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    },
    computed: {
      ...mapState({
        tenancy: state => state.newTenancy.tenancy
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
