<template>
  <v-footer class="fixed-bottom" :class="$route.path === '/' ? 'green' : ''">
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
