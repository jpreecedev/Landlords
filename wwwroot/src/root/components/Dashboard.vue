<template>
  <main>
    <h1 class="md-display-4">Landlord World!</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rem quae, quas reiciendis earum ad eius cum molestiae possimus fuga ipsam nobis, tempora sed et, dicta tempore odio repellat ratione, aspernatur.</p>

    <!--<modal ref="modal" trigger="startNewTenancyButton" journey="startTenancy" title="Start a new tenancy" />
    <md-button class="md-primary md-raised" id="startNewTenancyButton" @click.native="openDialog('modal')">Start a new tenancy</md-button>-->

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
