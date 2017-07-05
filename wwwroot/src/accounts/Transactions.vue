<template>
  <div>
    <h1 class="display-2">Transactions</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

    <p v-if="!transactions || !transactions.length">There are no transactions to display.</p>

    <v-card>
      <v-data-table :headers="headers" :items="transactions" class="transactions" :pagination.sync="pagination" :loading="loading">
        <template slot="items" scope="props">
          <td>
            {{ props.item.date | formatDate }}
          </td>
          <td>
            {{ props.item.reference }}
          </td>
          <td class="categories">
            <v-select :items="transactionCategories"
                      :hide-details="true"
                      @input="selectedCategory(props.item)"
                      v-if="permissions.TR_UpdateCategory"
                      v-model="props.item.category"
                      label="Category">
            </v-select>
            <span v-else>
              {{ props.item.category }}
            </span>
          </td>
          <td class="text-xs-right">
            <span v-if="props.item.in">{{ props.item.in | currency('£') }}</span>
            <span v-else>&nbsp;</span>
          </td>
          <td class="text-xs-right">
            <span v-if="props.item.out">{{ props.item.out | currency('£') }}</span>
            <span v-else>&nbsp;</span>
          </td>
          <td class="text-xs-right">
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
          <v-card-title class="title">
            Upload your statement
          </v-card-title>
          <v-card-text>
            <div>
              <file-upload v-model="files"
                           @formData="filesChange"
                           name="files"
                           label="Browse for a bank statement on your computer">
              </file-upload>
            </div>
            <div>
              <v-progress-linear v-if="progress"
                                 v-model="progress">
              </v-progress-linear>
            </div>
          </v-card-text>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script>
import FileUploadService from 'services/file-upload.service'
import utils from 'utils'

export default {
  name: 'transactions',
  data () {
    return {
      loading: false,
      pagination: {
        sortBy: 'date',
        descending: true
      },
      headers: [
        {
          text: 'Date',
          left: true,
          value: 'date'
        },
        {
          text: 'Reference',
          left: true,
          sortable: false
        },
        {
          text: 'Category',
          left: true,
          sortable: false
        },
        {
          text: 'Money in',
          value: 'in'
        },
        {
          text: 'Money out',
          value: 'out'
        },
        {
          text: 'Balance',
          value: 'balance'
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
    this.loading = true
    this.$http.get(`/api/transactions/?accountId=${this.accountId}`).then(response => {
      this.loading = false
      this.transactions = response.data.transactions
      Object.assign(this, utils.mapEntity(response.data, null, true))
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
    },
    selectedCategory (transaction) {
      if (this.permissions.TR_UpdateCategory) {
        this.$http.post(`/api/transactions/?accountId=${transaction.accountId}&transactionId=${transaction.transactionId}&category=${transaction.category}`)
      }
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

  .transactions table {
    .categories {
      width: 325px;
    }
  }

</style>
