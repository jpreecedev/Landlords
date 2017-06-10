<template>
  <main>
    <div> 
      <h1 class="md-display-2">Transactions</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>

    <p v-if="!data.transactions || !data.transactions.length">There are no transactions to display.</p>

    <md-table-card v-if="data.transactions && data.transactions.length">
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

    <div class="row">
      <div class="col-xs-12 col-md-6">
        <md-card>
          <md-card-header>
            <div class="md-title">Import transactions</div>
            <div class="md-subhead">Please upload your bank statements in CSV format</div>
          </md-card-header>
          <md-card-content>       
            <md-input-container>
              <md-file v-model="file" multiple placeholder="Browse for a bank statement on your computer"></md-file>
            </md-input-container>
            <md-progress v-if="progress" :md-progress="progress"></md-progress>
          </md-card-content>
        </md-card>
      </div>
    </div>
  </main>
</template>

<script>
export default {
  name: 'transactions',
  data () {
    return {
      permissions: this.$store.state.permissions,
      accountId: this.$route.params.accountId,
      file: null,
      progress: 0,
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
