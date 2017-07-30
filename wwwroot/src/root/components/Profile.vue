<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Profile</h1>
        <p class="display-2 grey--text text--darken-1">Complete your profile so that ourselves and your tenants can contact when it best suits you</p>
      </header>
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
                <v-card-title class="title primary">
                  Personal details
                </v-card-title>
                <v-card-text>
                  <div>
                    <text-field v-model="profile.firstName"
                                :rules="[$validation.rules.required, $validation.rules.min_length(profile.firstName, 2)]"
                                label="First name"
                                required>
                    </text-field>
                    <text-field v-model="profile.lastName"
                                :rules="[$validation.rules.required, $validation.rules.min_length(profile.lastName, 2)]"
                                label="Last name"
                                required>
                    </text-field>
                    <div>
                      <p>Email address
                        <br>
                        {{ profile.emailAddress }}
                      </p>
                    </div>
                  </div>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn warning flat v-if="!profile.emailConfirmed" @click="resendVerificationEmail()">Re-send verification email</v-btn>
                  </v-card-actions>
                </v-card-text>
              </v-card>
            </div>
            <div class="col-xs-12 col-md-6">
              <v-card>
                <v-card-title class="title primary">
                  Contact details
                </v-card-title>
                <v-card-text>
                  <select-list :items="times"
                               :rules="[$validation.rules.required]"
                               v-model="availableFrom"
                               label="Available from">
                  </select-list>
                  <select-list :items="times"
                               :rules="[$validation.rules.required]"
                               v-model="availableTo"
                               label="Available to">
                  </select-list>
                  <text-field v-model="profile.phoneNumber"
                              label="Main phone number"
                              type="tel">
                  </text-field>
                  <text-field v-model="profile.secondaryPhoneNumber"
                              label="Secondary phone number"
                              type="tel">
                  </text-field>
                </v-card-text>
              </v-card>
            </div>
          </div>
          <div class="row mt-3">
            <div class="col-xs-12">
              <v-btn primary v-if="permissions.P_Update" type="submit" :loading="isSaving">Save</v-btn>
              <v-btn flat v-if="permissions.P_Update" @click="reset()">Reset</v-btn>
            </div>
          </div>
        </fieldset>
      </form>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'profile',
  data () {
    return {
      isLoading: false,
      isSaving: false,
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
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  created () {
    this.times = utils.getTimesForSelectList()
    this.isLoading = true
    this.$http.get(`/api/profile`)
      .then(response => {
        Object.assign(this.profile, response.data)
        let fields = [ { key: 'availableFrom', value: 28 }, { key: 'availableTo', value: 76 } ]
        fields.forEach(field => {
          if (!response.data[field.key]) {
            this[field.key] = this.profile[field.key] = this.times[field.value]
          } else {
            let time = this.times.find(function (item) {
              return item.value === response.data[field.key]
            })
            if (time) {
              this[field.key] = this.profile[field.key] = time.value
            }
          }
        })
      })
      .finally(() => {
        this.isLoading = false
        this.$validation.commit(this.$children)
      })
  },
  methods: {
    validateBeforeSubmit () {
      this.profile.availableFrom = this.availableFrom
      this.profile.availableTo = this.availableTo

      this.$validation.validate(this.$children)
        .then(() => {
          this.isSaving = true
          this.$http.post('/api/profile', { ...this.profile })
            .then(response => {
              this.saved = true
            })
            .catch(response => {
              let validationResult = utils.getFormValidationErrors(response)
              validationResult.errors.forEach(validationError => {
                console.log('ERROR', validationError.key, validationError.messages[0], 'required')
              })
            })
            .finally(() => {
              this.isSaving = false
            })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
        })
    },
    resendVerificationEmail () {
      this.$http.post('/api/register/resendverification')
        .then(response => {
          this.resentVerification = true
        })
    },
    reset () {
      this.$validation.reset(this.$children)
    }
  }
}
</script>
