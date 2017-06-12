<template>
  <div>
    <router-link v-if="permissions.TR_GetById" :to="'/accounts/transactions/' + account.id" class="mt-3 d-block">
      <span>{{ account.name }}</span>
    </router-link>
    <p v-else>{{ account.name }}</p>
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
