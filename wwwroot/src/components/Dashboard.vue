<template>
  <div>
    <h1>Hello, Authenticated World!</h1>
    <button class="btn btn-primary" @click="callServer()">Make authenticated call</button>
  </div>
</template>

<script>
  export default {
    name: 'dashboard',
    methods: {
      callServer () {
        this.$http.get('/api/PropertyOverview').then((response) => {
          debugger
        })
      }
    }
  }
</script>

<style lang="scss" scoped>

</style>
