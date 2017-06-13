<template>
  <main>
    <h1 class="md-display-2">Tenant details</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div id="errorMessage" class="alert error" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset v-bind:disabled="!permissions.TE_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-header>
                <div class="md-title">Personal Details</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('firstName') }">
                      <label for="firstName">First name</label>
                      <md-input v-model="tenant.firstName" id="firstName" name="firstName" type="text" data-vv-name="firstName" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('firstName')" class="md-error">Enter a valid first name</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('middleName') }">
                      <label for="middleName">Middle name(s)</label>
                      <md-input v-model="tenant.middleName" id="middleName" name="middleName" type="text" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('lastName') }">
                      <label for="lastName">Last name</label>
                      <md-input v-model="tenant.lastName" id="lastName" name="lastName" type="text" data-vv-name="lastName" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('lastName')" class="md-error">Enter a valid last name</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('dateOfBirth') }">
                      <label for="dateOfBirth">Date of Birth</label>
                      <md-input v-model="tenant.dateOfBirth" id="dateOfBirth" name="dateOfBirth" type="date" />
                      <span v-if="errors.has('dateOfBirth')" class="md-error">Enter a date of birth</span>
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
            <md-card>
              <md-card-header>
                <div class="md-title">Contact Details</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid' : errors.has('mainContactNumber') }">
                      <label for="mainContactNumber">Main contact number</label>
                      <md-input v-model="tenant.mainContactNumber" data-vv-name="mainContactNumber" v-validate="'required'" data-vv-validate-on="change"  id="mainContactNumber" name="mainContactNumber" required />
                      <span v-if="errors.has('mainContactNumber')" class="md-error">Enter a valid contact number</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="secondaryContactNumber">Another contact number</label>
                      <md-input v-model="tenant.secondaryContactNumber" id="secondaryContactNumber" name="secondaryContactNumber" />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('emailAddress') }">
                      <label for="emailAddress">Email Address</label>
                      <md-input type="email" id="emailAddress" name="emailAddress" data-vv-name="emailAddress" v-validate="'required|email'" v-model="tenant.emailAddress" data-vv-validate-on="change" required />
                      <span v-if="errors.has('emailAddress')" class="md-error">Enter a valid email address</span>
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
          <div class="col-xs-12 col-md-6" v-if="tenant.addresses && tenant.addresses.length === 1">
            <md-card>
              <md-card-header>
                <div class="md-title">Addresses</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid' : errors.has('street') }">
                      <label for="street">Street address</label>
                      <md-textarea v-model="tenant.addresses[0].street" data-vv-name="street" v-validate="'required'" data-vv-validate-on="change"  id="street" name="street" required />
                      <span v-if="errors.has('street')" class="md-error">Enter a valid street address</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                  <md-input-container :class="{ 'md-input-invalid' : errors.has('townOrCity') }">
                    <label for="townOrCity">Town or City</label>
                    <md-input v-model="tenant.addresses[0].townOrCity" data-vv-name="townOrCity" v-validate="'required'" data-vv-validate-on="change"  id="townOrCity" name="townOrCity" required />
                    <span v-if="errors.has('townOrCity')" class="md-error">Enter a valid town or city</span>
                  </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                  <md-input-container :class="{ 'md-input-invalid' : errors.has('countyOrRegion') }">
                    <label for="countyOrRegion">County or Region</label>
                    <md-input v-model="tenant.addresses[0].countyOrRegion" data-vv-name="countyOrRegion" v-validate="'required'" data-vv-validate-on="change"  id="countyOrRegion" name="countyOrRegion" required />
                    <span v-if="errors.has('countyOrRegion')" class="md-error">Enter a valid county or region</span>
                  </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid' : errors.has('postcode') }">
                      <label for="postcode">Postcode</label>
                      <md-input v-model="tenant.addresses[0].postcode" data-vv-name="postcode" v-validate="'required'" data-vv-validate-on="change"  id="postcode" name="postcode" required />
                      <span v-if="errors.has('postcode')" class="md-error">Enter a valid postal code</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid' : errors.has('country') }">
                      <label for="country">Country</label>
                      <md-select v-model="tenant.addresses[0].country" data-vv-name="country" v-validate="'required'" data-vv-validate-on="change" id="country" name="country" required>
                        <md-option disabled value="">Select a Country</md-option>
                        <md-option v-for="country in countries" v-bind:value="country" :key="country">{{ country }}</md-option>
                      </md-select>
                      <span v-if="errors.has('country')" class="md-error">Select a valid country</span>
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary light v-if="permissions.TE_Update" type="submit">Save</v-btn>
            <v-btn flat v-if="permissions.TE_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
import { ErrorBag } from 'vee-validate'
import utils from 'utils'

export default {
  name: 'tenantsDetails',
  data () {
    return {
      permissions: this.$store.state.permissions,
      countries: [],
      tenant: {
        id: null,
        firstName: null,
        middleName: null,
        lastName: null,
        dateOfBirth: null,
        mainContactNumber: null,
        secondaryContactNumber: null,
        emailAddress: null,
        addresses: [{
          street: null,
          townOrCity: null,
          countyOrRegion: null,
          postcode: null,
          country: null
        }]
      }
    }
  },
  created () {
    this.$http.get(`/api/tenants/${this.$route.params.tenantId}`).then(response => {
      Object.assign(this, utils.mapEntity(response.data, 'tenant', false))
    })
  },
  methods: {
    validateBeforeSubmit: function () {
      this.$validator.validateAll().then(() => {
        var bag = new ErrorBag()
        this.$http.post(`/api/tenants/`, { ...this.tenant })
          .then(() => {
            this.$router.push({ name: 'tenants-overview' })
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
    }
  }
}

</script>

<style lang="scss" scoped>

</style>
