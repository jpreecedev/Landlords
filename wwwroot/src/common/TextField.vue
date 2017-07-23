<template>
  <v-text-field ref="field"
                @input="updateField"
                :value="value"
                :label="label"
                :append-icon="type === 'password' ? (togglePassword ? 'visibility' : 'visibility_off') : null"
                :append-icon-cb="() => (togglePassword = !togglePassword)"
                :type="togglePassword ? 'text' : type"
                :required="required"
                :error-messages="errorMessages"
                :min="min">
  </v-text-field>
</template>

<script>
  import field from './field'
  export default {
    mixins: [field],
    name: 'text-field',
    props: {
      min: {
        type: String,
        default: null,
        required: false
      }
    },
    data () {
      return {
        togglePassword: false
      }
    }
  }
</script>
