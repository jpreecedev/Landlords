<template>
    <v-progress-circular indeterminate
                         :size="70"
                         :class="[{'d-none': !loading }, 'primary--text center']">
    </v-progress-circular>
</template>

<script>
  export default {
    name: 'loader',
    props: {
      'loading': {
        type: Boolean,
        default: false
      }
    }
  }
</script>
