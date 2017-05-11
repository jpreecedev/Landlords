<template>
  <main class="container">
    <div> 
      <h1>Property Details</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>    
    <form @submit.prevent="validateBeforeSubmit" role="form" enctype="multipart/form-data" novalidate>
      <fieldset v-bind:disabled="!permissions.PD_Update">
        <div class="property-image-container">
          <div v-if="permissions.PI_Upload" class="property-image">
            <label>
              <input type="file" accept="image/x-png,image/gif,image/jpeg" multiple @change="filesChange($event.target.name, $event.target.files)" name="files">
              <img class="placeholder" src="../assets/images/placeholder.png" alt="Add more images...">
            </label>
          </div>
          <div class="property-image" v-for="propertyImage in propertyDetails.propertyImages">
            <img class="img-thumbnail" v-if="propertyImage.fileName" v-bind:src="'/static/uploads/' + propertyDetails.portfolioId + '/' + propertyImage.fileName" v-bind:alt="propertyImage.fileName" v-bind:title="propertyImage.fileName">
            <div v-if="permissions.PI_Delete" class="overlay">
              <button type="button" class="btn btn-danger pointer" @click="deleteImage(propertyImage)">Delete</button>
            </div>
          </div>
          <div class="row mt-4" v-if="isUploading">
            <div class="col">
              <div class="progress">
                <div class="progress-bar" role="progressbar" v-bind:aria-valuenow="progress" aria-valuemin="0" aria-valuemax="100" v-bind:style="{ width: progress + '%', height: '20px' }">{{ progress }}%</div>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col">
            <div class="card">
              <div class="card-block">
                <h3 class="card-title">Overview</h3>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="reference">Property Reference</label>
                  <div class="col-12">
                    <input v-model="propertyDetails.reference" class="form-control" id="reference" name="reference" type="text" required>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('propertyType') }">
                  <label class="col-12 col-form-label" for="propertyType">Property Type</label>
                  <div class="col-12">
                    <select v-model="propertyDetails.propertyType" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="propertyType" name="propertyType"  required>
                      <option disabled value="">Select a property type</option>
                      <option v-for="propertyType in propertyTypes" v-bind:value="propertyType">{{ propertyType }}</option>
                    </select>
                    <span v-show="errors.has('propertyType')" v-bind:title="errors.first('propertyType')" class="form-control-feedback">Select a valid property type</span>
                  </div>
                </div>
                <div class="form-group row">
                  <div class="col" :class="{ 'has-danger': errors.has('bedrooms') }">
                    <label class="col-form-label" for="bedrooms">Number of bedrooms</label>
                    <input type="number" class="form-control" v-model="propertyDetails.bedrooms" v-validate="'required'" id="bedrooms" name="bedrooms" data-vv-validate-on="blur" placeholder="Bedrooms">
                  </div>
                  <div class="col" :class="{ 'has-danger': errors.has('furnishing') }">
                    <label class="col-form-label" for="furnishing">Furnishing</label>
                    <select v-model="propertyDetails.furnishing" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="furnishing" name="furnishing" required>
                      <option disabled value="">Select a furnishing type</option>
                      <option v-for="furnishing in furnishings" v-bind:value="furnishing">{{ furnishing }}</option>
                    </select>
                    <span v-show="errors.has('furnishing')" v-bind:title="errors.first('furnishing')" class="form-control-feedback">Select a valid furnishing</span>
                  </div>                
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="constructionDate">Construction Date</label>          
                  <div class="col-12">
                    <datepicker v-model="propertyDetails.constructionDate" id="constructionDate" name="constructionDate" placeholder="Select date..." input-class="form-control"></datepicker>
                  </div>
                </div>
              </div>
            </div>

            <div class="card mt-3">
              <div class="card-block">
                <h3 class="card-title">Address</h3>
                <div class="form-group row" :class="{ 'has-danger': errors.has('propertyStreetAddress') }">
                  <label class="col-12 col-form-label" for="propertyStreetAddress">Street address</label>
                  <div class="col-12">
                    <textarea v-model="propertyDetails.propertyStreetAddress" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="propertyStreetAddress" name="propertyStreetAddress" required>
                    </textarea>
                    <span v-show="errors.has('propertyStreetAddress')" v-bind:title="errors.first('propertyStreetAddress')" class="form-control-feedback">Enter a valid street address</span>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="propertyTownOrCity">Town or City</label>
                  <div class="col-12">
                    <input v-model="propertyDetails.propertyTownOrCity" class="form-control" id="propertyTownOrCity" name="propertyTownOrCity" type="text" required>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="propertyCountyOrRegion">County or Region</label>
                  <div class="col-12">
                    <input v-model="propertyDetails.propertyCountyOrRegion" class="form-control" id="propertyCountyOrRegion" name="propertyCountyOrRegion" type="text" required>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="propertyPostcode">Postcode</label>          
                  <div class="col-12">
                    <input v-model="propertyDetails.propertyPostcode" class="form-control" id="propertyPostcode" name="propertyPostcode" type="text" required>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('propertyCountry') }">
                  <label class="col-12 col-form-label" for="propertyCountry">Country</label>          
                  <div class="col-12">
                    <select v-model="propertyDetails.propertyCountry" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="propertyCountry" name="propertyCountry" required>
                      <option disabled value="">Select a Country</option>
                      <option v-for="propertyCountry in countries" v-bind:value="propertyCountry">{{ propertyCountry }}</option>
                    </select>
                    <span v-show="errors.has('propertyCountry')" v-bind:title="errors.first('propertyCountry')" class="form-control-feedback">Select a valid country</span>
                  </div>
                </div>
                <div class="form-group row mt-4">
                  <div class="form-check col">
                    <label class="form-check-label">
                      <input type="checkbox" class="form-check-input" v-model="propertyDetails.isAvailableForLetting" id="isAvailableForLetting" name="isAvailableForLetting">
                        This property is available for letting
                    </label>
                  </div>
                </div>
              </div>
            </div>          
          </div>
          <div class="col">
            <div class="card">
              <div class="card-block">
                <h4 class="card-title">Ownership</h4>
                <div class="form-group row">
                  <div class="col">
                    <label class="col-form-label" for="purchaseDate">Purchase Date</label>
                    <datepicker v-model="propertyDetails.purchaseDate" id="purchaseDate" name="purchaseDate" placeholder="Select date..." input-class="form-control"></datepicker>
                  </div>
                  <div class="col">
                    <label class="col-form-label" for="purchasePrice">Purchase Price</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input v-model="propertyDetails.purchasePrice" class="form-control" id="purchasePrice" name="purchasePrice" type="number">                    
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                <div class="form-group row">
                  <div class="col">
                    <label class="col-form-label" for="mortgageAmount">Mortgage Amount</label>                    
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input v-model="propertyDetails.mortgageAmount" class="form-control" id="mortgageAmount" name="mortgageAmount" type="number">                    
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                  <div class="col">
                      <label class="col-form-label" for="interestRate">Interest Rate</label>
                      <div class="input-group">
                        <input v-model="propertyDetails.interestRate" class="form-control" id="interestRate" name="interestRate" type="number">
                        <span class="input-group-addon">%</span>
                      </div>
                  </div>
                </div>
                <div class="form-group row">
                  <div class="col-6">
                    <label class="col-form-label" for="monthlyPayment">Monthly Repayment</label>                    
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input v-model="propertyDetails.monthlyPayment" class="form-control" id="monthlyPayment" name="monthlyPayment" type="number">                    
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                  <div class="col-6">
                    <label class="col-form-label" for="mortgageProvider">Mortgage Provider</label>
                    <select v-model="propertyDetails.mortgageProvider" class="form-control" id="mortgageProvider" name="mortgageProvider">
                      <option disabled value="">Select a mortgage provider</option>
                      <option v-for="mortgageProvider in mortgageProviders" v-bind:value="mortgageProvider">{{ mortgageProvider }}</option>
                    </select>
                  </div>
                </div>
                <div class="form-group row">
                  <div class="col">
                    <label class="col-form-label" for="currentDealExpirationDate">Current deal expiration date</label>
                    <p class="mb-2 text-muted">Tell us the date when your current mortgage deal/product expires</p>
                    <datepicker v-model="propertyDetails.currentDealExpirationDate" id="currentDealExpirationDate" name="currentDealExpirationDate" placeholder="Select date..." input-class="form-control"></datepicker>
                  </div>
                </div>
              </div>
            </div>
            <div class="card mt-3">
              <div class="card-block">
                <h4 class="card-title">Projected Rent</h4>
                <h6 class="card-subtitle mb-2 text-muted">Enter expected rents for this property.</h6>
                <div class="form-group row">
                  <div class="col">
                    <label class="col-form-label" for="targetRent">Target Rent</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input v-model="propertyDetails.targetRent" class="form-control" id="targetRent" name="targetRent" type="number" required>
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                  <div class="col">
                    <label class="col-form-label" for="paymentTerm">Payment Term</label>
                    <select v-model="propertyDetails.paymentTerm" class="form-control" id="paymentTerm" name="paymentTerm" required>
                      <option disabled value="">Select a payment term</option>
                      <option v-for="paymentTerm in paymentTerms" v-bind:value="paymentTerm">{{ paymentTerm }}</option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
            <div class="card mt-3">
              <div class="card-block">
                <h4 class="card-title">Selling Details</h4>
                <h6 class="card-subtitle mb-2 text-muted">Tell us the actual or expected selling price.</h6>
                <div class="form-group row">
                  <div class="col">
                    <label class="col-form-label" for="sellingDate">Selling Date</label>
                    <datepicker v-model="propertyDetails.sellingDate" id="sellingDate" name="sellingDate" placeholder="Select date..." input-class="form-control"></datepicker>
                  </div>
                  <div class="col">
                    <label class="col-form-label" for="sellingPrice">Selling Price</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input v-model="propertyDetails.sellingPrice" class="form-control" id="sellingPrice" name="sellingPrice" type="number" required>
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col">
            <button v-if="permissions.PD_Update" type="submit" class="btn btn-primary">Save</button>
            <input v-if="permissions.PD_Update" type="reset" class="btn btn-secondary" value="Reset" />
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
import Datepicker from 'vuejs-datepicker'
import { ErrorBag } from 'vee-validate'
import utils from '@/utils'
import fileUpload from '@/file-upload.service'

