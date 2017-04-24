<template>
  <header class="navbar navbar-toggleable-md navbar-inverse bg-inverse">
    <nav class="container">
      <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbar-collapse" aria-controls="navbar-collapse" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <router-link to="/" class="navbar-brand">Landlords</router-link>

      <div class="collapse navbar-collapse" id="navbar-collapse">
        <ul class="navbar-nav mr-auto">
          <router-link v-if="auth.isLoggedIn" tag="li" to="/dashboard" active-class="active" class="nav-item">
            <a class="nav-link">Dashboard <span class="sr-only">(current)</span></a>
          </router-link>
          <router-link v-if="!auth.isLoggedIn" tag="li" to="/registration" active-class="active" class="nav-item">
            <a class="nav-link">Registration</a>
          </router-link>
          <router-link v-if="auth.isLoggedIn" tag="li" to="/manager/property-details" active-class="active" class="nav-item">
            <a class="nav-link">Property Details</a>
          </router-link>
          <router-link v-if="auth.isLoggedIn" tag="li" to="/manager/property-list" active-class="active" class="nav-item">
            <a class="nav-link">Property List</a>
          </router-link>
          <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="calculatorsDropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Calculators</a>
              <div class="dropdown-menu" aria-labelledby="calculatorsDropdown">
                <router-link class="dropdown-item" to="/calculators/rental-yield">Rental Yield</router-link>
                <router-link class="dropdown-item" to="/calculators/monthly-payment">Monthly Payment</router-link>
                <router-link class="dropdown-item" to="/calculators/how-much-can-i-borrow">How much can I borrow?</router-link>
                <router-link class="dropdown-item" to="/calculators/is-this-property-a-good-investment">Is this property a good investment?</router-link>
              </div>
          </li>
          <li v-if="auth.isLoggedIn" class="nav-item">
            <a class="pointer nav-link" @click="logout()">Log Out</a>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  data () {
    return {
      auth: this.$store.state.auth
    }
  },
  methods: {
    logout: function () {
      this.$auth.logout(false)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
