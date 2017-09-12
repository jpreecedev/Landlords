<template>
  <div>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <v-card>
        <v-card-text>
          <header>
            <h3 class="text-left blue--text">Welcome</h3>
            <p class="text-left subheading">Enter a few basic details to quickly create your account</p>
          </header>
          <text-field v-model="newUser.firstName"
                      :rules="[$validation.rules.required, $validation.rules.min_length(newUser.firstName, 2)]"
                      label="First name"
                      required>
          </text-field>
          <text-field v-model="newUser.lastName"
                      :rules="[$validation.rules.required, $validation.rules.min_length(newUser.lastName, 2)]"
                      label="Last name"
                      required>
          </text-field>
          <text-field v-model="newUser.emailAddress"
                      :rules="[$validation.rules.required, $validation.rules.email]"
                      label="Email address"
                      required>
          </text-field>
          <text-field v-model="newUser.password"
                      :rules="[$validation.rules.required, $validation.rules.min_length(newUser.password, 8)]"
                      type="password"
                      label="Password"
                      min="8"
                      required>
          </text-field>
          <text-field v-model="newUser.repeatPassword"
                      :rules="[$validation.rules.required, $validation.rules.min_length(newUser.repeatPassword, 8), $validation.rules.match('password', newUser.password, newUser.repeatPassword)]"
                      type="password"
                      label="Repeat password"
                      min="8"
                      required>
          </text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn primary flat type="submit" :loading="isRegistering" id="register" name="register">Register</v-btn>
          <v-btn flat type="reset" @click="reset()">Reset</v-btn>
        </v-card-actions>
      </v-card>
    </form>
  </div>
</template>

<script>
  export default {
    name: 'registerform',
    data () {
      return {
        newUser: {
          firstName: '',
          lastName: '',
          emailAddress: '',
          password: '',
          passwordPlain: '',
          repeatPassword: '',
          repeatPasswordPlain: ''
        },
        isRegistering: false,
        errors: []
      }
    },
    methods: {
      validateBeforeSubmit () {
        this.$validation.validate(this.$children)
          .then(() => {
            this.errors = []
            this.isRegistering = true

            const registrationDetails = {
              firstName: this.newUser.firstName,
              lastName: this.newUser.lastName,
              emailAddress: this.newUser.emailAddress,
              password: this.newUser.password
            }

            this.$http.post('/api/register', registrationDetails)
              .then(() => {
                this.$router.push({name: 'dashboard'})
              })
              .catch(errors => {
                this.errors = errors
                this.isRegistering = false
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
