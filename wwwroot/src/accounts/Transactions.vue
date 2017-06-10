<template>
  <main>
    <div> 
      <h1 class="md-display-2">Transactions</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <md-table-card>
      <md-table>
        <md-table-header class="thead-inverse">
          <md-table-row>
            <md-table-head>Date</md-table-head>
            <md-table-head>Reference</md-table-head>
            <md-table-head>Money In</md-table-head>
            <md-table-head>Money Out</md-table-head>
            <md-table-head>Balance</md-table-head>
          </md-table-row>
        </md-table-header>
        <md-table-body>
          <md-table-row v-if="data.transactions" v-for="transaction in data.transactions" :key="transaction">
            <md-table-cell>
              {{ transaction.date }}
            </md-table-cell>
            <md-table-cell>
              {{ transaction.reference }}
            </md-table-cell>
            <md-table-cell>
              {{ transaction.in }}
            </md-table-cell>
            <md-table-cell>
              {{ transaction.out }}
            </md-table-cell>
            <md-table-cell>
              {{ transaction.balance }}
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>

  </main>
</template>

<script>
export default {
  name: 'transactions',
  data () {
    return {
      permissions: this.$store.state.permissions,
      accountId: this.$route.params.accountId,
      data: {
        transactions: []
      }
    }
  },
  created () {
    this.$http.get(`/api/transactions/?accountId=${this.accountId}`).then(response => {
      this.data = response.data
    })
  },
  methods: {

  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
