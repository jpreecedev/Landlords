<template>
  <div>
    <header>
      <h1 class="headline primary--text">Invoices</h1>
      <p class="display-2 grey--text text--darken-1">Manage your recent invoices</p>
      <p class="subheading">Click on the account name to see invoices.</p>
    </header>
    <v-card>
      <v-data-table :headers="headers" :items="data.invoices" :loading="isLoading">
        <template slot="headers" slot-scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
        <template slot="items" slot-scope="props">
          <td>
            <router-link v-if="permissions.IN_GetById" :to="'/finances/invoice/' + props.item.id" class="link">
              {{ props.item.supplier.name }}
            </router-link>
            <span v-else>
              {{ props.item.supplier.name }}
            </span>
          </td>
          <td>
            {{ props.item.number }}
          </td>
          <td>
            {{ props.item.date | formatDate }}
          </td>
          <td>
            {{ props.item.poNumber }}
          </td>
          <td>
            {{ props.item.total | currency('£') }}
          </td>
          <td>
            <router-link v-if="permissions.IN_GetById" :to="'/finances/invoice/' + props.item.id">
              <v-icon>edit</v-icon>
            </router-link>
          </td>
        </template>
        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
      <v-btn v-if="permissions.IN_SaveInvoice"
             @click="addInvoice()"
             class="blue darken-2 action-button"
             absolute dark fab top right>
        <v-icon>add</v-icon>
      </v-btn>
    </v-card>

    <v-btn color="primary"
           v-if="permissions.IN_SaveInvoice"
           class="mt-4 no-left-margin"
           @click="addInvoice()">
           Create an invoice
    </v-btn>

  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'invoices-overview',
  data () {
    return {
      isLoading: false,
      pagination: {},
      headers: [
        {
          text: 'Supplier',
          left: true,
          sortable: false
        }, {
          text: 'Number',
          left: true,
          sortable: false
        }, {
          text: 'Date',
          left: true,
          sortable: false
        }, {
          text: 'PO Number',
          left: true,
          sortable: false
        }, {
          text: 'Total',
          left: true,
          sortable: false
        }, {
          text: '',
          left: true,
          sortable: false
        }
      ],
      data: {
        invoices: []
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
    this.$http.get('/api/finances/invoices')
      .then(response => {
        this.data.invoices = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  },
  methods: {
    addInvoice () {
      this.$router.push({ name: 'invoice' })
    }
  }
}
</script>

<style lang="scss" scoped>

  td a:not(.link) {
    text-decoration: none;
  }

</style>
