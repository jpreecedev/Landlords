<template>
  <main class="container">
    <div> 
      <h1>Bank accounts</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <table class="table table-striped table-hover">
      <thead class="thead-inverse">
        <tr>
          <th>Account name</th>
          <th>Type</th>
          <th>Provider</th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="data.accounts" v-for="entry in data.accounts">
          <td>
            <router-link v-if="permissions.AC_GetById" :to="{name: 'accounts-details', params: {accountId: entry.id}}">
              <account-display :account="entry" />
            </router-link>
            <span v-else>
              <account-display :account="entry" />
            </span>
          </td>
          <td>
            {{ entry.type }}
          </td>
          <td>
            {{ entry.providerName }}
          </td>
        </tr>
      </tbody>
    </table>
    <md-button v-if="permissions.AC_New" type="button" class="md-raised md-primary pointer" @click.native="addAccount()">Add an account</md-button>    
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
