<template>
  <v-footer :class="{ 'green': $route.path === '/', 'fixed-bottom': $route.path !== '/' }">
    <v-btn flat warning dark @click.native="$store.commit('CLEAR_ALL_DATA')">Clear Store</v-btn>
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
