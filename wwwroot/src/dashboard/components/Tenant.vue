<template>
  <header>
    <h1 class="headline primary--text">Dashboard</h1>
    <p class="display-2 grey--text text--darken-1">{{user.name}}, you're all up to date!
    <p class="subheading">Review the charts below for more information.</p></p>
  </header>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'tenant-dashboard',
  computed: {
    ...mapState({
      user: state => state.user
    })
  }
}
</script>
