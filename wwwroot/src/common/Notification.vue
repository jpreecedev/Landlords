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
    <v-btn dark flat @click.native="snackbar = false">Dismiss</v-btn>
  </v-snackbar>
</template>

<script>
  export default {
    name: 'name',
    props: {
      context: {
        type: String,
        default: 'primary',
        required: false
      }
    },
    data () {
      return {
        timeout: 15000,
        snackbar: false,
        text: null
      }
    },
    methods: {
      show (text) {
        this.text = text
        this.snackbar = true
      }
    }
  }
</script>
