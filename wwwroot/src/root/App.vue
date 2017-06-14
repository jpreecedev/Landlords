<template>
  <v-app light>
    <app-nav></app-nav>
    <router-view></router-view>
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
