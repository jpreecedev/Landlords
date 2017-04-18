<template>
  <main class="container">
    <div class="alert alert-danger" v-if="expired">
      Your session has expired, please log in again.
    </div>
    <div class="alert alert-success" v-if="loggedOut">
      You have been logged out
    </div>
    <div class="row">
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

  import LoginForm from './common/LoginForm.vue'
  import RegisterForm from './common/RegisterForm.vue'

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
  
<style lang="scss" scoped>
  
</style>
