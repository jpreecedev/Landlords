<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Tenancy details</h1>
        <p class="display-2 grey--text text--darken-1">See details of your agreement with your tenant</p>
        <p class="subheading">
          <span v-if="tenancy.step === 1">Click 'Continue' below to get started.</span>
          <span v-else>Click 'Continue' to go to the next step.</span>
        </p>
      </header>
      <starttenancy v-if="permissions.J_StartTenancy"></starttenancy>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'tenancy-details',
  data () {
    return {
      isLoading: false,
      viewData: {
        counties: [],
        countries: [],
        rentalFrequencies: [],
        tenancyTypes: []
      }
    }
  },
  created () {
    this.isLoading = true
    this.$store.commit('TENANCY_CLEAR')
    this.$http.get(`/api/tenancies/tenancy?tenancyId=${this.$route.params.tenancyId}`)
      .then(response => {
        this.$store.commit('TENANCY_SET', response.data)
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions,
      tenancy: state => state.newTenancy
    })
  }
}
</script>
