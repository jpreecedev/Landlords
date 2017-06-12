<template>  
  <md-input-container>       
    <label :for="name">Select a property from your portfolio</label>
    <md-select v-model="propertyModel" v-bind:disabled="disabled" :name="name">
      <md-option v-for="property in properties" :key="property.id" v-bind:value="property.id">{{ property.propertyReference }}<span v-if="property.propertyStreetAddress"> ({{property.propertyStreetAddress}})</span></md-option>
    </md-select>
  </md-input-container>
</template>

<script>
  export default {
    data () {
      return {
        propertyModel: null
      }
    },
    props: {
      disabled: Boolean,
      name: String,
      properties: Array
    },
    components: {
    },
    methods: {
    },
    watch: {
      propertyModel: function (value) {
        this.$emit('selected', value)
      }
    }
  }
</script>

<style lang="scss" scoped>

</style>
