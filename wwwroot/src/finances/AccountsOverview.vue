<template>
  <div>
    <header>
      <h1 class="headline primary--text">Bank accounts</h1>
      <p class="display-2 grey--text text--darken-1">Manage your accounts online to identify your portfolio financial strengths and weaknesses</p>
      <p class="subheading">Click on the account name to see transactions.</p>
    </header>
    <v-card>
      <v-data-table :headers="headers" :items="data.accounts" :loading="isLoading">
        <template slot="headers" slot-scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" slot-scope="props">
          <td>
            <account-display :account="props.item"></account-display>
          </td>
          <td>{{ props.item.type }}</td>
          <td>{{ props.item.providerName }}</td>
          <td>
            <router-link v-if="permissions.FI_GetById" :to="'/finances/account-details/' + props.item.id">
              <v-icon>edit</v-icon>
            </router-link>
          </td>
        </template>
        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
      <v-btn v-if="permissions.FI_New"
             @click="addAccount()"
             class="blue darken-2 action-button"
             absolute dark fab top right>
        <v-icon>add</v-icon>
      </v-btn>
    </v-card>

    <v-btn color="primary"
           v-if="permissions.FI_New"
           class="mt-4 no-left-margin"
           @click="addAccount()"
           :loading="isAddingAccount">Add an account</v-btn>

  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'accounts-overview',
  data () {
    return {
      isLoading: false,
      isAddingAccount: false,
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
      data: {
        accounts: []
      }
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  mounted () {
    this.isLoading = true
    this.$http.get('/api/finances')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  methods: {
    addAccount () {
      this.isAddingAccount = true
      this.$http.post('/api/finances/new')
        .then(response => {
          this.isAddingAccount = false
          this.$router.push({ name: 'accounts-details', params: { accountId: response.data.id } })
        })
        .catch(() => {
          this.isAddingAccount = false
        })
    }
  }
}
</script>

<style lang="scss" scoped>

  td a {
    text-decoration: none;
  }

</style>
