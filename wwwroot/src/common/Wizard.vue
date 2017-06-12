<template>  
  <md-dialog :ref="journey" :md-open-from="'#' + trigger" >
    <md-dialog-title>{{ title }}</md-dialog-title>
    <md-dialog-content>
      <starttenancy v-if="journey === 'startTenancy'"></starttenancy>
    </md-dialog-content>
    <md-dialog-actions>
      <md-button @click.native="closeDialog()">Cancel</md-button>
      <md-button class="md-primary" @click.native="closeDialog()">Next</md-button>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
  import startTenancy from 'wizard-journeys/StartTenancy'

  export default {
    props: {
      'journey': String,
      'trigger': String,
      'title': String
    },
    components: {
      'starttenancy': startTenancy
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

<style lang="scss">

  .md-tabs .md-tabs-content {
    width: 600px;
  }

</style>
