<template>
  <div>
    <h2 class="title">Now, how about payment?</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-text-field v-model="rentalAmount"
                      :value="rentalAmount"
                      @input="updateRentalAmount"
                      prefix="£"
                      label="How much is the rent?"
                      required>
        </v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <v-select :items="rentalFrequencies"
                  :value="selectedRentalFrequency"
                  @input="updateRentalFrequency"
                  item-value="id"
                  label="How often will the rent be paid?"
                  dark single-line auto required>
         </v-select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-xs-12 col-md-6">
        <v-text-field v-model="rentalPaymentReference"
                      :value="rentalPaymentReference"
                      @input="updateRentalPaymentReference"
                      label="What reference should the tenant use when paying?"
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
    computed: mapState({
      rentalAmount: state => state.newTenancy.tenancy.rentalAmount,
      selectedRentalFrequency: state => state.newTenancy.tenancy.rentalFrequency,
      rentalPaymentReference: state => state.newTenancy.tenancy.rentalPaymentReference
    }),
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
