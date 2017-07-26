<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-title class="title primary">
          Register
        </v-card-title>
        <v-card-text>
          <v-text-field v-model="newUser.firstName"
                        :rules="[$validation.rules.required, $validation.rules.min_length(newUser.firstName, 2)]"
                        label="First name"
                        required>
          </v-text-field>
          <v-text-field v-model="newUser.lastName"
                        :rules="[$validation.rules.required, $validation.rules.min_length(newUser.lastName, 2)]"
                        label="Last name"
                        required>
          </v-text-field>
          <v-text-field v-model="newUser.emailAddress"
                        :rules="[$validation.rules.required, $validation.rules.email]"
                        label="Email address"
                        required>
          </v-text-field>
          <v-text-field v-model="newUser.password"
                        :rules="[$validation.rules.required, $validation.rules.min_length(newUser.password, 8)]"
                        :append-icon="newUser.passwordPlain ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (newUser.passwordPlain = !newUser.passwordPlain)"
                        :type="!newUser.passwordPlain ? 'password' : 'text'"
                        label="Password"
                        min="8"
                        required>
          </v-text-field>
          <v-text-field v-model="newUser.repeatPassword"
                        :rules="[$validation.rules.required, $validation.rules.min_length(newUser.repeatPassword, 8)]"
                        :append-icon="newUser.repeatPasswordPlain ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (newUser.repeatPasswordPlain = !newUser.repeatPasswordPlain)"
                        :type="!newUser.repeatPasswordPlain ? 'password' : 'text'"
                        label="Repeat password"
                        min="8"
                        required>
          </v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn primary flat type="submit" :loading="registering" id="register" name="register">Register</v-btn>
          <v-btn flat type="reset" @click="reset()">Reset</v-btn>
        </v-card-actions>
      </v-card>
    </form>
  </div>
</template>

<script>
  import utils from 'utils'

  let defaultUser = {
    firstName: '',
    lastName: '',
    emailAddress: '',
    password: '',
    passwordPlain: '',
    repeatPassword: '',
    repeatPasswordPlain: ''
  }

  export default {
    name: 'registerform',
    data () {
      return {
        newUser: Object.assign({}, defaultUser),
        registering: false,
        errors: []
      }
    },
    methods: {
      validateBeforeSubmit () {
        this.registering = true
        this.errors = []

        const registrationDetails = {
          firstName: this.newUser.firstName,
          lastName: this.newUser.lastName,
          emailAddress: this.newUser.emailAddress,
          password: this.newUser.password
        }

        this.$http.post('/api/register', registrationDetails)
          .then((response) => {
            this.registering = false
            if (!response.ok) {
              let validationResult = utils.getFormValidationErrors(response)
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
            }
          })
          .catch(() => {
            this.registering = false
          })
      },
      reset () {
        this.newUser = Object.assign({}, this.defaultUser)
      }
    }
  }
</script>
