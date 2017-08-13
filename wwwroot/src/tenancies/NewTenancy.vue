<template>
    <starttenancy v-if="permissions.J_StartTenancy"></starttenancy>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'new-tenancy',
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  }
}
</script>
