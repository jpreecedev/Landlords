<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-title class="primary title">
          Sign in
        </v-card-title>
        <v-card-text>
          <div class="row">
            <div class="col-xs-12">
              <p v-if="errors.length > 0">
                <span class="red--text">{{errors[0].message}}</span>
              </p>
            </div>
          </div>
          <text-field v-model="credentials.username"
                      :rules="[$validation.rules.required, $validation.rules.email]"
                      label="Email address"
                      required>
          </text-field>
          <text-field v-model="credentials.password"
                      :rules="[$validation.rules.required, $validation.rules.min_length(credentials.password, 8)]"
                      :append-icon="credentials.passwordPlain ? 'visibility' : 'visibility_off'"
                      :append-icon-cb="() => (credentials.passwordPlain = !credentials.passwordPlain)"
                      :type="!credentials.passwordPlain ? 'password' : 'text'"
                      label="Password"
                      min="8"
                      required>
          </text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn primary flat type="submit" :loading="loggingIn">Log in</v-btn>
          <v-btn flat type="reset" @click.native="reset()">Reset</v-btn>
        </v-card-actions>
      </v-card>
    </form>
  </div>
</template>

<script>
import utils from 'utils'

export default {
  name: 'loginform',
  data () {
    return {
      credentials: {
        username: '',
        password: '',
        passwordPlain: false
      },
      loggingIn: false,
      errors: []
    }
  },
  methods: {
    validateBeforeSubmit () {
      this.$validation.validate(this.$children)
        .then(() => {
          this.loggingIn = true
          this.errors = []

          this.$auth.login(this.credentials, 'dashboard')
            .then(response => {
              this.loggingIn = false
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
              this.loggingIn = false
            })
        })
    },
    reset () {
      this.errors = []
    }
  }
}
</script>
