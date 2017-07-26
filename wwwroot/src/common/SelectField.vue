<template>
  <v-select ref="field"
            @input="updateField"
            :items="items"
            :value="value"
            :label="label"
            :item-value="itemValue"
            :required="required"
            :error-messages="errorMessages"
            :disabled="disabled">
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
      itemText: {
        type: String,
        default: 'text',
        required: false
      },
      'item-value': {
        type: String,
        default: 'value',
        required: false
      }
    }
  }
</script>
