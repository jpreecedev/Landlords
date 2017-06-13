<template>
  <v-alert error :value="!permission">
    Sorry this resource is currently unavailable
  </v-alert>
</template>

<script>
export default {
  name: 'permissions-warning',
  props: {
    'permission': {
      type: Object,
      default: null
    }
  },
  data () {
    return {
    }
  }
}
</script>
