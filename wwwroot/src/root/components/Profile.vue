<template>
  <main class="container">
    <div>
      <h1 class="md-display-2">Profile</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>
    <div class="alert alert-success" v-if="saved">
      Your profile has been updated
    </div>
    <div class="alert alert-success" v-if="resentVerification">
      We have sent you a new verification email.  Please check your inbox.
    </div>
    <div class="alert alert-success" v-if="confirmed">
      Your email address has been verified
    </div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset v-bind:disabled="!permissions.P_Update">
        <div class="row">
          <div class="col">
            <div class="card">
              <div class="card-block">
                <h3 class="card-title">Personal Details</h3>
                <div class="form-group row" :class="{ 'has-danger': errors.has('firstName') }">
                  <label class="col-12 col-form-label" for="firstName">First name</label>
                  <div class="col-12">
                    <input v-model="profile.firstName" class="form-control" id="firstName" name="firstName" type="text" placeholder="Your first name" v-validate="'required'" data-vv-validate-on="change" required>
                    <span v-show="errors.has('firstName')" v-bind:title="errors.first('firstName')" class="form-control-feedback">Enter a valid first name</span>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('lastName') }">
                  <label class="col-12 col-form-label" for="lastName">Last name</label>
                  <div class="col-12">
                    <input v-model="profile.lastName" id="lastName" name="lastName" type="text" placeholder="Your last name" class="form-control" v-validate="'required'" data-vv-validate-on="change" required>
                    <span v-show="errors.has('lastName')" v-bind:title="errors.first('lastName')" class="form-control-feedback">Enter a valid last name</span>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="secondaryPhoneNumber">Email address</label>
                  <div class="col-12">
                    <p>{{ profile.emailAddress }}</p>
                    <md-button v-if="!profile.emailConfirmed" @click.native="resendVerificationEmail()" type="button" class="md-raised md-warn pointer">Re-send verification email</md-button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col">
            <div class="card">
              <div class="card-block">
                <h3 class="card-title">Contact Details</h3>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="availableFrom">Available From</label>
                  <div class="col-12">
                    <select v-model="profile.availableFrom" class="form-control" id="availableFrom" name="availableFrom">
                      <option disabled value="">Select a time</option>
                      <option v-for="time in times" v-bind:value="time.value">{{ time.display }}</option>
                    </select>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="availableTo">Available To</label>
                  <div class="col-12">
                    <select v-model="profile.availableTo" class="form-control" id="availableTo" name="availableTo">
                      <option disabled value="">Select a time</option>
                      <option v-for="time in times" v-bind:value="time.value">{{ time.display }}</option>
                    </select>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="phoneNumber">Main Phone Number</label>
                  <div class="col-12">
                    <input v-model="profile.phoneNumber" class="form-control" id="phoneNumber" name="phoneNumber" type="tel" required>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="secondaryPhoneNumber">Secondary Phone Number</label>
                  <div class="col-12">
                    <input v-model="profile.secondaryPhoneNumber" class="form-control" id="secondaryPhoneNumber" name="secondaryPhoneNumber" type="tel" required>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col">
            <md-button v-if="permissions.P_Update" type="submit" class="md-raised md-primary">Save</md-button>
            <md-button v-if="permissions.P_Update" type="reset" class="md-raised md-default">Reset</md-button>
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
        times: utils.getTimesForSelectList(),
        saved: false,
        resentVerification: false,
        confirmed: this.$route.query.confirmed,
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
    },
    methods: {
      validateBeforeSubmit: function () {
        this.$validator.validateAll().then(() => {
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
