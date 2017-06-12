<template>
  <main>
    <h1 class="md-display-4">Landlord World!</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rem quae, quas reiciendis earum ad eius cum molestiae possimus fuga ipsam nobis, tempora sed et, dicta tempore odio repellat ratione, aspernatur.</p>

    <wizard ref="wizard" trigger="startNewTenancyButton" journey="startTenancy">
      <starttenancyjourney></starttenancyjourney>
    </wizard>

    <md-button class="md-primary md-raised" id="startNewTenancyButton" @click.native="openDialog('wizard')">Start a new tenancy</md-button>

  </main>
</template>

<script>
import startTenancyJourney from 'wizard-journeys/StartTenancy'

export default {
  name: 'dashboard',
  components: { 'starttenancyjourney': startTenancyJourney },
  methods: {
    callServer: function () {
      this.$http.get('/api/PropertyDetails').then((response) => {
        debugger
      })
    },
    openDialog: function (dialog) {
      this.$refs[dialog].openDialog()
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
