<template>
  <v-select v-bind:items="properties" v-model="propertyModel" item-value="id" label="Select a property from your portfolio" dark single-line auto></v-select>
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
    watch: {
      propertyModel: function (value) {
        this.$emit('selected', value)
      }
    }
  }
</script>

<style lang="scss" scoped>

</style>
