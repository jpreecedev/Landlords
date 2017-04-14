<template>
  <div id="app">
    <app-nav></app-nav>
    <div class="main container">
      <router-view></router-view>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>

import AppNav from './AppNav.vue'
import AppFooter from './AppFooter.vue'

export default {
  name: 'app',
  components: { AppNav, AppFooter },
  data () {
    return {
    }
  }
}

</script>

<style lang="scss">

body {
  padding-top: 5rem;
}

.main {
  padding: 3rem 1.5rem;
}

</style>
