<template>
  <div>
    <p v-if="errors.length > 0" class="red--text">
      {{ errors[0].message }}
    </p>
  </div>
</template>

<script>
  export default {
    name: 'validation-summary',
    props: {
      'errors': {
        type: Array,
        default: []
      }
    }
  }
</script>
