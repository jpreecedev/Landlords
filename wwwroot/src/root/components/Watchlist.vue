<template>
  <div>
    <header>
      <h1 class="headline primary--text">Property shortlist</h1>
      <p class="display-2 grey--text text--darken-1">Easily track properties you are considering purchasing and see the potential ROI of each</p>
      <p class="subheading">Click on the property reference for more information.</p>
    </header>
    <v-card>
      <v-data-table :headers="headers" :items="data" :loading="loading">
        <template slot="headers" scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" scope="props">
          <td>
            <router-link v-if="permissions.SP_GetById" :to="'/calculators/return-on-investment/' + props.item.shortlistedPropertyId">
              {{ props.item.reference }}
            </router-link>
          </td>
          <td>{{ props.item.address }}</td>
          <td>{{ (getReturnOnInvestment(props.item).roi * 100).toFixed(2)}}%</td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>

      <v-btn @click="$router.push('/calculators/return-on-investment')"
             class="blue darken-2 action-button"
             absolute dark fab top right>
        <v-icon>add</v-icon>
      </v-btn>

    </v-card>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'watchlist',
  data () {
    return {
      loading: false,
      pagination: {},
      headers: [
        {
          text: 'Reference',
          left: true,
          sortable: false
        }, {
          text: 'Address',
          left: true,
          sortable: false
        }, {
          text: 'Potential ROI',
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
    this.loading = true
    this.$http.get('/api/shortlistedproperties')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.loading = false
      })
  },
  methods: {
    getReturnOnInvestment (shortlistedProperty) {
      if (!shortlistedProperty) {
        return false
      }

      return utils.getReturnOnInvestment(shortlistedProperty)
    }
  }
}
</script>
