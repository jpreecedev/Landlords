<template>
  <div class="mt-2">
    <router-link v-if="permissions.TR_GetById" :to="'/finances/transactions/' + account.id" class="mt-3 d-block">
      <span>{{ account.name }}</span>
    </router-link>
    <span v-else>{{ account.name }}</span>
    <p>
      <span class="d-inline-block mr-2" v-if="account.number">{{ account.number }}</span>
      <span class="d-inline-block" v-if="account.sortCode">{{ account.sortCode }}</span>
    </p>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'account-display',
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
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
