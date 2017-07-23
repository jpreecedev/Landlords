<template>
  <v-menu ref="field"
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
  import field from './field'
  export default {
    mixins: [field],
    name: 'date-picker'
  }
</script>
