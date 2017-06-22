<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-row class="primary white--text">
          <v-card-title>
            Sign in
          </v-card-title>
        </v-card-row>
        <v-card-text>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('emailAddress') }">
              <label for="emailAddress">Email Address</label>
              <md-input type="email" id="emailAddress" name="emailAddress" data-vv-name="emailAddress" v-validate="'required|email'" v-model="credentials.username" data-vv-validate-on="change" required />
              <span v-if="errorBag.has('emailAddress')" class="md-error">Enter a valid email address</span>
            </md-input-container>
          </v-card-row>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('password') }">
              <label for="password">Password</label>
              <md-input type="password" id="password" name="password" data-vv-name="password" v-model="credentials.password" v-validate="'required|min:8'" data-vv-validate-on="change" required />
              <span v-if="errorBag.has('password')" class="md-error">Enter your password</span>
            </md-input-container>
          </v-card-row>
        </v-card-text>
        <v-card-row actions>
          <v-btn primary flat type="submit" :disabled="loggingIn">Log in</v-btn>
          <v-btn flat type="reset" @click.native="reset()">Reset</v-btn>
        </v-card-row>
      </v-card>
    </form>
  </div>
</template>

<script>

import utils from 'utils'
import { ErrorBag } from 'vee-validate'

export default {
  name: 'loginform',
  data () {
    return {
      credentials: {
        username: '',
        password: ''
      },
      loggingIn: false
    }
  },
  methods: {
    validateBeforeSubmit: function () {
      this.$validator.validateAll().then(() => {
        this.loggingIn = true
        var bag = new ErrorBag()
        this.$auth.login(this.credentials, 'dashboard')
        .then(response => {
          this.loggingIn = false
          if (!response.ok) {
            var validationResult = utils.getFormValidationErrors(response)
            validationResult.errors.forEach(validationError => {
              bag.add(validationError.key, validationError.messages[0], 'required')
            })
            if (validationResult.status) {
              bag.add('GenericError', validationResult.status, 'generic')
            }
          }
        })
        this.$validator.errorBag = bag
      })
    },
    reset: function () {
      this.errors.clear()
    }
  }
}
</script>

<style lang="scss" scoped>

  h3 {
    text-align: center;
  }

</style>
