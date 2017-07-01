<template>
  <div>
    <h1 class="display-2">Profile</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-alert success :value="saved">
      Your profile has been updated
    </v-alert>
    <v-alert info :value="resentVerification">
      We have sent you a new verification email.  Please check your inbox.
    </v-alert>
    <v-alert success :value="confirmed">
      Your email address has been verified
    </v-alert>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.P_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-title class="primary white--text">
                Personal details
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <v-text-field v-model="profile.firstName"
                                :rules="[$validation.rules.required, $validation.rules.min_length(profile.firstName, 2)]"
                                label="First name"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="profile.lastName"
                                :rules="[$validation.rules.required, $validation.rules.min_length(profile.lastName, 2)]"
                                label="Last name"
                                required>
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <div>
                    <p>Email address
                      <br>
                      {{ profile.emailAddress }}
                    </p>
                  </div>
                </v-card-row>
                <v-card-row actions>
                  <v-btn warning flat light v-if="!profile.emailConfirmed" @click.native="resendVerificationEmail()">Re-send verification email</v-btn>
                </v-card-row>
              </v-card-text>
            </v-card>
          </div>
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-title class="primary white--text">
                Contact details
              </v-card-title>
              <v-card-text>
                <v-card-row>
                  <v-select :items="times"
                            :rules="[$validation.rules.required]"
                            v-model="availableFrom"
                            label="Available from"
                            item-value="value"
                            dark>
                  </v-select>
                </v-card-row>
                <v-card-row>
                  <v-select :items="times"
                            :rules="[$validation.rules.required]"
                            v-model="availableTo"
                            label="Available to"
                            item-value="value"
                            dark>
                  </v-select>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="profile.phoneNumber"
                                label="Main phone number"
                                type="tel">
                  </v-text-field>
                </v-card-row>
                <v-card-row>
                  <v-text-field v-model="profile.secondaryPhoneNumber"
                                label="Secondary phone number"
                                type="tel">
                  </v-text-field>
                </v-card-row>
              </v-card-text>
            </v-card>
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
  </div>
</template>

<script>
  import utils from 'utils'

  export default {
    name: 'profile',
    data () {
      return {
        errors: [],
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
      this.times = utils.getTimesForSelectList()
      this.$http.get(`/api/profile`).then(response => {
        Object.assign(this.profile, response.data)
        var fields = [ { key: 'availableFrom', value: 28 }, { key: 'availableTo', value: 76 } ]
        fields.forEach(field => {
          if (!response.data[field.key]) {
            this[field.key] = this.profile[field.key] = this.times[field.value]
          } else {
            var time = this.times.find(function (item) {
              return item.value === response.data[field.key]
            })
            if (time) {
              this[field.key] = this.profile[field.key] = time.value
            }
          }
        })
      })
    },
    methods: {
      validateBeforeSubmit: function () {
        this.errors = []
        this.profile.availableFrom = this.availableFrom
        this.profile.availableTo = this.availableTo

        this.$http.post('/api/profile', { ...this.profile })
          .then(response => {
            this.saved = true
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
