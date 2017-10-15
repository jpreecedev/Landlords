<template>
  <div>
    <header>
      <h1 class="headline primary--text">Your tenants</h1>
      <p class="display-2 grey--text text--darken-1">There are no notifications or upcoming events</p>
      <p class="subheading">Click on the tenant name for contact information.</p>
    </header>
    <v-card>
      <v-card-title class="title">
        Tenants
      </v-card-title>
      <v-data-table :headers="headers" :items="data" :loading="isLoading">
        <template slot="headers" slot-scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" slot-scope="props">
          <td>
            <router-link v-if="permissions.TE_GetById" :to="'/tenants/details/' + props.item.id">
              <span>{{ props.item.firstName + ' ' + props.item.lastName }}</span>
            </router-link>
            <span v-else>{{ props.item.firstName + ' ' + props.item.lastName }}</span>
          </td>
          <td>
            {{ props.item.mainContactNumber }} <span v-if="props.item.secondaryContactNumber">or {{ props.item.secondaryContactNumber }}</span>
          </td>
          <td>
            {{ props.item.emailAddress }}
          </td>
        </template>
        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'tenants-overview',
  data () {
    return {
      isLoading: false,
      leadTenantOnly: true,
      pagination: {},
      headers: [
        {
          text: 'Name',
          left: true,
          sortable: false
        },
        {
          text: 'Contact Number',
          left: true,
          sortable: false
        },
        {
          text: 'Email Address',
          left: true,
          sortable: false
        }
      ],
      data: []
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  mounted () {
    this.isLoading = true
    this.$http.get('/api/tenants')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  }
}
</script>

<style lang="scss" scoped>

</style>
