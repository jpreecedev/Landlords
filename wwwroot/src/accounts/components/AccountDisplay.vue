<template>
  <div>
    <span class="d-block">{{ account.name }}</span>
    <span class="d-inline-block mr-2" v-if="account.number">{{ account.number }}</span>
    <span class="d-inline-block" v-if="account.sortCode">{{ account.sortCode }}</span>
  </div>
</template>

<script>
export default {
  name: 'account-display',
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
