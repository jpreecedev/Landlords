<template>
  <div>
    <router-link v-if="permissions.TR_GetById" :to="'/accounts/transactions/' + account.id">
      <p>{{ account.name }}</p>
    </router-link>
    <span v-else>
      <p>{{ account.name }}</p>
    </span>
    <p>
      <span class="d-inline-block mr-2" v-if="account.number">{{ account.number }}</span>
      <span class="d-inline-block" v-if="account.sortCode">{{ account.sortCode }}</span>
    </p>
  </div>
</template>

<script>
export default {
  name: 'account-display',
  data () {
    return {
      permissions: this.$store.state.permissions
    }
  },
  props: {
    'account': {
      type: Object,
      default: null
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
