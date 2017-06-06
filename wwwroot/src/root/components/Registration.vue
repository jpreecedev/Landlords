<template>
  <main>
    <div class="alert alert-danger" v-if="expired">
      Your session has expired, please log in again.
    </div>
    <div class="alert alert-success" v-if="loggedOut">
      You have been logged out
    </div>
    <div> 
      <h1 class="md-display-2">Please Sign In or Register using the forms below</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div class="row mt-5">
      <div class="col-12 col-md-5">
        <loginform />
      </div>
      <div class="col-12 col-md-7">
        <registerform />
      </div>
    </div>
  </main>
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
