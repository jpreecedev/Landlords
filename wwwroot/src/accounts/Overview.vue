<template>
  <main>
    <div> 
      <h1 class="md-display-2">Bank accounts</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <md-table-card>
      <md-table>
        <md-table-header class="thead-inverse">
          <md-table-row>
            <md-table-head>Account name</md-table-head>
            <md-table-head>Type</md-table-head>
            <md-table-head>Provider</md-table-head>
          </md-table-row>
        </md-table-header>
        <md-table-body>
          <md-table-row v-if="data.accounts" v-for="entry in data.accounts" :key="entry">
            <md-table-cell>
              <account-display :account="entry" />
            </md-table-cell>
            <md-table-cell>
              {{ entry.type }}
            </md-table-cell>
            <md-table-cell>
              {{ entry.providerName }}
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>

    <md-button v-if="permissions.AC_New" type="button" class="md-raised md-primary pointer mt-4" @click.native="addAccount()">Add an account</md-button>   

  </main>
</template>

<script>
import AccountDisplay from './components/AccountDisplay'

export default {
  name: 'accounts-overview',
  components: { AccountDisplay },
  data () {
    return {
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
