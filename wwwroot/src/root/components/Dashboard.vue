<template>
  <div>
    <header>
      <h1 class="headline primary--text">Dashboard</h1>
      <p class="display-2 grey--text text--darken-1">Your property portfolio is performing well
      <p class="subheading">Review the charts below for more information.</p></p>
    </header>
    <h1 class="display-2"></h1>
    <starttenancy v-if="permissions.J_StartTenancy"></starttenancy>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'dashboard',
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  methods: {
    openDialog (dialog) {
      this.$refs[dialog].openDialog()
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
