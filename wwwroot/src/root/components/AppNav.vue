<template>
  <v-toolbar class="fixed-top primary" :class="$route.path === '/' ? 'green' : ''" dark>
    <v-toolbar-title class="hidden-sm-and-down">Landlords</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items>
      <v-btn flat v-if="auth.isLoggedIn" @click.native="$router.push('/dashboard')">
        Dashboard
      </v-btn>
      <v-btn flat v-if="permissions.LL_List" @click.native="$router.push('/agency/landlord-list')">
        Landlord List
      </v-btn>
      <v-btn flat v-if="permissions.PD_GetList" @click.native="$router.push('/manager/property-list')">
        Property List
      </v-btn>
      <v-btn flat v-if="permissions.PE_List" @click.native="$router.push('/permissions')">
        Permissions
      </v-btn>
      <v-btn flat v-if="permissions.CL_Overview" @click.native="$router.push('/checklists/')">
        Checklists
      </v-btn>
      <v-btn flat v-if="permissions.TE_GetListById" @click.native="$router.push('/tenants/')">
        Your Tenants
      </v-btn>
      <v-btn flat v-if="!auth.isLoggedIn" @click.native="$router.push('/registration/')">
        Log in or Register
      </v-btn>
      <v-btn flat v-if="permissions.AC_Overview" @click.native="$router.push('/accounts/')">
        Accounts
      </v-btn>

      <v-menu class="calculators" transition="v-slide-y-transition" bottom :nudge-right="90" :nudge-top="-10">
        <v-btn flat class="primary" slot="activator">
          Calculators
        </v-btn>
        <v-list>
          <v-list-tile @click.native="$router.push('/calculators/rental-yield')">
            <v-list-tile-title>Rental Yield</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click.native="$router.push('/calculators/monthly-payment')">
            <v-list-tile-title>Monthly Payment</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click.native="$router.push('/calculators/how-much-can-i-borrow')">
            <v-list-tile-title>How much can I borrow?</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click.native="$router.push('/calculators/is-this-property-a-good-investment')">
            <v-list-tile-title>Is this property a good investment?</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>

      <v-menu v-if="auth.isLoggedIn" class="more"  transition="v-slide-y-transition" bottom :nudge-right="90" :nudge-top="-10">
        <v-btn icon slot="activator">
          <v-icon>more_vert</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile @click.native="logout()">
            <v-list-tile-title>Log out</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click.native="$router.push('/profile')" v-if="permissions.P_View">
            <v-list-tile-title>View profile</v-list-tile-title>
          </v-list-tile>
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

<style lang="scss">
  .calculators, .calculators a {
    height: 100%;
  }

  .calculators > .menu__activator, .more > .menu__activator {
    height: 100%;
    display: flex;
  }

  .more > .menu__activator .btn__content {
    padding: 0;
  }

</style>
