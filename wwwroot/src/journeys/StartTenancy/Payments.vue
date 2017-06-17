<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How much is the rent?</div>
        <v-text-field v-model="rentalAmount" prefix="£" id="rentalAmount" name="rentalAmount" hint="Rental Amount"></v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">How often will the rent be paid?</div>
        <md-input-container v-if="viewdata && viewdata.rentalFrequencies">
          <label for="rentalFrequencies">Rental payment frequency</label>
          <md-select v-model="selectedRentalFrequency" id="rentalFrequencies" name="rentalFrequencies">
            <md-option disabled>Select a payment frequency</md-option>
            <md-option v-for="rentalFrequencies in viewdata.rentalFrequencies" :key="rentalFrequencies" v-bind:value="rentalFrequencies">{{ rentalFrequencies }}</md-option>
          </md-select>
        </md-input-container>
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
