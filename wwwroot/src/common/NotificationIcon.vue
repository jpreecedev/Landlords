<template>
  <v-icon v-if="type === 'Notice'">alarm</v-icon>
  <v-icon class="blue--text" v-else-if="type === 'Important'">add_alert</v-icon>
  <v-icon class="orange--text" v-else-if="type === 'Immediate'">warning</v-icon>
  <v-icon class="red--text" v-else-if="type === 'Overdue'">error_outline</v-icon>
  <v-icon class="green--text" v-else-if="type === 'Message'">message</v-icon>
  <v-icon v-else>info_outline</v-icon>
</template>

<script>
  export default {
    name: 'notification-icon',
    props: {
      'type': {
        type: String,
        required: true
      }
    }
  }
</script>
