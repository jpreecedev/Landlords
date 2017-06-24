<template>
  <div>
    <slot></slot>
  </div>
</template>

<script>
  import Vue from 'vue'
  export default {
    props: {
      value: {
        type: Boolean,
        required: true
      }
    },
    data () {
      return {
        errors: {}
      }
    },
    watch: {
      errors: {
        handler () {
          const errors = Object.keys(this.errors).reduce((err, key) => {
            return err || this.errors[key]
          }, false)
          this.$emit('input', !errors)
        },
        deep: true
      }
    },
    methods: {
      validate () {
        // Remove keys of any inputs that have been removed
        const ids = this.inputs.map(input => input._uid)
        Object.keys(this.errors).forEach((key) => {
          if (!ids.includes(key)) {
            Vue.delete(this.errors, key)
          }
        })
        this.inputs.forEach((child) => {
          child.validate()
        })
      }
    },
    computed: {
      inputs () {
        return this.$children.filter(child => child.hasOwnProperty('errors'))
      }
    },
    mounted () {
      this.$children.forEach((child) => {
        Vue.set(this.errors, child._uid, true)
        if (child.hasOwnProperty('errors')) {
          child.$watch('errors', (errors) => {
            Vue.set(this.errors, child._uid, errors.length > 0)
          })
        }
      })
    }
  }
</script>
