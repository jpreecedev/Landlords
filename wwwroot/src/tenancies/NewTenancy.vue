<template>
  <div>
    <header>
      <h1 class="headline primary--text">New tenancy</h1>
      <p class="display-2 grey--text text--darken-1">Capture your agreement with your new tenant</p>
      <p class="subheading">
        <span v-if="newTenancy.step === 1">Click 'Continue' below to get started.</span>
        <span v-else>Click 'Continue' to go to the next step.</span>
      </p>
    </header>
    <starttenancy v-if="permissions.J_StartTenancy"></starttenancy>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'new-tenancy',
  mounted () {
    this.$store.commit('TENANCY_CLEAR')
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions,
      newTenancy: state => state.newTenancy
    })
  }
}
</script>
