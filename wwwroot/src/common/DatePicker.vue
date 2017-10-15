<template>
  <v-menu ref="field"
          lazy
          offset-y
          full-width
          :nudge-left="40"
          :nudge-top="60"
          max-width="290px">

    <div class="input-group formatted input-group--dirty input-group--prepend-icon input-group--text-field"
         slot="activator">
      <label>{{ label }}</label>
      <i aria-hidden="true" class="material-icons icon input-group__prepend-icon">date_range</i>
      <span>{{ formatted }}</span>
      <div class="input-group__details">
        <div class="input-group__messages"></div>
      </div>
    </div>
    <v-date-picker :value="value"
                   :date-format="date => new Date(date).toDateString()"
                   :formatted-value.sync="formatted"
                   @input="updateField"
                   scrollable>
    </v-date-picker>
  </v-menu>
</template>

<script>
  import field from './field'
  export default {
    mixins: [field],
    name: 'date-picker',
    data () {
      return {
        formatted: null
      }
    },
    methods: {
      updateFormatted () {
        this.formatted = this.value ? new Date(this.value).toDateString() : new Date().toDateString()
      }
    },
    mounted () {
      this.updateFormatted()
    },
    watch: {
      value () {
        this.updateFormatted()
      }
    }
  }
</script>

<style lang="scss" scoped>
  .input-group.formatted {
    span {
      padding-top: 4px;
      font-size: 16px;
      height: 30px;
    }
  }
</style>

