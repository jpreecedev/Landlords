<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset>
        <md-card>
          <md-card-header>
            <div class="md-title">Register</div>
          </md-card-header>
          <md-card-content>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container :class="{ 'md-input-invalid': errors.has('firstName') }">
                  <label for="firstName">First name</label>
                  <md-input v-model="newUser.firstName" id="firstName" name="firstName" type="text" data-vv-name="firstName" v-validate="'required'" data-vv-validate-on="change" required />
                  <span v-if="errors.has('firstName')" class="md-error">Enter a valid first name</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container :class="{ 'md-input-invalid': errors.has('lastName') }">
                  <label for="lastName">Last name</label>
                  <md-input v-model="newUser.lastName" id="lastName" name="lastName" type="text" data-vv-name="lastName" v-validate="'required'" data-vv-validate-on="change" required />
                  <span v-if="errors.has('lastName')" class="md-error">Enter a valid last name</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container :class="{ 'md-input-invalid': errors.has('emailAddress') }">
                  <label for="email">E-mail</label>
                  <md-input v-model="newUser.emailAddress" id="emailAddress" name="emailAddress" type="text" data-vv-name="emailAddress" v-validate="'required|email'" data-vv-validate-on="none" required />
                  <span v-if="errors.has('emailAddress')" class="md-error">Enter a valid email address</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container :class="{ 'md-input-invalid': errors.has('password') }">
                  <label for="password">Password</label>
                  <md-input v-model="newUser.password" id="password" name="password" type="password" data-vv-name="password" v-validate="'required|min:8|confirmed:repeatPassword'" data-vv-validate-on="change" required />
                  <span v-if="errors.has('password:min')" class="md-error">{{ errors.first('password:min') }}</span>
                  <span v-else-if="errors.has('password:required')" class="md-error">{{ errors.first('password:required') }}</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <md-input-container :class="{ 'md-input-invalid': errors.has('password') }">
                  <label for="repeatPassword">Repeat password </label>
                  <md-input id="repeatPassword" name="repeatPassword" type="password" data-vv-name="repeatPassword" v-validate="'required'" v-model="newUser.repeatPassword" data-vv-as="password confirmation" data-vv-validate-on="change" />
                  <span v-if="errors.has('password:confirmed')" class="md-error">{{ errors.first('password:confirmed') }}</span>
                </md-input-container>
              </div>
            </div>  
          </md-card-content>
          <md-card-actions>
            <md-button type="submit" v-bind:disabled="registering" id="register" name="register" class="md-primary">Register</md-button>
            <md-button type="reset" @click.native="reset()" class="md-default">Reset</md-button>
          </md-card-actions>
        </md-card>
      </fieldset>
    </form>
  </div>
</template>

<script>
  import utils from 'utils'
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
