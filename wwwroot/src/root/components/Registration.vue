<template>
  <div>
    <v-alert error :value="expired" class="mb-4">
      Your session has expired, please log in again.
    </v-alert>
    <v-alert success :value="loggedOut" class="mb-4">
      You have been logged out
    </v-alert>
    <div>
      <h1 class="display-2">Please sign in or register</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div class="row mt-5">
      <div class="col-xs-12 col-md-5">
        <loginform />
      </div>
      <div class="col-xs-12 col-md-offset-1 col-md-6">
        <registerform />
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
