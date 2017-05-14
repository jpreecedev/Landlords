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
          <router-link v-if="permissions.LL_List" tag="li" to="/agency/landlord-list" active-class="active" class="nav-item">
            <a class="nav-link">Landlord List</a>
          </router-link>
          <router-link v-if="!auth.isLoggedIn" tag="li" to="/registration" active-class="active" class="nav-item">
            <a class="nav-link">Registration</a>
          </router-link>
          <router-link v-if="permissions.PD_GetList" tag="li" to="/manager/property-list" active-class="active" class="nav-item">
            <a class="nav-link">Property List</a>
          </router-link>
          <router-link v-if="permissions.PE_List" tag="li" to="/permissions" active-class="active" class="nav-item">
            <a class="nav-link">Permissions</a>
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
        </ul>
        <ul class="navbar-nav" v-if="auth.isLoggedIn">
          <li class="navbar-text">
            Hi, {{ user.name }}
          </li>
          <router-link v-if="permissions.P_View" tag="li" to="/profile" active-class="active" class="nav-item">
            <a class="nav-link">View Profile</a>
          </router-link>
          <li class="nav-item">
            <a class="pointer nav-link d-inline-block" @click="logout()">Log Out.</a>
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
      user: this.$store.state.user,
      permissions: this.$store.state.permissions,
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
