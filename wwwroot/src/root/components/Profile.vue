<template>
  <main>
    <h1 class="md-display-2">Profile</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <div id="errorMessage" class="alert error" v-if="errors.any()">
      <span v-if="!errors.has('GenericError')">Please fix validation errors and try and submit the form again</span>
      <span v-if="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>
    <div class="alert success" v-if="saved">
      Your profile has been updated
    </div>
    <div class="alert success" v-if="resentVerification">
      We have sent you a new verification email.  Please check your inbox.
    </div>
    <div class="alert success" v-if="confirmed">
      Your email address has been verified
    </div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset v-bind:disabled="!permissions.P_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-header>
                <div class="md-title">Personal details</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('firstName') }">
                      <label for="firstName">First name</label>
                      <md-input v-model="profile.firstName" id="firstName" name="firstName" type="text" data-vv-name="firstName" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('firstName')" class="md-error">Enter a valid first name</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errors.has('lastName') }">
                      <label for="lastName">Last name</label>
                      <md-input v-model="profile.lastName" id="lastName" name="lastName" type="text" data-vv-name="lastName" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errors.has('lastName')" class="md-error">Enter a valid last name</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <label for="secondaryPhoneNumber">Email address</label>
                    <p>{{ profile.emailAddress }}</p>
                    <v-btn warning light v-if="!profile.emailConfirmed" @click.native="resendVerificationEmail()" type="button" class="ml-0">Re-send verification email</v-btn>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-header>
                <div class="md-title">Contact details</div>
              </md-card-header>
              <md-card-content>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="availableFrom">Available From</label>
                      <md-select v-model="availableFrom" id="availableFrom" name="availableFrom">
                        <md-option disabled value="">Select a time</md-option>
                        <md-option v-for="time in times" :key="time" :value="time.value">{{ time.display }}</md-option>
                      </md-select>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="availableTo">Available To</label>
                      <md-select v-model="availableTo" id="availableTo" name="availableTo">
                        <md-option disabled value="">Select a time</md-option>
                        <md-option v-for="time in times" :key="time" v-bind:value="time.value">{{ time.display }}</md-option>
                      </md-select>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="phoneNumber">Main Phone Number</label>
                      <md-input v-model="profile.phoneNumber" id="phoneNumber" name="phoneNumber" type="tel" required />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="secondaryPhoneNumber">Secondary Phone Number</label>
                      <md-input v-model="profile.secondaryPhoneNumber" id="secondaryPhoneNumber" name="secondaryPhoneNumber" type="tel" required />
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary light v-if="permissions.P_Update" type="submit">Save</v-btn>
            <v-btn flat v-if="permissions.P_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
  import utils from 'utils'
  import { ErrorBag } from 'vee-validate'

  export default {
    name: 'profile',
    data () {
      return {
        permissions: this.$store.state.permissions,
        times: [],
        saved: false,
        resentVerification: false,
        confirmed: this.$route.query.confirmed,
        availableFrom: '',
        availableTo: '',
        profile: {
          userId: '',
          firstName: '',
          lastName: '',
          availableFrom: '',
          availableTo: '',
          phoneNumber: '',
          secondaryPhoneNumber: '',
          emailAddress: '',
          emailConfirmed: false
        }
      }
    },
    created () {
      this.$http.get(`/api/profile`).then(response => {
        Object.assign(this.profile, response.data)

        var fields = [ { key: 'availableFrom', value: 28 }, { key: 'availableTo', value: 76 } ]
        fields.forEach(field => {
          if (!response.data[field.key]) {
            this.profile[field.key] = this.times[field.value]
          } else {
            var time = this.times.find(function (item) {
              return item.value === response.data[field.key]
            })
            if (time) {
              this.profile[field.key] = time.value
            }
          }
        })
      })
      this.times = utils.getTimesForSelectList()
    },
    methods: {
      validateBeforeSubmit: function () {
        this.$validator.validateAll().then(() => {
          this.profile.availableFrom = this.availableFrom
          this.profile.availableTo = this.availableTo
          var bag = new ErrorBag()
          this.$http.post('/api/profile', { ...this.profile })
            .then(response => {
              this.saved = true
            })
            .catch(response => {
              this.saved = false
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
      resendVerificationEmail: function () {
        this.$http.post('/api/register/resendverification')
          .then(response => {
            this.resentVerification = true
          })
          .catch(response => {
            debugger
          })
      }
    }
  }

</script>

<style lang="scss" scoped>

</style>
