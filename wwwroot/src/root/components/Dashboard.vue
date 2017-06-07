<template>
  <main>
    <h1 class="md-display-4">Landlord World!</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rem quae, quas reiciendis earum ad eius cum molestiae possimus fuga ipsam nobis, tempora sed et, dicta tempore odio repellat ratione, aspernatur.</p>
  </main>
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
