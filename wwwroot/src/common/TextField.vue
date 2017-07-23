<template>
  <v-text-field ref="textField"
                @input="updateText"
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
  export default {
    name: 'text-field',
    props: {
      value: {
        type: String,
        default: null
      },
      label: {
        type: String,
        default: null,
        required: false
      },
      required: {
        type: Boolean,
        default: false,
        required: false
      },
      rules: {
        type: Array,
        default: [],
        required: false
      },
      type: {
        type: String,
        default: 'text',
        required: false
      },
      min: {
        type: String,
        default: null,
        required: false
      }
    },
    data () {
      return {
        errorMessages: [],
        initialValue: null,
        isDirty: false,
        togglePassword: false
      }
    },
    mounted () {
      this.initialValue = this.value
      this.$refs.textField.lazyValue = this.$refs.textField.inputValue = this.value
    },
    methods: {
      updateText (newValue) {
        this.$emit('input', newValue)
      },
      validate (force) {
        this.errorMessages.length = 0
        if (this.isDirty || force) {
          this.$props.rules.forEach(rule => {
            let invokedRule = rule
            if (typeof invokedRule === 'function') {
              invokedRule = invokedRule.apply(this, [this.value])
            }
            if (typeof invokedRule === 'string') {
              this.errorMessages.push(invokedRule)
            }
          })
        }
      },
      reset () {
        this.$refs.textField.lazyValue = this.$refs.textField.inputValue = this.initialValue
      }
    },
    watch: {
      value () {
        this.isDirty = this.value !== this.initialValue
        this.validate()
      }
    },
    computed: {
      isValid () {
        return this.errorMessages.length === 0
      }
    }
  }
</script>
