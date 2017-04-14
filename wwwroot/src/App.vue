<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: 'app'
}
</script>

<style lang="scss">
@import '../node_modules/bootstrap/dist/css/bootstrap.css';

body {
  padding-top: 50px;
}
#app {
  padding: 40px 15px;
  text-align: center;
}
</style>
