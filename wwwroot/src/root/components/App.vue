<template>
  <v-app light :class="{ landingPage: $route.path === '/' }">
    <app-nav></app-nav>
    <app-header v-if="$route.path === '/'"></app-header>
    <notification ref="notification"></notification>
    <main :class="{ contained: $route.path !== '/', wide: $route.path.indexOf('/accounts/transactions') > -1 }">
      <transition appear name="fade">
        <router-view></router-view>
      </transition>
    </main>
  </v-app>
</template>

<script>
import AppNav from './AppNav.vue'
import AppFooter from './AppFooter.vue'
import AppHeader from './Header.vue'

export default {
  name: 'app',
  components: { AppNav, AppFooter, AppHeader },
  mounted () {
    this.$bus.$on('SHOW_VALIDATION_NOTIFICATION', () => {
      this.$refs.notification.show('Please fix the form validation issues before continuing.  Errors are highlighted in red.', 'error')
    })
    this.$bus.$on('SHOW_NOTIFICATION', data => {
      this.$refs.notification.show(data.message, data.context, data.timeout)
    })
  }
}
</script>

<style lang="scss">

.application.landingPage {
  height: 100%;
}

.fade-enter-active {
  transition: all .3s ease;
}
.fade-enter {
  transform: translateX(10px);
  opacity: 0;
}
.fade-leave-to, .fade-leave-active {
  opacity: 0
}

</style>
