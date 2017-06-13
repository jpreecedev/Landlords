<template>
  <main>
    <h1 class="md-display-4">Landlord World!</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rem quae, quas reiciendis earum ad eius cum molestiae possimus fuga ipsam nobis, tempora sed et, dicta tempore odio repellat ratione, aspernatur.</p>

    <starttenancy v-if="permissions.J_StartTenancy" />

  </main>
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
