<template>
  <main>
    <h1 class="md-display-2">Property Details</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-alert error :value="errorBag.any()">
      <span v-if="!errorBag.has('GenericError')">Please fix validation errors and try and submit the form again</span>
      <span v-else-if="errorBag.has('GenericError')">{{ errorBag.first('GenericError') }}</span>
    </v-alert>
    <form @submit.prevent="validateBeforeSubmit" role="form" enctype="multipart/form-data" novalidate>
      <fieldset v-bind:disabled="!permissions.PD_Update">
        <div class="property-image-container" v-if="(permissions.PI_Upload) || (!permissions.PI_Upload && propertyDetails.propertyImages.length)">
          <div v-if="permissions.PI_Upload" class="property-image">
            <label>
              <input type="file" accept="image/x-png,image/gif,image/jpeg" multiple @change="filesChange($event.target.name, $event.target.files)" name="files">
              <img class="placeholder" src="../../assets/images/placeholder.png" alt="Add more images...">
            </label>
          </div>
          <div class="property-image" v-for="propertyImage in propertyDetails.propertyImages">
            <img class="img-thumbnail" v-if="propertyImage.fileName" v-bind:src="'/static/uploads/' + propertyDetails.portfolioId + '/' + propertyImage.fileName" v-bind:alt="propertyImage.fileName" v-bind:title="propertyImage.fileName">
            <div v-if="permissions.PI_Delete" class="overlay">
              <v-btn warning type="button" @click.native="deleteImage(propertyImage)">Delete</v-btn>
            </div>
          </div>
          <div class="row mt-4" v-if="isUploading">
            <div class="col-xs-12">
              <div class="progress">
                <div class="progress-bar" role="progressbar" v-bind:aria-valuenow="progress" aria-valuemin="0" aria-valuemax="100" v-bind:style="{ width: progress + '%', height: '20px' }">{{ progress }}%</div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-4">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-title class="primary white--text">
                Overview
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('reference') }">
                    <label for="reference">Property Reference</label>
                    <md-input v-model="propertyDetails.reference" id="reference" name="reference" data-vv-name="reference" v-validate="'required'" data-vv-validate-on="change" required />
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyType') }">
                    <label for="propertyType">Property Type</label>
                    <md-select v-model="propertyDetails.propertyType" data-vv-name="propertyType" v-validate="'required'" data-vv-validate-on="change" id="propertyType" name="propertyType" required>
                      <md-option disabled value="">Select a property type</md-option>
                      <md-option v-for="propertyType in propertyTypes" v-bind:value="propertyType" :key="propertyType">{{ propertyType }}</md-option>
                    </md-select>
                    <span v-if="errorBag.has('propertyType')" class="md-error">Select a valid property type</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('bedrooms') }">
                    <label for="bedrooms">Number of bedrooms</label>
                    <md-input type="number" v-model="propertyDetails.bedrooms" id="bedrooms" name="bedrooms" />
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('furnishing') }">
                    <label for="furnishing">Furnishing</label>
                    <md-select v-model="propertyDetails.furnishing" data-vv-name="furnishing" v-validate="'required'" data-vv-validate-on="change" id="furnishing" name="furnishing" required>
                      <md-option disabled value="">Select a furnishing type</md-option>
                      <md-option v-for="furnishing in furnishings" v-bind:value="furnishing" :key="furnishing">{{ furnishing }}</md-option>
                    </md-select>
                    <span v-if="errorBag.has('furnishing')" class="md-error">Select a valid furnishing</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container>
                    <label for="constructionDate">Construction Date</label>
                    <md-input v-model="propertyDetails.constructionDate" id="constructionDate" name="constructionDate" type="date" />
                  </md-input-container>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Address
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyStreetAddress') }">
                    <label for="propertyStreetAddress">Street address</label>
                    <md-textarea v-model="propertyDetails.propertyStreetAddress" data-vv-name="propertyStreetAddress" v-validate="'required'" data-vv-validate-on="change"  id="propertyStreetAddress" rows="3" name="propertyStreetAddress" required />
                    <span v-if="errorBag.has('propertyStreetAddress')" class="md-error">Enter a valid street address</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyTownOrCity') }">
                    <label for="propertyTownOrCity">Town or City</label>
                    <md-input v-model="propertyDetails.propertyTownOrCity" data-vv-name="propertyTownOrCity" v-validate="'required'" data-vv-validate-on="change"  id="propertyTownOrCity" name="propertyTownOrCity" required />
                    <span v-if="errorBag.has('propertyTownOrCity')" class="md-error">Enter a valid town or city</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyCountyOrRegion') }">
                    <label for="propertyCountyOrRegion">County or Region</label>
                    <md-input v-model="propertyDetails.propertyCountyOrRegion" data-vv-name="propertyCountyOrRegion" v-validate="'required'" data-vv-validate-on="change"  id="propertyCountyOrRegion" name="propertyCountyOrRegion" required />
                    <span v-if="errorBag.has('propertyCountyOrRegion')" class="md-error">Enter a valid town or city</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyPostcode') }">
                    <label for="propertyPostcode">Postcode</label>
                    <md-input v-model="propertyDetails.propertyPostcode" data-vv-name="propertyPostcode" v-validate="'required'" data-vv-validate-on="change"  id="propertyPostcode" name="propertyPostcode" required />
                    <span v-if="errorBag.has('propertyPostcode')" class="md-error">Enter a valid postal code</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-input-container :class="{ 'md-input-invalid' : errorBag.has('propertyCountry') }">
                    <label for="propertyCountry">Country</label>
                    <md-select v-model="propertyDetails.propertyCountry" data-vv-name="propertyCountry" v-validate="'required'" data-vv-validate-on="change" id="propertyCountry" name="propertyCountry" required>
                      <md-option disabled value="">Select a Country</md-option>
                      <md-option v-for="propertyCountry in countries" v-bind:value="propertyCountry" :key="propertyCountry">{{ propertyCountry }}</md-option>
                    </md-select>
                    <span v-if="errorBag.has('propertyCountry')" class="md-error">Select a valid country</span>
                  </md-input-container>
                </v-card-row>
                <v-card-row>
                  <md-checkbox v-model="propertyDetails.isAvailableForLetting" id="isAvailableForLetting" name="isAvailableForLetting">
                  This property is available for letting
                  </md-checkbox>
                </v-card-row>
              </v-card-text>
            </v-card>
          </div>
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-title class="primary white--text">
                Ownership
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="purchaseDate">Purchase Date</label>
                      <md-input v-model="propertyDetails.purchaseDate" id="purchaseDate" name="purchaseDate" type="date" />
                    </md-input-container>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="purchasePrice">Purchase Price</label>
                      <md-input v-model="propertyDetails.purchasePrice" type="number" id="purchasePrice" name="purchasePrice"></md-input>
                    </md-input-container>
                  </div>
                </v-card-row>
                <v-card-row>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="mortgageAmount">Mortgage Amount</label>
                      <md-input v-model="propertyDetails.mortgageAmount" id="mortgageAmount" name="mortgageAmount" type="number" />
                    </md-input-container>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="interestRate">Interest Rate</label>
                      <md-input v-model="propertyDetails.interestRate" id="interestRate" name="interestRate" type="number" />
                    </md-input-container>
                  </div>
                </v-card-row>
                <v-card-row>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="monthlyPayment">Monthly Repayment</label>
                      <md-input v-model="propertyDetails.monthlyPayment" id="monthlyPayment" name="monthlyPayment" type="number" />
                    </md-input-container>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="mortgageProvider">Mortgage Provider</label>
                      <md-select v-model="propertyDetails.mortgageProvider" id="mortgageProvider" name="mortgageProvider">
                        <md-option disabled value="">Select a mortgage provider</md-option>
                        <md-option v-for="mortgageProvider in mortgageProviders" v-bind:value="mortgageProvider" :key="mortgageProvider">{{ mortgageProvider }}</md-option>
                      </md-select>
                    </md-input-container>
                  </div>
                </v-card-row>
                <v-card-row>
                  <md-input-container>
                    <label for="currentDealExpirationDate">Current deal expiration date</label>
                    <md-input v-model="propertyDetails.currentDealExpirationDate" id="currentDealExpirationDate" name="currentDealExpirationDate" type="date" />
                  </md-input-container>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Projected Rent
              </v-card-title>
              <v-card-text>
                <v-card-row class="row">
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="targetRent">Target Rent</label>
                      <md-input v-model="propertyDetails.targetRent" id="targetRent" name="targetRent" type="number" />
                    </md-input-container>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="paymentTerm">Payment Term</label>
                      <md-select v-model="propertyDetails.paymentTerm" id="paymentTerm" name="paymentTerm">
                        <md-option disabled value="">Select a payment term</md-option>
                        <md-option v-for="paymentTerm in paymentTerms" v-bind:value="paymentTerm" :key="paymentTerm">{{ paymentTerm }}</md-option>
                      </md-select>
                    </md-input-container>
                  </div>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Selling Details
              </v-card-title>
              <v-card-text>
                <v-card-row class="row">
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="sellingDate">Selling Date</label>
                      <md-input v-model="propertyDetails.sellingDate" id="sellingDate" name="sellingDate" type="date" />
                    </md-input-container>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <md-input-container>
                      <label for="sellingPrice">Selling Price</label>
                      <md-input v-model="propertyDetails.sellingPrice" id="sellingPrice" name="sellingPrice" type="number" />
                    </md-input-container>
                  </div>
                </v-card-row>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary light v-if="permissions.PD_Update" type="submit">Save</v-btn>
            <v-btn flat v-if="permissions.PD_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
import { ErrorBag } from 'vee-validate'
import utils from 'utils'
import FileUploadService from 'services/file-upload.service'

export default {
  name: 'propertyDetails',
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

      FileUploadService.upload(formData, `/api/propertyimage/upload?entityId=${this.$route.params.propertyId}`, uploadProgress => { this.progress = uploadProgress })
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

  .property-image-container {

    padding: 0 16px 16px 16px;
    background-color: #fafafa;

    .property-image {
      position: relative;
      display: inline-block;
      margin: 16px 16px 0 0;
      vertical-align: top;
      label {
        input[type="file"] {
          position: fixed;
          top: -1000px;
        }
      }
      img {
        max-width: 200px;
        max-height: 150px;
        height: auto;
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 5px;
        &.placeholder {
          cursor: pointer;
        }
        &:hover {
          box-shadow: 0 0 2px 1px rgba(0, 140, 186, 0.5);
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
