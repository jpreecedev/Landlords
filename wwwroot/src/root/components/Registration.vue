<template>
  <div>
    <div class="row center-xs">
      <div class="col-xs-12 col-md-5">
        <v-tabs class="elevation-3"
                dark fixed icons centered>
          <v-tabs-bar>
            <v-tabs-slider class="blue"></v-tabs-slider>
            <v-tabs-item href="login">
              <v-icon>face</v-icon>
              Log In
            </v-tabs-item>
            <v-tabs-item href="register">
              <v-icon>account_circle</v-icon>
              Register
            </v-tabs-item>
          </v-tabs-bar>
          <v-tabs-items>
            <v-tabs-content key="login" id="login">
              <v-card flat>
                <loginform></loginform>
              </v-card>
            </v-tabs-content>
            <v-tabs-content key="register" id="register">
              <v-card flat>
                <registerform></registerform>
              </v-card>
            </v-tabs-content>
          </v-tabs-items>
        </v-tabs>
      </div>
    </div>
  </div>
</template>

<script>
  import LoginForm from './LoginForm.vue'
  import RegisterForm from './RegisterForm.vue'

  export default {
    name: 'registration',
    components: {
      'loginform': LoginForm,
      'registerform': RegisterForm
    },
    data () {
      return {
        expired: this.$route.query.expired,
        loggedOut: this.$route.query.loggedOut
      }
    }
  }
</script>
