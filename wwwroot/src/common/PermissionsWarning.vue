<template>
  <div v-if="!permission" class="alert alert-danger">
    Sorry this resource is currently unavailable
  </div>  
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
