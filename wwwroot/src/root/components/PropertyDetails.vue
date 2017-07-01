<template>
  <div>
    <h1 class="display-2">Property Details</h1>
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
              <div class="thumbnail">
                <img class="placeholder" src="../../assets/images/placeholder.png" alt="Add more images...">
              </div>
            </label>
          </div>
          <div class="property-image" v-for="propertyImage in propertyDetails.propertyImages">
            <div class="thumbnail">
              <img v-if="propertyImage.fileName" v-bind:src="'/static/uploads/' + propertyDetails.portfolioId + '/' + propertyImage.fileName" v-bind:alt="propertyImage.fileName" v-bind:title="propertyImage.fileName">
              <div v-if="permissions.PI_Delete" class="overlay">
                <v-btn warning type="button" @click.native="deleteImage(propertyImage)">Delete</v-btn>
              </div>
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
                  <v-text-field v-model="propertyDetails.reference"
                                :rules="[$validation.rules.required, $validation.rules.min_length(propertyDetails.reference, 2), $validation.rules.max_length(propertyDetails.reference, 255)]"
                                label="Property reference"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-select :items="propertyTypes"
                            :rules="[$validation.rules.required]"
                            v-model="propertyDetails.propertyType"
                            label="Property type"
                            dark>
                  </v-select>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="propertyDetails.bedrooms"
                                label="Number of bedrooms"
                                type="number">
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-select :items="furnishings"
                            :rules="[$validation.rules.required]"
                            v-model="propertyDetails.furnishing"
                            label="Furnishings"
                            dark>
                  </v-select>
                </v-card-row>
                <v-card-row>
                  <v-menu lazy :nudge-left="100">
                    <v-text-field slot="activator"
                                  label="Construction date"
                                  v-model="propertyDetails.constructionDate"
                                  prepend-icon="date_range"
                                  required readonly>
                    </v-text-field>
                    <v-date-picker v-model="propertyDetails.constructionDate"
                                    scrollable>
                    </v-date-picker>
                  </v-menu>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Address
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <v-text-field v-model="propertyDetails.propertyStreetAddress"
                                :multi-line="true"
                                :rows="3"
                                :auto-grow="true"
                                :rules="[$validation.rules.required, $validation.rules.min_length(propertyDetails.propertyStreetAddress, 2), $validation.rules.max_length(propertyDetails.propertyStreetAddress, 255)]"
                                label="Street address"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="propertyDetails.propertyTownOrCity"
                                :rules="[$validation.rules.required, $validation.rules.min_length(propertyDetails.propertyTownOrCity, 2), $validation.rules.max_length(propertyDetails.propertyTownOrCity, 255)]"
                                label="Town or city"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-select :items="counties"
                            v-model="propertyDetails.propertyCountyOrRegion"
                            label="County or region"
                            dark required>
                  </v-select>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="propertyDetails.propertyPostcode"
                                :rules="[$validation.rules.required, $validation.rules.min_length(propertyDetails.propertyPostcode, 5), $validation.rules.max_length(propertyDetails.propertyPostcode, 7)]"
                                label="Postcode"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-select :items="countries"
                            :rules="[$validation.rules.required]"
                            v-model="propertyDetails.propertyCountry"
                            label="Country"
                            dark required>
                  </v-select>
                </v-card-row>
                <v-card-row>
                <v-checkbox v-model="propertyDetails.isAvailableForLetting"
                            label="This property is available for letting">
                </v-checkbox>
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
                    <v-menu lazy :nudge-left="100">
                      <v-text-field slot="activator"
                                    label="Purchase date"
                                    v-model="propertyDetails.purchaseDate"
                                    prepend-icon="date_range"
                                    required readonly>
                      </v-text-field>
                      <v-date-picker v-model="propertyDetails.purchaseDate"
                                    scrollable>
                      </v-date-picker>
                    </v-menu>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.purchasePrice"
                                  label="Purchase price"
                                  prefix="£"
                                  type="number">
                    </v-text-field>
                  </div>
                </v-card-row>
                <v-card-row>
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.mortgageAmount"
                                  label="Mortgage amount"
                                  prefix="£"
                                  type="number">
                    </v-text-field>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.interestRate"
                                  label="Interest rate"
                                  suffix="%"
                                  type="number">
                    </v-text-field>
                  </div>
                </v-card-row>
                <v-card-row>
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.monthlyPayment"
                                  label="Monthly repayment"
                                  prefix="£"
                                  type="number">
                    </v-text-field>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-select :items="mortgageProviders"
                              v-model="propertyDetails.mortgageProvider"
                              label="Mortgage provider"
                              dark>
                    </v-select>
                  </div>
                </v-card-row>
                <v-card-row>
                  <v-menu lazy :nudge-left="100">
                    <v-text-field slot="activator"
                                  label="Current deal expiration date"
                                  v-model="propertyDetails.currentDealExpirationDate"
                                  prepend-icon="date_range"
                                  required readonly>
                    </v-text-field>
                    <v-date-picker v-model="propertyDetails.currentDealExpirationDate"
                                  scrollable>
                    </v-date-picker>
                  </v-menu>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Projected Rent
              </v-card-title>
              <v-card-text>
                <v-card-row class="row">
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.targetRent"
                                  label="Target rent"
                                  prefix="£"
                                  type="number">
                    </v-text-field>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-select :items="paymentTerms"
                              v-model="propertyDetails.paymentTerm"
                              label="Payment term"
                              dark>
                    </v-select>
                  </div>
                </v-card-row>
              </v-card-text>
              <v-card-title>
                Selling Details
              </v-card-title>
              <v-card-text>
                <v-card-row class="row">
                  <div class="col-xs-12 col-md-6">
                    <v-menu lazy :nudge-left="100">
                      <v-text-field slot="activator"
                                    label="Selling date"
                                    v-model="propertyDetails.sellingDate"
                                    prepend-icon="date_range"
                                    required readonly>
                      </v-text-field>
                      <v-date-picker v-model="propertyDetails.sellingDate"
                                    scrollable>
                      </v-date-picker>
                    </v-menu>
                  </div>
                  <div class="col-xs-12 col-md-6">
                    <v-text-field v-model="propertyDetails.sellingPrice"
                                  label="Selling price"
                                  prefix="£"
                                  type="number">
                    </v-text-field>
                  </div>
                </v-card-row>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary light v-if="permissions.PD_Update" type="submit" :disabled="isSaving">Save</v-btn>
            <v-btn flat v-if="permissions.PD_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </div>
</template>

<script>
import utils from 'utils'
import FileUploadService from 'services/file-upload.service'

export default {
  name: 'propertyDetails',
  data () {
    return {
      permissions: this.$store.state.permissions,
      isUploading: false,
      errors: [],
      isSaving: false,
      progress: 0,
      propertyTypes: [],
      paymentTerms: [],
      furnishings: [],
      counties: [],
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
      this.errors = []
      this.isSaving = true

      this.$http.post(`/api/propertyDetails?entityId=${this.propertyDetails.id}`, { ...this.propertyDetails })
        .then(() => {
          this.isSaving = false
          this.$router.push('/manager/property-list')
        })
        .catch(response => {
          var validationResult = utils.getFormValidationErrors(response)
          validationResult.errors.forEach(validationError => {
            this.errors.push({
              key: validationError.key,
              message: validationError.messages[0]
            })
          })
          if (validationResult.status) {
            this.errors.push({
              key: 'GenericError',
              message: validationResult.status
            })
          }
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
      margin: 16px 8px 0 0;
      vertical-align: top;
      label {
        input[type="file"] {
          position: fixed;
          top: -1000px;
        }
      }
    }
  }

</style>
