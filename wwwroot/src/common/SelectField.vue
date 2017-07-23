<template>
  <v-select ref="field"
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
  import field from './field'
  export default {
    mixins: [field],
    name: 'select-list',
    props: {
      items: {
        type: Array,
        default: () => [],
        required: true
      },
      itemValue: {
        type: String,
        default: 'value',
        required: false
      }
    }
  }
</script>
