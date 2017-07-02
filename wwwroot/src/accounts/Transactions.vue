<template>
  <div>
    <h1 class="display-2">Transactions</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

    <p v-if="!transactions || !transactions.length">There are no transactions to display.</p>

    <v-card v-if="transactions && transactions.length">
      <v-data-table :headers="headers" :items="transactions">
        <template slot="items" scope="props">
          <td>
            {{ props.item.date | formatDate }}
          </td>
          <td>
            {{ props.item.reference }}
          </td>
          <td>
            <span v-if="props.item.in">{{ props.item.in | currency('£') }}</span>
            <span v-else>&nbsp;</span>
          </td>
          <td>
            <span v-if="props.item.out">{{ props.item.out | currency('£') }}</span>
            <span v-else>&nbsp;</span>
          </td>
          <td>
            <span>{{ props.item.balance | currency('£') }}</span>
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>

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
              <file-upload v-model="files"
                           @formData="filesChange"
                           name="files"
                           label="Browse for a bank statement on your computer">
              </file-upload>
            </v-card-row>
            <v-card-row>
              <v-progress-linear v-if="progress"
                                 v-model="progress">
              </v-progress-linear>
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
      pagination: {},
      headers: [
        {
          text: 'Date',
          left: true,
          sortable: false
        },
        {
          text: 'Reference',
          left: true,
          sortable: false
        },
        {
          text: 'Money',
          left: true,
          sortable: false
        },
        {
          text: 'Money',
          left: true,
          sortable: false
        },
        {
          text: 'Balance',
          left: true,
          sortable: false
        }
      ],
      permissions: this.$store.state.permissions,
      accountId: this.$route.params.accountId,
      files: null,
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
    filesChange: function (formData) {
      FileUploadService.upload(formData[0], `/api/transactions/upload?entityId=${this.accountId}`, uploadProgress => { this.progress = Number.parseInt(uploadProgress) })
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
  table.datatable.table tbody tr {
    td:first-child {
      width: 150px;
    }
  }
</style>