export default {
  name: 'propertyDetails',
  components: { Datepicker },
  data () {
    return {
      permissions: this.$store.state.permissions,
      isUploading: false,
      progress: 0,
      propertyTypes: [],
      paymentTerms: [],
      furnishings: [],
      countries: [],
      mortgageProviders: [],
      propertyDetails: {
        bedrooms: 0,
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
        isAvailableForLetting: true,
        propertyStreetAddress: '',
        propertyTownOrCity: '',
        propertyCountyOrRegion: '',
        propertyPostcode: '',
        propertyCountry: '',
        propertyImages: [],
        userId: '',
        mortgageAmount: 0,
        interestRate: 0,
        mortgageProvider: '',
        currentDealExpirationDate: '',
        monthlyPayment: 0,
        portfolioId: ''
      }
    }
  },
  created () {
    this.$http.get(`/api/propertydetails/${this.$route.params.propertyId ? this.$route.params.propertyId : 'viewdata'}`).then(response => {
      Object.assign(this, utils.mapEntity(response.data, 'propertyDetails', !this.$route.params.propertyId))
    })
  },
  methods: {
    validateBeforeSubmit: function () {
      this.$validator.validateAll().then(() => {
        var bag = new ErrorBag()
        this.$http.post(`/api/propertyDetails?entityId=${this.propertyDetails.id}`, { ...this.propertyDetails })
          .then(() => {
            this.$router.push('/manager/property-list')
          })
          .catch(response => {
            var validationResult = utils.getFormValidationErrors(response)
            validationResult.errors.forEach(validationError => {
              bag.add(validationError.key, validationError.messages[0], 'required')
            })
            if (validationResult.status) {
              bag.add('GenericError', validationResult.status, 'generic')
            }
          })
        this.$validator.errorBag = bag
      }).catch(() => {
        window.scrollTo(0, 0)
      })
    },
    filesChange: function (fieldName, fileList) {
      const formData = new FormData()
      if (!fileList.length) return

      this.isUploading = true

      Array.from(Array(fileList.length).keys()).map(x => {
        formData.append(fieldName, fileList[x], fileList[x].name)
      })

      fileUpload.upload(formData, `/api/propertyimage/upload?entityId=${this.$route.params.propertyId}`, uploadProgress => { this.progress = uploadProgress })
        .then(images => {
          if (images) {
            images.forEach(image => {
              this.propertyDetails.propertyImages.push(image)
            })
          }
        })
        .finally(() => {
          this.isUploading = false
        })
    },
    deleteImage: function (propertyImage) {
      if (confirm('Are you sure you want to delete this image?')) {
        this.$http.delete(`/api/propertyimage?entityId=${propertyImage.id}`)
          .then(() => {
            this.propertyDetails.propertyImages.splice(this.propertyDetails.propertyImages.indexOf(propertyImage), 1)
          })
          .catch(() => {
            alert('Unable to delete image at this time')
          })
      }
    }
  },
  watch: {
    '$route' (to, from) {}
  }
}
</script>

<style lang="scss" scoped>

  @import '../assets/styles/app';

  .property-image-container {

    background-color: $gray-lighter;
    margin-bottom: 2rem;
    overflow-x: scroll;
    overflow-y: hidden;
    min-height: 275px;
    white-space: nowrap;

    @include media-breakpoint-up(lg) {
      padding: remc(24px) remc(16px);      
    }

    .property-image {
      position: relative;
      display: inline-block;
      margin-right: remc(16px);
      label {
        input[type="file"] {
          position: fixed;
          top: -1000px;
        }
      }
      img {
        max-width: 200px;
        max-height: 200px;
        height: auto;
        &:hover {
          background-color: $gray-lightest;
        }
        &.placeholder {
          border: 3px dashed $gray-light; 
          border-radius: 30px;
          cursor: pointer;
        }
      }
      .img-thumbnail {
        opacity: 1;
        transition: .5s ease;
        backface-visibility: hidden;
      }

      .overlay {
        transition: .5s ease;
        opacity: 0;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        -ms-transform: translate(-50%, -50%)
      }

      &:hover .img-thumbnail {
        opacity: 0.3;
      }

      &:hover .overlay {
        opacity: 1;
      }
    }
  }

</style>
