<template>
  <nav class="navbar navbar-toggleable-md navbar-inverse bg-inverse fixed-top">
    <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbar-collapse" aria-controls="navbar-collapse" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <router-link to="home" class="navbar-brand">Landlords</router-link>

    <div class="collapse navbar-collapse" id="navbar-collapse">
      <ul class="navbar-nav mr-auto">
        <router-link tag="li" to="/dashboard" active-class="active" class="nav-item">
          <a class="nav-link">Dashboard <span class="sr-only">(current)</span></a>
        </router-link>
        <router-link v-if="!isLoggedIn" tag="li" to="/registration" active-class="active" class="nav-item">
          <a class="nav-link">Registration</a>
        </router-link>
        <router-link v-if="isLoggedIn" tag="li" to="/propertyDetails" active-class="active" class="nav-item">
          <a class="nav-link">Property Details</a>
        </router-link>
        <li v-if="isLoggedIn" class="nav-item">
          <a class="pointer nav-link" @click="logout()">Log Out</a>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  data () {
    return {
      isLoggedIn: this.$store.state.auth.isLoggedIn
    }
  },
  mounted () {
    this.$store.watch((state) => {
      return state.auth.isLoggedIn
    }, (isLoggedIn) => {
      this.isLoggedIn = isLoggedIn
    })
  },
  methods: {
    logout () {
      this.$auth.logout(false)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
