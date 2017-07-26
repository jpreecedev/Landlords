<template>
  <v-snackbar :timeout="timeout"
              :top="true"
              :bottom="false"
              :right="true"
              :left="false"
              :vertical="false"
              :success="context === 'success'"
              :info="context === 'info'"
              :warning="context === 'warning'"
              :error="context === 'error'"
              :primary="context === 'primary'"
              :secondary="context === 'secondary'"
              v-model="snackbar">
    {{ text }}
    <v-btn dark flat @click="snackbar = false">Dismiss</v-btn>
  </v-snackbar>
</template>

<script>
  export default {
    name: 'name',
    data () {
      return {
        context: 'error',
        timeout: 15000,
        snackbar: false,
        text: null
      }
    },
    methods: {
      show (text, context = 'error', timeout = 5000) {
        this.text = text
        this.context = context
        this.timeout = timeout
        this.snackbar = true
      }
    }
  }
</script>
