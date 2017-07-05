<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-title class="primary title">
          Sign in
        </v-card-title>
        <v-card-text>
          <v-text-field v-model="credentials.username"
                        :rules="[$validation.rules.required, $validation.rules.email]"
                        :value="credentials.username"
                        label="Email address"
                        required>
          </v-text-field>
          <v-text-field v-model="credentials.password"
                        :rules="[$validation.rules.required, $validation.rules.min_length(credentials.password, 8)]"
                        :append-icon="credentials.passwordPlain ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (credentials.passwordPlain = !credentials.passwordPlain)"
                        :type="!credentials.passwordPlain ? 'password' : 'text'"
                        label="Password"
                        min="8"
                        required>
          </v-text-field>
        </v-card-text>
        <v-card-actions>
          <div class="col-xs-6">
            <validation-summary :errors="errors"></validation-summary>
          </div>
          <div class="col-xs-6">
            <v-btn primary flat type="submit" :disabled="loggingIn">Log in</v-btn>
            <v-btn flat type="reset" @click.native="reset()">Reset</v-btn>
          </div>
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
    validateBeforeSubmit: function () {
      this.loggingIn = true
      this.errors = []

      this.$auth.login(this.credentials, 'dashboard')
        .then(response => {
          this.loggingIn = false
          if (!response.ok) {
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
          }
        })
    },
    reset: function () {
      this.errors = []
    }
  }
}
</script>
