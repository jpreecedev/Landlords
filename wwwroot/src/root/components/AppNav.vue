<template>
  <nav>
    <md-toolbar class="md-medium">

      <md-button class="md-icon-button">
        <md-icon>menu</md-icon>
      </md-button>

      <h2 class="md-title">Landlords</h2>
      <router-link tag="button" class="md-button" v-if="auth.isLoggedIn" to="/dashboard">Dashboard</router-link>
      <router-link v-if="permissions.LL_List" tag="button" class="md-button" to="/agency/landlord-list">
        Landlord List
      </router-link>
      <router-link v-if="!auth.isLoggedIn" tag="button" class="md-button" to="/registration">
        Registration
      </router-link>
      <router-link v-if="permissions.PD_GetList" tag="button" class="md-button" to="/manager/property-list">
        Property List
      </router-link>
      <router-link v-if="permissions.PE_List" tag="button" class="md-button" to="/permissions">
        Permissions
      </router-link>
      <router-link v-if="permissions.CL_Overview" tag="button" class="md-button" to="/checklists/">
        Checklists
      </router-link>

      <md-menu md-size="6" md-direction="bottom left">
        <md-button md-menu-trigger>Calculators</md-button>

        <md-menu-content>
          <md-menu-item @click.native="$router.push('/calculators/rental-yield')">
            Rental Yield
          </md-menu-item>
          <md-menu-item @click.native="$router.push('/calculators/monthly-payment')">
            Monthly Payment
          </md-menu-item>
          <md-menu-item @click.native="$router.push('/calculators/how-much-can-i-borrow')">
            How much can I borrow?
          </md-menu-item>
          <md-menu-item @click.native="$router.push('/calculators/is-this-property-a-good-investment')">
            Is this property a good investment?
          </md-menu-item>
        </md-menu-content>
      </md-menu>

      <router-link v-if="permissions.AC_Overview" tag="button" class="md-button" to="/accounts/">
        Accounts
      </router-link>

      <template v-if="auth.isLoggedIn">
        <router-link v-if="permissions.P_View" tag="md-avatar" class="md-avatar" to="/profile">
          <img src="../../assets/images/avatar.jpg" alt="Avatar">
        </router-link>        
      </template>

      <md-button class="md-icon-button">
        <md-icon>more_vert</md-icon>
      </md-button>

    </md-toolbar>
  </nav>
</template>

<script>
export default {
  data () {
    return {
      user: this.$store.state.user,
      permissions: this.$store.state.permissions,
      auth: this.$store.state.auth
    }
  },
  methods: {
    logout: function () {
      this.$auth.logout(false)
    }
  }
}
</script>

<style lang="scss" scoped>

  .md-title {
    flex: 1;
  }

</style>
