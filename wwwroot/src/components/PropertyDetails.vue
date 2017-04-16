<template>
  <div>
    <h2>Property Details</h2>
    <div class="row">
      <div class="col">
        <div class="form-group row">
          <label class="col-12 col-form-label" for="reference">Property Reference</label>
          <div class="col-12">
            <input data-id="propertyDetails.reference" v-model="propertyDetails.reference" class="form-control" id="reference" name="reference" type="text" required>
          </div>
        </div>
        <div class="form-group row">
          <label class="col-12 col-form-label" for="propertyType">Property Type</label>
          <div class="col-12">
            <select data-id="propertyDetails.propertyType" v-model="propertyDetails.propertyType" class="form-control" id="propertyType" name="propertyType" required>
              <option disabled value="">Select a property type</option>
              <option v-for="propertyType in propertyTypes" v-bind:value="propertyType.split(' ').join('')">{{ propertyType }}</option>
            </select>
          </div>
        </div>
        <div class="form-group row">
          <label class="col-12 col-form-label" for="furnishing">Furnishing</label>
          <div class="col-12">
            <select data-id="propertyDetails.furnishing" v-model="propertyDetails.furnishing" class="form-control" id="furnishing" name="furnishing" required>
              <option disabled value="">Select a furnishing type</option>
              <option v-for="furnishing in furnishings" v-bind:value="furnishing.split(' ').join('')">{{ furnishing }}</option>
            </select>
          </div>
        </div>
        <div class="form-group row">
          <label class="col-12 col-form-label" for="constructionDate">Construction Date</label>          
          <div class="col-12">
            <datepicker v-model="propertyDetails.constructionDate" id="constructionDate" name="constructionDate" placeholder="Select date..." input-class="form-control"></datepicker>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card">
          <div class="card-block">
            <h4 class="card-title">Projected Rent</h4>
            <h6 class="card-subtitle mb-2 text-muted">Enter expected rents for this property.</h6>
            <div class="form-group row">
              <div class="col">
                <label class="col-form-label" for="targetRent">Target Rent</label>
                <div>
                  <input data-id="propertyDetails.targetRent" v-model="propertyDetails.targetRent" class="form-control" id="targetRent" name="targetRent" type="number" required>
                </div>
              </div>
              <div class="col">
                <label class="col-form-label" for="paymentTerm">Payment Term</label>
                <div>
                  <select data-id="propertyDetails.paymentTerm" v-model="propertyDetails.paymentTerm" class="form-control" id="paymentTerm" name="paymentTerm" required>
                    <option disabled value="">Select a payment term</option>
                    <option v-for="paymentTerm in paymentTerms" v-bind:value="paymentTerm.split(' ').join('')">{{ paymentTerm }}</option>
                  </select>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="card mt-3">
          <div class="card-block">
            <h4 class="card-title">Purchase Details</h4>
            <div class="form-group row">
              <div class="col">
                <label class="col-form-label" for="purchaseDate">Purchase Date</label>
                <div>
                  <datepicker v-model="propertyDetails.purchaseDate" id="purchaseDate" name="purchaseDate" placeholder="Select date..." input-class="form-control"></datepicker>
                </div>
              </div>
              <div class="col">
                <label class="col-form-label" for="purchasePrice">Purchase Price</label>
                <div>
                  <input data-id="propertyDetails.purchasePrice" v-model="propertyDetails.purchasePrice" class="form-control" id="purchasePrice" name="purchasePrice" type="number" required>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <button @click="save()" class="btn btn-primary">Save</button>
      </div>
    </div>
  </div>
</template>

<script>
import Datepicker from 'vuejs-datepicker'

export default {
  name: 'propertyDetails',
  components: { Datepicker },
  data () {
    return {
      propertyTypes: [ 'Detached', 'Bungalow', 'Semi-detached' ],
      paymentTerms: ['Monthly', 'Weekly', 'Annually'],
      furnishings: [ 'Fully', 'Part', 'None' ],
      propertyDetails: {
        reference: '',
        propertyType: '',
        furnishing: '',
        constructionDate: '',
        targetRent: '',
        paymentTerm: '',
        purchaseDate: '',
        purchasePrice: '',
        sellingDate: '',
        sellingPrice: '',
        isAvailableForLetting: true
      }
    }
  },
  methods: {
    save () {
      this.$http.post('/api/propertyDetails', { ...this.propertyDetails })
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
