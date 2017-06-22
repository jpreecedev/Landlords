<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-title class="primary white--text">
          Register
        </v-card-title>
        <v-card-text>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('firstName') }">
              <label for="firstName">First name</label>
              <md-input v-model="newUser.firstName" id="firstName" name="firstName" type="text" data-vv-name="firstName" v-validate="'required'" data-vv-validate-on="change" required />
              <span v-if="errorBag.has('firstName')" class="md-error">Enter a valid first name</span>
            </md-input-container>
          </v-card-row>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('lastName') }">
              <label for="lastName">Last name</label>
              <md-input v-model="newUser.lastName" id="lastName" name="lastName" type="text" data-vv-name="lastName" v-validate="'required'" data-vv-validate-on="change" required />
              <span v-if="errorBag.has('lastName')" class="md-error">Enter a valid last name</span>
            </md-input-container>
          </v-card-row>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('emailAddress') }">
              <label for="email">E-mail</label>
              <md-input v-model="newUser.emailAddress" id="emailAddress" name="emailAddress" type="text" data-vv-name="emailAddress" v-validate="'required|email'" data-vv-validate-on="none" required />
              <span v-if="errorBag.has('emailAddress')" class="md-error">Enter a valid email address</span>
            </md-input-container>
          </v-card-row>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('password') }">
              <label for="password">Password</label>
              <md-input v-model="newUser.password" id="password" name="password" type="password" data-vv-name="password" v-validate="'required|min:8|confirmed:repeatPassword'" data-vv-validate-on="change" required />
              <span v-if="errorBag.has('password:min')" class="md-error">{{ errorBag.first('password:min') }}</span>
              <span v-else-if="errorBag.has('password:required')" class="md-error">{{ errorBag.first('password:required') }}</span>
            </md-input-container>
          </v-card-row>
          <v-card-row>
            <md-input-container :class="{ 'md-input-invalid': errorBag.has('password') }">
              <label for="repeatPassword">Repeat password </label>
              <md-input id="repeatPassword" name="repeatPassword" type="password" data-vv-name="repeatPassword" v-validate="'required'" v-model="newUser.repeatPassword" data-vv-as="password confirmation" data-vv-validate-on="change" />
              <span v-if="errorBag.has('password:confirmed')" class="md-error">{{ errorBag.first('password:confirmed') }}</span>
            </md-input-container>
          </v-card-row>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-row actions>
          <v-btn primary flat type="submit" :disabled="registering" id="register" name="register">Register</v-btn>
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
