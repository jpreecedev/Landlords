<template>
  <div>
    <header>
      <h1 class="headline primary--text">Your active tenancies</h1>
      <p class="display-2 grey--text text--darken-1">There are no notifications or upcoming events</p>
      <p class="subheading">Click on a tenancy to view it.</p>
    </header>
    <v-card>
      <v-card-title class="title">
        Tenancies
      </v-card-title>
      <v-data-table :headers="headers" :items="data" :loading="isLoading">
        <template slot="headers" scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" scope="props">
          <td>
            <router-link v-if="permissions.TT_GetById" :to="'/tenancies/details/' + props.item.tenancyId">
              <span>{{ props.item.leadTenant }}</span>
            </router-link>
            <span v-else>{{ props.item.leadTenant }}</span>
          </td>
          <td>
            <span>{{ props.item.tenantPhoneNumber }} <span v-if="props.item.tenantSecondaryPhoneNumber">or {{ props.item.tenantSecondaryPhoneNumber }}</span></span>
            <template v-if="props.item.emailAddress">
              <br/>
              <span>
                {{ props.item.tenantEmailAddress }}
              </span>
            </template>
          </td>
          <td>
            {{ props.item.propertyReference }}
          </td>
          <td>
            {{ props.item.tenancyEndDate | formatDate }}
        </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
      <v-btn v-if="permissions.J_StartTenancy"
             @click="$router.push('/tenancies/new')"
             class="blue darken-2 action-button"
             absolute dark fab top right>
        <v-icon>add</v-icon>
      </v-btn>
    </v-card>

    <v-btn primary v-if="permissions.J_StartTenancy" type="button" class="mt-4 no-left-margin" @click="$router.push('/tenancies/new')">
      <v-icon dark>add</v-icon>
      Start new tenancy
    </v-btn>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'tenancies-overview',
  data () {
    return {
      isLoading: false,
      pagination: {},
      headers: [
        {
          text: 'Lead Tenant',
          left: true,
          sortable: false
        },
        {
          text: 'Tenant Contact Details',
          left: true,
          sortable: false
        },
        {
          text: 'Property Reference',
          left: true,
          sortable: false
        },
        {
          text: 'Tenancy End Date',
          left: true,
          sortable: false
        }
      ],
      data: []
    }
  },
  mounted () {
    this.isLoading = true
    this.$http.get('/api/tenancies')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  }
}
</script>
