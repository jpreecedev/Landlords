<template>
  <div>
    <header>
      <h1 class="headline primary--text">Maintenance Requests</h1>
      <p class="display-2 grey--text text--darken-1">Log maintenance and repair requests with your landlord</p>
      <p class="subheading">Click on a maintenance request for more details.</p>
    </header>
    <v-card>
      <v-data-table :headers="headers" :items="data" :loading="isLoading">
        <template slot="headers" scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" scope="props">
          <td>{{ props.item.title }}</td>
          <td>
            <notification-icon :type="notification.type"></notification-icon>
          </td>
          <td>
            <router-link v-if="permissions.MR_GetById" :to="'/maintenance/details/' + props.item.id">
              <v-icon>edit</v-icon>
            </router-link>
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
      <v-btn @click="addMaintenanceRequest()"
             class="blue darken-2 action-button"
             absolute dark fab top right>
        <v-icon>add</v-icon>
      </v-btn>
    </v-card>

    <v-btn primary
           class="mt-4 no-left-margin"
           @click="addMaintenanceRequest()">Open New Maintenance Request</v-btn>

  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'maintenance-request-overview',
  data () {
    return {
      isLoading: false,
      pagination: {},
      headers: [
        {
          text: 'Title',
          left: true,
          sortable: false
        }, {
          text: 'Severity',
          left: true,
          sortable: false
        }, {
          text: 'Actions',
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
  created () {
    this.isLoading = true
    this.$http.get('/api/maintenancerequests')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  methods: {
    addMaintenanceRequest () {
      this.$router.push({ name: 'maintenance-request-details' })
    }
  }
}
</script>

<style lang="scss" scoped>

  td a {
    text-decoration: none;
  }

</style>
