<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How much is the rent?</div>
        <v-text-field v-model="rentalAmount" prefix="£" id="rentalAmount" name="rentalAmount" hint="Rental Amount"></v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How often will the rent be paid?</div>
        <v-select v-if="viewdata && viewdata.rentalFrequencies" v-bind:items="viewdata.rentalFrequencies" v-model="selectedRentalFrequency" item-value="id" label="Select a rental frequency" dark single-line auto></v-select>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">What reference should the tenant use when paying?</div>
        <v-text-field v-model="paymentReference" id="paymentReference" name="paymentReference" hint="Rental Payment Reference"></v-text-field>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'payments',
    props: {
      viewdata: Object
    },
    data () {
      return {
        permissions: this.$store.state.permissions,
        selectedRentalFrequency: null,
        selectedRentalAmount: null,
        paymentReference: null,
        rentalAmount: null
      }
    },
    created () {
    },
    methods: {
    }
  }

</script>
