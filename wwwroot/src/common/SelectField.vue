<template>
  <v-select ref="field"
            @input="updateField"
            :items="items"
            :value="value"
            :label="label"
            :item-text="itemText"
            :item-value="itemValue"
            :required="required"
            :error-messages="errorMessages"
            :return-object="returnObject"
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
      itemValue: {
        type: String,
        default: 'value',
        required: false
      },
      returnObject: {
        type: Boolean,
        default: false,
        required: false
      }
    }
  }
</script>
