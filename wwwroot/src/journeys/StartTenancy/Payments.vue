<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How much is the rent?</div>
        <v-text-field v-model="rentalAmount"
                      :value="rentalAmount"
                      @input="updateRentalAmount"
                      prefix="£"
                      label="Rental Amount"
                      required>
        </v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How often will the rent be paid?</div>
        <v-select :items="rentalFrequencies"
                  :value="selectedRentalFrequency"
                  @input="updateRentalFrequency"
                  item-value="id"
                  label="Select a rental frequency"
                  dark single-line auto>
         </v-select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">What reference should the tenant use when paying?</div>
        <v-text-field v-model="rentalPaymentReference"
                      :value="rentalPaymentReference"
                      @input="updateRentalPaymentReference"
                      label="Rental Payment Reference"
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
