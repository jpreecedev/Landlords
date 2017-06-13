<template>
  <v-app light>
    <app-nav></app-nav>
    <transition name="fade">
      <router-view></router-view>
    </transition>
    <app-footer></app-footer>
  </v-app>
</template>

<script>

import AppNav from './AppNav.vue'
import AppFooter from './AppFooter.vue'

export default {
  name: 'app',
  components: { AppNav, AppFooter },
  data () {
    return {
      app: this.$store.state.app
    }
  }
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
