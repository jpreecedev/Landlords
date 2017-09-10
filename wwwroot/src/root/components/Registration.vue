<template>
  <div>
    <v-alert error :value="expired" class="mb-4">
      Your session has expired, please log in again.
    </v-alert>
    <v-alert success :value="loggedOut" class="mb-4">
      You have been logged out
    </v-alert>
    <header>
      <h1 class="headline primary--text">Please sign in or register</h1>
      <p class="display-2 grey--text text--darken-1">We need to know who you are</p>
      <p class="subheading">Please either sign in or register using the appropriate form below.</p>
    </header>
    <div class="row">
      <div class="col-xs-12 col-sm-5">
        <loginform></loginform>
      </div>
      <div class="col-xs-12 col-sm-offset-1 col-sm-6">
        <registerform></registerform>
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
