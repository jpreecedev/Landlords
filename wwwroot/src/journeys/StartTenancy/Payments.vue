<template>
  <md-tab md-icon="attach_money">
    <div class="row">
      <div class="col-xs-12 col-md-6">
      </div>
      <div class="col-xs-12 col-md-6">

      </div>
    </div>
  </md-tab>
</template>

<script>
  export default {
    name: 'payments',
    props: {
      viewdata: Object
    },
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    },
    created () {
    },
    methods: {
    }
  }

</script>
