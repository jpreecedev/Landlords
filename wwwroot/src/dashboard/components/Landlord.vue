<template>
  <header>
    <h1 class="headline primary--text">Dashboard</h1>
    <p class="display-2 grey--text text--darken-1">{{user.name}}, your property portfolio is performing well
    <p class="subheading">Review the charts below for more information.</p></p>
  </header>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'landlord-dashboard',
  computed: {
    ...mapState({
      user: state => state.user
    })
  }
}
</script>
