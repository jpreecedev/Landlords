<template>
  <div>
    <h2 class="title">Confirm the identify of each tenant</h2>

  </div>
</template>

<script>
  import { mapState } from 'vuex'

  export default {
    name: 'tenants',
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    },
    computed: {
      ...mapState({
        tenants: state => state.newTenancy.tenants
      })
    }
  }

</script>
