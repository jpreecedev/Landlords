<template>
  <div>
    <h1 class="display-2">Bank accounts</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-card>
      <v-data-table :headers="headers" :items="data.accounts">
        <template slot="items" scope="props">
          <td>
            <account-display :account="props.item" />
          </td>
          <td>{{ props.item.type }}</td>
          <td>{{ props.item.providerName }}</td>
          <td>
            <router-link v-if="permissions.AC_GetById" :to="'/accounts/details/' + props.item.id">
              <v-icon dark>edit</v-icon>
            </router-link>
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>

    <v-btn primary light v-if="permissions.AC_New" class="md-raised md-primary mt-4" @click.native="addAccount()">Add an account</v-btn>

  </div>
</template>

<script>
import AccountDisplay from './components/AccountDisplay'

export default {
  name: 'accounts-overview',
  components: { AccountDisplay },
  data () {
    return {
      pagination: {},
      headers: [
        {
          text: 'Account name',
          left: true,
          sortable: false
        }, {
          text: 'Type',
          left: true,
          sortable: false
        }, {
          text: 'Provider',
          left: true,
          sortable: false
        }, {
          text: 'Actions',
          left: true,
          sortable: false
        }
      ],
      permissions: this.$store.state.permissions,
      data: {
        accounts: []
      }
    }
  },
  created () {
    this.$http.get('/api/accounts').then(response => {
      this.data = response.data
    })
  },
  methods: {
    addAccount: function () {
      this.$http.post('/api/accounts/new').then(response => {
        this.$router.push({ name: 'accounts-details', params: { accountId: response.data.id } })
      })
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
