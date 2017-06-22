<template>
  <v-footer :class="{ 'green': $route.path === '/', 'fixed-bottom': $route.path !== '/' }">
    <v-spacer></v-spacer>
    <div>© {{ new Date().getFullYear() }}</div>
  </v-footer>
</template>

<script>
export default {
  data () {
    return {
    }
  }
}
</script>
