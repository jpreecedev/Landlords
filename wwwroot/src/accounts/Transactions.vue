<template>
  <div>
    <h1 class="display-2">Transactions</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

    <p v-if="!transactions || !transactions.length">There are no transactions to display.</p>

    <md-table-card v-if="transactions && transactions.length">
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
          <md-table-row v-if="index >= (page - 1) * size && index < (page - 1) * size + size" v-for="(transaction, index) in transactions" :key="transaction">
            <md-table-cell>
              {{ transaction.date | formatDate }}
            </md-table-cell>
            <md-table-cell>
              {{ transaction.reference }}
            </md-table-cell>
            <md-table-cell>
              <span v-if="transaction.in">{{ transaction.in | currency('£') }}</span>
              <span v-else>&nbsp;</span>
            </md-table-cell>
            <md-table-cell>
              <span v-if="transaction.out">{{ transaction.out | currency('£') }}</span>
              <span v-else>&nbsp;</span>
            </md-table-cell>
            <md-table-cell>
              <span>{{ transaction.balance | currency('£') }}</span>
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
      <md-table-pagination
        :md-size="size"
        :md-total="transactions.length"
        :md-page="page"
        :md-page-options="[10, 25, 50]"
        md-label="Transactions per page"
        @pagination="onPagination" />
    </md-table-card>

    <div class="row mt-5">
      <div class="col-xs-12 col-md-6">
        <v-card>
          <v-card-row>
            <v-card-title>
              Upload your statement
            </v-card-title>
          </v-card-row>
          <v-card-text>
            <v-card-row>
              <md-input-container>
                <md-file v-model="file" multiple placeholder="Browse for a bank statement on your computer" @selected="filesChange('files', $event)" name="files" />
              </md-input-container>
              <md-progress v-if="progress" :md-progress="progress"></md-progress>
            </v-card-row>
          </v-card-text>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script>
import FileUploadService from 'services/file-upload.service'

export default {
  name: 'transactions',
  data () {
    return {
      page: 1,
      size: 10,
      permissions: this.$store.state.permissions,
      accountId: this.$route.params.accountId,
      file: null,
      progress: 0,
      transactions: []
    }
  },
  created () {
    this.$http.get(`/api/transactions/?accountId=${this.accountId}`).then(response => {
      this.transactions = response.data
    })
  },
  methods: {
    filesChange: function (fieldName, fileList) {
      const formData = new FormData()
      if (!fileList.length) return

      Array.from(Array(fileList.length).keys()).map(x => {
        formData.append(fieldName, fileList[x], fileList[x].name)
      })

      FileUploadService.upload(formData, `/api/transactions/upload?entityId=${this.accountId}`, uploadProgress => { this.progress = Number.parseInt(uploadProgress) })
        .then(transactions => {
          if (transactions) {
            this.transactions.push(...transactions)
          }
        })
        .finally(() => {
          this.progress = 0
        })
    },
    onPagination: function (pagination) {
      this.page = pagination.page

      if (this.size !== pagination.size) {
        this.page = 1
      }

      this.size = pagination.size
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
