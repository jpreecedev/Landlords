<template>
  <div>
    <h1 class="display-2">Dashboard</h1>
    <starttenancy v-if="permissions.J_StartTenancy"></starttenancy>
  </div>
</template>

<script>
export default {
  name: 'dashboard',
  data () {
    return {
      permissions: this.$store.state.permissions
    }
  },
  methods: {
    openDialog: function (dialog) {
      this.$refs[dialog].openDialog()
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
