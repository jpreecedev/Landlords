<template>
  <v-menu ref="datePickerField"
          lazy
          :nudge-left="100">
    <v-text-field slot="activator"
                  @input="updateField"
                  :label="label"
                  :required="required"
                  :value="value"
                  :error-messages="errorMessages"
                  prepend-icon="date_range"
                  readonly>
    </v-text-field>
    <v-date-picker :value="value"
                    @input="updateField"
                    scrollable>
    </v-date-picker>
  </v-menu>
</template>

<script>
  export default {
    name: 'select-list',
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
      }
    },
    data () {
      return {
        errorMessages: [],
        initialValue: null,
        isDirty: false
      }
    },
    mounted () {
      this.initialValue = this.value
      this.$refs.datePickerField.lazyValue = this.$refs.datePickerField.inputValue = this.value
    },
    methods: {
      updateField (newValue) {
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

        return this.errorMessages.length === 0
      },
      reset () {
        this.$refs.datePickerField.lazyValue = this.$refs.datePickerField.inputValue = this.initialValue
      },
      commit () {
        this.initialValue = this.$refs.datePickerField.lazyValue = this.$refs.datePickerField.inputValue
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
