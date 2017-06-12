<template>  
  <md-dialog :ref="journey" :md-open-from="'#' + trigger" >
    <div class="wrapper" v-on:closeDialog="closeDialog">
      <slot></slot>      
      <md-dialog-actions>
        <md-button @click.native="closeDialog()">Cancel</md-button>
        <md-button class="md-primary" @click.native="closeDialog()">Next</md-button>
      </md-dialog-actions>
    </div>
  </md-dialog>
</template>

<script>
  export default {
    props: {
      'journey': String,
      'trigger': String
    },
    methods: {
      openDialog: function () {
        this.$refs[this.journey].open()
      },
      closeDialog: function () {
        this.$refs[this.journey].close()
      }
    }
  }
</script>

<style lang="scss" scoped>

</style>
