<template>
  <div class="card">
    <div class="card-block">
      <h3 class="card-title">Register</h3>
      <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
        <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
        <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
      </div>
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <fieldset>
          <div class="form-group row" :class="{ 'has-danger': errors.has('firstName') }">
            <label class="col-3 col-form-label" for="firstName">First name</label>
            <div class="col-9">
              <input v-model="newUser.firstName" class="form-control" id="firstName" name="firstName" type="text" placeholder="Your first name" v-validate="'required'" data-vv-validate-on="change" required>
              <span v-show="errors.has('firstName')" v-bind:title="errors.first('firstName')" class="form-control-feedback">Enter a valid first name</span>
            </div>
          </div>
          <div class="form-group row" :class="{ 'has-danger': errors.has('lastName') }">
            <label class="col-3 col-form-label" for="lastName">Last name</label>
            <div class="col-9">
              <input v-model="newUser.lastName" id="lastName" name="lastName" type="text" placeholder="Your last name" class="form-control" v-validate="'required'" data-vv-validate-on="change" required>
              <span v-show="errors.has('lastName')" v-bind:title="errors.first('lastName')" class="form-control-feedback">Enter a valid last name</span>
            </div>
          </div>
          <div class="form-group row" :class="{ 'has-danger': errors.has('emailAddress') }">
            <label class="col-3 col-form-label" for="email">E-mail</label>
            <div class="col-9">
              <input v-model="newUser.emailAddress" id="emailAddress" name="emailAddress" type="text" placeholder="you@email.com" class="form-control" v-validate="'required|email'" data-vv-validate-on="none" required>
              <span v-show="errors.has('emailAddress')" v-bind:title="errors.first('emailAddress')" class="form-control-feedback">Enter a valid email address</span>
            </div>
          </div>
          <div class="form-group row" :class="{ 'has-danger': errors.has('password') }">
            <label class="col-3 col-form-label" for="password">Password</label>
            <div class="col-9">
              <input v-model="newUser.password" id="password" name="password" type="password" placeholder="Enter your password" class="form-control" v-validate="'required|min:8|confirmed:repeatPassword'" data-vv-validate-on="change" required>
              <span v-show="errors.has('password:min')" v-bind:title="errors.first('password:min')" class="form-control-feedback">{{ errors.first('password:min') }}</span>
              <span v-show="errors.has('password:required')" v-bind:title="errors.first('password:required')" class="form-control-feedback">{{ errors.first('password:required') }}</span>
            </div>
          </div>
          <div class="form-group row" :class="{ 'has-danger': errors.has('password') }">
            <label class="col-3 col-form-label" for="repeatPassword">Repeat password </label>
            <div class="col-9">
              <input id="repeatPassword" name="repeatPassword" type="password" placeholder="Repeat your password" v-validate="'required'" v-model="newUser.repeatPassword" data-vv-as="password confirmation" data-vv-validate-on="change" class="form-control">
              <span v-show="errors.has('password:confirmed')" class="form-control-feedback">{{ errors.first('password:confirmed') }}</span>
            </div>
          </div>     
          <button type="submit" v-bind:disabled="registering" id="register" name="register" class="btn btn-primary">Register</button>
          <button type="reset" @click="reset()" class="btn btn-secondary">Reset</button>
        </fieldset>
      </form>
    </div>
  </div>
</template>

<script>
  import utils from '@/utils'
  import { ErrorBag } from 'vee-validate'

  export default {
    name: 'registerform',
    data () {
      return {
        newUser: {
          firstName: '',
          lastName: '',
          emailAddress: '',
          password: '',
          repeatPassword: ''
        },
        registering: false
      }
    },
    methods: {
      validateBeforeSubmit: function () {
        this.$validator.validateAll().then(() => {
          this.registering = true
          var bag = new ErrorBag()

          const registrationDetails = {
            firstName: this.newUser.firstName,
            lastName: this.newUser.lastName,
            emailAddress: this.newUser.emailAddress,
            password: this.newUser.password
          }

          this.$http.post('/api/register', registrationDetails).then((response) => {
            this.registering = false
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
        })
        .catch(() => {
          window.scrollTo(0, 0)
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
