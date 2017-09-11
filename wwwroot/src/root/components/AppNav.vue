<template>
  <div>
    <v-navigation-drawer v-model="drawer"
                         light overflow
                         class="hidden-md-and-up">
      <v-toolbar flat class="transparent">
        <v-list>
          <v-list-tile>
            <v-list-tile-content>
              <v-list-tile-title>HomesInOne.co.uk</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
      </v-toolbar>
      <v-list class="pt-0" dense>
        <v-divider></v-divider>
        <v-list-tile v-for="item in items" :key="item.title">
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  <v-toolbar class="primary" :class="$route.path === '/' ? 'green' : ''" dark fixed dense>
    <v-toolbar-side-icon @click.stop="drawer = !drawer"
                         class="hidden-md-and-up toolbar-trigger">
    </v-toolbar-side-icon>
    <v-toolbar-title>
      <router-link :to="auth.isLoggedIn ? '/dashboard' : '/'" tag="a" class="home">
        <v-icon class="hidden-sm-and-down white--text">home</v-icon>
        <span class="white--text">HomesInOne.co.uk</span>
      </router-link>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-xs-only">
      <v-btn flat v-if="auth.isLoggedIn" @click="$router.push('/dashboard')" :class="{'active': $route.path.startsWith('/dashboard')}">
        Dashboard
      </v-btn>
      <v-btn flat v-if="permissions.LL_List" @click="$router.push('/agency/landlord-list')" :class="{'active': $route.path.startsWith('/agency/landlord-list')}">
        Landlord List
      </v-btn>
      <v-btn flat v-if="permissions.PD_GetList" @click="$router.push('/manager/property-list')" :class="{'active': $route.path.startsWith('/manager/property-list') || $route.path.startsWith('/manager/property-details')}">
        Properties
      </v-btn>
      <v-btn flat v-if="permissions.TT_GetListById" @click="$router.push('/tenancies/')" :class="{'active': $route.path.startsWith('/tenancies')}">
        Tenancies
      </v-btn>
      <v-btn flat v-if="permissions.PE_List" @click="$router.push('/permissions')" :class="{'active': $route.path.startsWith('/permissions')}">
        Permissions
      </v-btn>
      <v-btn flat v-if="permissions.CL_Overview" @click="$router.push('/checklists/')" :class="{'active': $route.path.startsWith('/checklists')}">
        Checklists
      </v-btn>
      <v-btn flat v-if="permissions.CO_View" @click="$router.push('/conversations')" :class="{'active': $route.path.startsWith('/conversations')}">
        Chat
      </v-btn>
      <v-btn flat v-if="permissions.MR_Overview" @click="$router.push('/maintenance')" :class="{'active': $route.path.startsWith('/maintenance')}">
        Maintenance &amp; Repairs
      </v-btn>
      <v-btn flat v-if="!auth.isLoggedIn" @click="$router.push('/registration/')" :class="{'active': $route.path.startsWith('/registration')}">
        Log in or Register
      </v-btn>
      <v-btn flat v-if="permissions.AC_Overview" @click="$router.push('/accounts/')" :class="{'active': $route.path.startsWith('/accounts')}">
        Accounts
      </v-btn>

      <v-menu class="calculators" transition="v-slide-y-transition" bottom :nudge-right="90" :nudge-top="-10">
        <v-btn flat class="primary" slot="activator" :class="{'active': $route.path.startsWith('/calculators')}">
          Calculators
        </v-btn>
        <v-list>
          <v-list-tile @click="$router.push('/calculators/rental-yield')">
            <v-list-tile-title>Rental Yield</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="$router.push('/calculators/monthly-payment')">
            <v-list-tile-title>Monthly Payment</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="$router.push('/calculators/how-much-can-i-borrow')">
            <v-list-tile-title>How much can I borrow?</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="$router.push('/calculators/is-this-property-a-good-investment')">
            <v-list-tile-title>Is this property a good investment?</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="$router.push('/calculators/return-on-investment')">
            <v-list-tile-title>Return on investment (ROI)</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>

      <template v-if="notifications && notifications.length">
        <v-menu v-if="auth.isLoggedIn" transition="v-slide-y-transition" bottom :nudge-right="notifications.length > 0 ? 260 : 90" :nudge-top="-10">
          <v-btn @click="checkNotifications()" icon slot="activator">
            <v-icon class="red--after notifications" v-badge="{ value: notifications.length }">add_alert</v-icon>
          </v-btn>
          <v-list class="notifications-list" two-line v-if="notifications.length > 0">
            <v-subheader>Notifications</v-subheader>
            <template v-for="(notification, index) in notifications">
              <v-list-tile avatar :key="notification.id" download target="_blank">
                <v-list-tile-avatar>
                  <notification-icon :type="notification.type"></notification-icon>
                </v-list-tile-avatar>
                <v-list-tile-content>
                  <v-list-tile-title>{{notification.message}}</v-list-tile-title>
                  <v-list-tile-sub-title v-if="notification.secondaryMessage">
                    {{ notification.secondaryMessage }}
                  </v-list-tile-sub-title>
                </v-list-tile-content>
              </v-list-tile>
            </template>
          </v-list>
          <v-list v-else>
            <v-list-tile>
              <v-list-tile-content>
                <v-list-tile-title></v-list-tile-title>
                <v-list-tile-sub-title>You have no notifications</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
        </v-menu>
      </template>

      <v-menu v-if="auth.isLoggedIn" class="more" transition="v-slide-y-transition" bottom :nudge-right="85" :nudge-top="-10">
        <v-btn icon slot="activator">
          <v-icon>more_vert</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile @click="$router.push('/watchlist')" v-if="permissions.SP_GetListById">
            <v-list-tile-title>
              My shortlist
            </v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="$router.push('/profile')" v-if="permissions.P_View">
            <v-list-tile-title>View profile</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="logout()">
            <v-list-tile-title>Log out</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
    </v-toolbar-items>

  </v-toolbar>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  computed: {
    ...mapState({
      notifications: state => state.notifications,
      user: state => state.user,
      permissions: state => state.permissions,
      auth: state => state.auth
    })
  },
  data () {
    return {
      drawer: false,
      items: [
        { title: 'Home', icon: 'dashboard' },
        { title: 'About', icon: 'question_answer' }
      ]
    }
  },
  mounted () {
    this.checkNotifications()
  },
  methods: {
    updateStore (data) {
      this.$store.commit('UPDATE_NOTIFICATIONS', data)
    },
    checkNotifications () {
      if (!this.auth.isLoggedIn) {
        return
      }

      this.$notifications.open(this.auth.accessToken)
        .then(() => {
          this.$notifications.invoke('GetAllNotifications').then(this.updateStore)
          this.$bus.$on('GetAllNotifications', this.updateStore)
        })
    },
    logout () {
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

    button {
      margin: 0;
      height: 100%;
    }
  }

  .more > .menu__activator .btn__content {
    padding: 0;
  }

  .home {
    text-decoration: none;

    i {
      font-size: 2rem;
      height: 32px;
      padding-bottom: 4px;
    }
  }

  .notifications-list {
    .list__tile {
      padding-right: 3rem;
    }
  }

  .notifications {
    &::after {
      top: -7px;
      right: -10px;
    }
  }

</style>
