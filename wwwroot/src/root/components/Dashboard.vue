<template>
  <div>
    <div class="jumbotron">
      <div class="container">
        <h1 class="display-3">Landlord World!</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rem quae, quas reiciendis earum ad eius cum molestiae possimus fuga ipsam nobis, tempora sed et, dicta tempore odio repellat ratione, aspernatur.</p>
      </div>
    </div>
    <main class="container">
      <h1>Hello, Authenticated World!</h1>
      <md-button class="md-raised md-primary" @click="callServer()">Make authenticated call</md-button>
    </main>
  </div>
</template>

<script>
  export default {
    name: 'dashboard',
    methods: {
      callServer: function () {
        this.$http.get('/api/PropertyDetails').then((response) => {
          debugger
        })
      }
    }
  }
</script>

<style lang="scss" scoped>

</style>
