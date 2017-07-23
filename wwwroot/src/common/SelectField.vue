<template>
  <v-select ref="selectField"
            @input="updateField"
            :items="items"
            :value="value"
            :label="label"
            :item-value="itemValue"
            :required="required"
            :error-messages="errorMessages">
  </v-select>
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
      },
      items: {
        type: Array,
        default: [],
        required: true
      },
      itemValue: {
        type: String,
        default: 'value',
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
      this.$refs.selectField.lazyValue = this.$refs.selectField.inputValue = this.value
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
        this.$refs.selectField.lazyValue = this.$refs.selectField.inputValue = this.initialValue
      },
      commit () {
        this.initialValue = this.$refs.selectField.lazyValue = this.$refs.selectField.inputValue
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
