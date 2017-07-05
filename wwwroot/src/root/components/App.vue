<template>
  <v-app>
    <app-nav></app-nav>
    <app-header v-if="$route.path === '/'"></app-header>
    <main :class="{ contained: $route.path !== '/', wide: $route.path.indexOf('/accounts/transactions') > -1 }">
      <router-view></router-view>
    </main>
    <app-footer></app-footer>
  </v-app>
</template>

<script>
import AppNav from './AppNav.vue'
import AppFooter from './AppFooter.vue'
import AppHeader from './Header.vue'

export default {
  name: 'app',
  components: { AppNav, AppFooter, AppHeader }
}
</script>

<style lang="scss">

.application {
  height: 100%;
}

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
