<template>
  <v-text-field ref="field"
                @input="updateField"
                @blur="$emit('blur')"
                :value="value"
                :label="label"
                :append-icon="type === 'password' ? (togglePassword ? 'visibility' : 'visibility_off') : null"
                :append-icon-cb="() => (togglePassword = !togglePassword)"
                :type="togglePassword ? 'text' : type"
                :required="required"
                :error-messages="errorMessages"
                :min="min"
                :max="max"
                :step="step"
                :prefix="prefix"
                :suffix="suffix"
                :multi-line="multiline"
                :rows="rows"
                :auto-grow="multiline"
                :placeholder="placeholder"
                :hint="hint"
                :disabled="disabled">
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
      },
      max: {
        type: String,
        default: null,
        required: false
      },
      step: {
        type: String,
        default: null,
        required: false
      },
      prefix: {
        type: String,
        default: null,
        required: false
      },
      suffix: {
        type: String,
        default: null,
        required: false
      },
      multiline: {
        type: Boolean,
        default: false,
        required: false
      },
      rows: {
        type: Number,
        default: 1,
        required: false
      },
      placeholder: {
        type: String,
        default: null,
        required: false
      },
      hint: {
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
