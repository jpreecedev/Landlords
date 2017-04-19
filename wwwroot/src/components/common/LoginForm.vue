<template>
  <div class="card">
    <div class="card-block">
      <h3 class="card-title">Sign In</h3>
      <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
        <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
        <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
      </div>
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <div class="form-group row" :class="{ 'has-danger': errors.has('emailAddress') }">
          <label for="emailAddress" class="col-3">Email Address</label>
          <div class="col-9">
            <input type="email" class="form-control" id="emailAddress" name="emailAddress" placeholder="Enter your email" v-validate="'required|email'" v-model="credentials.username" required>
            <span v-show="errors.has('emailAddress')" v-bind:title="errors.first('emailAddress')" class="form-control-feedback">Enter a valid email address</span>
          </div>
        </div>
        <div class="form-group row" :class="{ 'has-danger': errors.has('password') }">
          <label for="password" class="col-3">Password</label>
          <div class="col-9">
            <input type="password" class="form-control" id="password" name="password" placeholder="Password" v-model="credentials.password" v-validate="'required|min:8'" required>
            <span v-show="errors.has('password')" v-bind:title="errors.first('password')" class="form-control-feedback">Enter your password</span>
          </div>
        </div>
        <button type="submit" v-bind:disabled="loggingIn" class="btn btn-primary">Log in</button>
        <button class="btn btn-secondary" type="reset">Reset</button>
      </form>
    </div>
  </div>
</template>

<script>

import utils from '@/utils'
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
    validateBeforeSubmit () {
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
        debugger
        this.$validator.errorBag = bag
      })
    }
  }
}
</script>

<style lang="scss" scoped>

  h3 {
    text-align: center;
  }

</style>
