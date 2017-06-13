<template>
  <md-dialog :ref="journey" :md-open-from="'#' + trigger" >
    <md-dialog-title>{{ title }}</md-dialog-title>
    <md-dialog-content>
      <starttenancy v-if="journey === 'startTenancy'"></starttenancy>
    </md-dialog-content>
    <md-dialog-actions>
      <v-btn @click.native="closeDialog()">Cancel</v-btn>
      <v-btn class="md-primary" @click.native="closeDialog()">Next</v-btn>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
  import startTenancy from 'journeys/StartTenancy'

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

</style>
