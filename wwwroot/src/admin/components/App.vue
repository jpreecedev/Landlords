<template>
  <div id="app">
    Hello, from admin
    <router-view></router-view>
  </div>
</template>

<script>

export default {
  name: 'app'
}

</script>

<style lang="scss">

.fade-enter-active {
  transition: all .3s ease;
}
.fade-enter {
  transform: translateY(10px);
  opacity: 0;
}
.fade-leave-to, .fade-leave-active {
  opacity: 0
}

</style>
