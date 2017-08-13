<template>
  <landlord-dashboard v-if="auth.role === 'Landlord'"></landlord-dashboard>
  <tenant-dashboard v-else-if="auth.role === 'Tenant'"></tenant-dashboard>
  <agency-dashboard v-else-if="auth.role === 'Agency'"></agency-dashboard>
  <administrator-dashboard v-else-if="auth.role === 'Administrator'"></administrator-dashboard>
  <div v-else>
    We don't have enough information to build a dashboard for you right now.<br/><br/>Please check back another time or contact customer support.
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'dashboard',
  computed: {
    ...mapState({
      auth: state => state.auth
    })
  }
}
</script>