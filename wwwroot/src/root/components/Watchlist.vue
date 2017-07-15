<template>
  <div>
    <h1 class="display-2">My property shortlist</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
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
    </v-card>
  </div>
</template>

<script>
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
      permissions: this.$store.state.permissions,
      data: []
    }
  },
  created () {
    this.loading = true
    this.$http.get('/api/shortlistedproperties')
      .then(response => {
        this.loading = false
        this.data = response.data
      })
      .catch(() => {
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
