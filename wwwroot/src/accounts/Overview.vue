<template>
  <div>
    <h1 class="display-2">Bank accounts</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-card>
      <v-data-table :headers="headers" :items="data.accounts" :loading="loading">
        <template slot="items" scope="props">
          <td>
            <account-display :account="props.item"></account-display>
          </td>
          <td>{{ props.item.type }}</td>
          <td>{{ props.item.providerName }}</td>
          <td>
            <router-link v-if="permissions.AC_GetById" :to="'/accounts/details/' + props.item.id">
              <v-icon>edit</v-icon>
            </router-link>
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>

    <v-btn primary v-if="permissions.AC_New" class="mt-4" @click.native="addAccount()">Add an account</v-btn>

  </div>
</template>

<script>
import AccountDisplay from './components/AccountDisplay'

export default {
  name: 'accounts-overview',
  components: { AccountDisplay },
  data () {
    return {
      loading: false,
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
    this.loading = true
    this.$http.get('/api/accounts').then(response => {
      this.loading = false
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
