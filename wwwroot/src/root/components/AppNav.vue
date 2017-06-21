<template>
  <v-toolbar class="fixed-top" :class="$route.path === '/' ? 'green' : ''" light>
    <v-toolbar-title class="hidden-sm-and-down">Landlords</v-toolbar-title>

    <v-toolbar-items>
      <router-link class="toolbar__item" v-if="auth.isLoggedIn" to="/dashboard">
        Dashboard
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.LL_List" to="/agency/landlord-list">
        Landlord List
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.PD_GetList" to="/manager/property-list">
        Property List
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.PE_List" to="/permissions">
        Permissions
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.CL_Overview" to="/checklists/">
        Checklists
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.TE_GetListById" to="/tenants/">
        Tenants
      </router-link>
      <router-link class="toolbar__item" v-if="!auth.isLoggedIn" to="/registration/">
        Log in or Register
      </router-link>
      <router-link class="toolbar__item" v-if="permissions.AC_Overview" to="/accounts/">
        Accounts
      </router-link>

      <v-menu offset-y transition="v-slide-y-transition">
        <v-toolbar-item slot="activator">
          Calculators
        </v-toolbar-item>
        <v-list>
          <v-list-item>
            <v-list-tile @click.native="$router.push('/calculators/rental-yield')">
              Rental Yield
            </v-list-tile>
          </v-list-item>
          <v-list-item>
            <v-list-tile @click.native="$router.push('/calculators/monthly-payment')">
              Monthly Payment
            </v-list-tile>
          </v-list-item>
          <v-list-item>
            <v-list-tile @click.native="$router.push('/calculators/how-much-can-i-borrow')">
              How much can I borrow?
            </v-list-tile>
          </v-list-item>
          <v-list-item>
            <v-list-tile @click.native="$router.push('/calculators/is-this-property-a-good-investment')">
              Is this property a good investment?
            </v-list-tile>
          </v-list-item>
        </v-list>
      </v-menu>

      <v-menu offset-y transition="v-slide-y-transition">
        <v-btn light icon slot="activator">
          <v-icon>more_vert</v-icon>
        </v-btn>
        <v-list>
          <v-list-item v-if="auth.isLoggedIn" @click.native="logout()">
            Log out
          </v-list-item>
          <v-list-item v-if="permissions.P_View" @click.native="$router.push('/profile')">
            View profile
          </v-list-item>
        </v-list>
      </v-menu>
    </v-toolbar-items>

  </v-toolbar>
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

</style>
