<template>
  <v-app>
    <app-nav></app-nav>
    <main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </main>
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
