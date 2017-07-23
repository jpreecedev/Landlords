export default {
  props: {
    value: {
      type: String | Number,
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
      default: () => [],
      required: false
    },
    type: {
      type: String,
      default: 'text',
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
    this.$refs.field.lazyValue = this.$refs.field.inputValue = this.value
  },
  methods: {
    updateField (newValue) {
      this.$emit('input', newValue)
    },
    validate (force) {
      this.errorMessages = []
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
      this.$refs.field.lazyValue = this.$refs.field.inputValue = this.initialValue
    },
    commit () {
      this.initialValue = this.$refs.field.lazyValue = this.$refs.field.inputValue
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
