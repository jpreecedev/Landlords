<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-text>
          <header>
            <h3 class="text-left blue--text">Hello</h3>
            <p class="text-left subheading">Please log in with you account details</p>
          </header>
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
                      type="password"
                      label="Password"
                      min="8"
                      required>
          </text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn primary flat type="submit" :loading="loggingIn">Log in</v-btn>
          <v-btn flat type="reset" @click="reset()">Reset</v-btn>
        </v-card-actions>
      </v-card>
    </form>
  </div>
</template>

<script>
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

          this.$auth.login(this.credentials)
            .then(() => {
              this.$router.push({name: 'dashboard'})
            })
            .catch(errors => {
              this.errors = errors
              this.loggingIn = false
            })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
        })
    },
    reset () {
      this.$validation.reset(this.$children)
    }
  }
}
</script>

<style lang="scss" scoped>
  h3 {
    margin: 1.5rem 0 0 0;
  }
</style>

