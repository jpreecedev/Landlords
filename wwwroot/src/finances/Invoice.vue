<template>
  <div>
    <header>
      <h1 class="headline primary--text">Invoice</h1>
      <p class="display-2 grey--text text--darken-1">Create or edit your invoice</p>
      <p class="subheading">Create a new invoice using the form below.</p>
    </header>

    <loader :loading="isLoading"></loader>

    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.IN_SaveInvoice">
        <v-card>
          <v-card-text>
            <div class="row">
              <div class="col-xs-12 col-md-4">
                <select-list :items="suppliers"
                              v-model="invoice.supplier"
                              label="Supplier"
                              :rules="[$validation.rules.required]">
                </select-list>
              </div>
              <div class="col-xs-12 col-md-4">
                <date-picker v-model="invoice.date"
                            label="Invoice date"
                            :rules="[$validation.rules.required]">
                </date-picker>
              </div>
              <div class="col-xs-12 col-md-4">
                <text-field v-model="invoice.number"
                            label="Invoice Number"
                            :rules="[$validation.rules.required]">
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12 col-md-offset-4 col-md-4">
                <date-picker v-model="invoice.dueDate"
                            label="Invoice date"
                            :rules="[$validation.rules.required]">
                </date-picker>
              </div>
              <div class="col-xs-12 col-md-4">
                <text-field v-model="invoice.poNumber"
                            label="Purchase Order Number"
                            :rules="[$validation.rules.required]">
                </text-field>
              </div>
            </div>

            <table class="app-table elevation-1">
              <thead>
                <tr>
                  <th>Item</th>
                  <th>Description</th>
                  <th>Unit Cost</th>
                  <th>Quantity</th>
                  <th>Line Total</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="line in invoice.lines"
                    :key="line.id">
                  <td>
                    <text-field v-model="line.item"
                                @blur="addMoreLines()"
                                :rules="[$validation.rules.required]"
                                :box="true">
                    </text-field>
                  </td>
                  <td>
                    <text-field v-model="line.description"
                                @blur="addMoreLines()"
                                :multiline="true"
                                :rows="1"
                                :rules="[$validation.rules.required]"
                                :box="true">
                    </text-field>
                  </td>
                  <td>
                    <text-field v-model="line.unitCost"
                                @blur="addMoreLines()"
                                :value="line.unitCost"
                                :rules="[$validation.rules.required, $validation.rules.min_value(line.unitCost, 0), $validation.rules.max_value(line.unitCost, 1000000)]"
                                :box="true"
                                class="text-right"
                                type="number"
                                min="0"
                                max="1000000"
                                step="1">
                    </text-field>
                  </td>
                  <td>
                    <text-field v-model="line.quantity"
                                @blur="addMoreLines()"
                                :value="line.quantity"
                                :rules="[$validation.rules.required, $validation.rules.min_value(line.quantity, 0), $validation.rules.max_value(line.quantity, 10000)]"
                                :box="true"
                                class="text-right"
                                type="number"
                                min="0"
                                max="10000"
                                step="1">
                    </text-field>
                  </td>
                  <td class="text-right">
                    {{ getLineTotal(line) | currency('£') }}
                  </td>
                </tr>
              </tbody>
            </table>

            <div class="row">
              <div class="col-xs-12 col-md-offset-8 col-md-4 sub-total">
                <div class="row">
                  <div class="col-xs-6">
                    Sub Total
                  </div>
                  <div class="col-xs-6 text-right">
                    {{ getSubTotal() | currency('£') }}
                  </div>
                </div>
              </div>
              <div class="col-xs-12 col-md-offset-8 col-md-4 tax">
                <div class="row">
                  <div class="col-xs-6">
                    Tax
                  </div>
                  <div class="col-xs-6 text-right">
                    {{ getTax() | currency('£') }}
                  </div>
                </div>
              </div>
              <div class="col-xs-12 col-md-offset-8 col-md-4 balance-due">
                <div class="row">
                  <div class="col-xs-6">
                    Balance due
                  </div>
                  <div class="col-xs-6 text-right">
                    {{ getBalanceDue() | currency('£') }}
                  </div>
                </div>
              </div>
            </div>
          </v-card-text>
        </v-card>

        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary class="no-left-margin" v-if="permissions.IN_SaveInvoice" type="submit" :loading="isSaving">Save</v-btn>
            <v-btn flat  v-if="permissions.IN_SaveInvoice" @click="reset()">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </div>
</template>

<script>
  import { mapState } from 'vuex'
  import utils from 'utils'

  const defaultLine = {
    id: '1',
    item: null,
    description: null,
    unitCost: 0,
    quantity: 1
  };

  export default {
    name: 'invoice',
    data () {
      return {
        isLoading: false,
        isSaving: false,
        suppliers: [],
        invoice: {
          supplier: null,
          date: null,
          number: null,
          dueDate: null,
          poNumber: null,
          lines: [
            Object.assign({}, defaultLine)
          ]
        }
      }
    },
    computed: {
      ...mapState({
        permissions: state => state.permissions
      })
    },
    mounted () {
      if (this.$route.params.invoiceId) {
        this.$http.get(`/api/finances/invoices/${this.$route.params.invoiceId}`)
          .then(response => {
            Object.assign(this, utils.mapEntity(response.data, 'invoice', false))
            this.$validation.commit(this.$children)
          })
      }
    },
    methods: {
      getLineTotal(line) {
        return line.unitCost * line.quantity
      },
      getSubTotal() {
        let subTotal = 0;
        this.invoice.lines.forEach(line => {
          subTotal += this.getLineTotal(line)
        })
        return subTotal
      },
      getTax() {
        let tax = 0
        this.invoice.lines.forEach(line => {
          tax += this.getLineTotal(line) * 0.2
        })
        return tax
      },
      getBalanceDue() {
        return this.getSubTotal() + this.getTax()
      },
      isLineEmpty(line) {
        return !(line.item || line.description || line.unitCost || line.quantity !== 1);
      },
      addMoreLines() {
        let lastLine = this.invoice.lines[this.invoice.lines.length - 1];
        if (lastLine && !this.isLineEmpty(lastLine)) {
          let newLine = Object.assign({}, defaultLine, {
            id: String(this.invoice.lines.length + 1)
          })

          this.invoice.lines.push(newLine)
        }
      },
      validateBeforeSubmit () {
        this.$validation.validate(this.$children)
          .then(() => {
            this.isSaving = true
            this.$http.post(`/api/finances/invoices/`, { ...this.invoice })
              .then(() => {
                this.$router.push({ name: 'invoices' })
              })
              .catch(response => {
                let validationResult = utils.getFormValidationErrors(response)
                validationResult.errors.forEach(validationError => {
                  console.log('ERROR', validationError.key, validationError.messages[0], 'required')
                })
              })
              .finally(() => {
                this.isSaving = false
              })
          })
          .catch(() => {
            this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
          })
      },
      reset () {
        this.$validation.reset(this.$children)
      }
    }
  }

</script>

<style lang="scss" scoped>

  .sub-total, .tax, .balance-due {
    line-height: 32px;
    font-size: 16px;

    .col-xs-6 {
      padding: 8px;
    }
  }

  .sub-total, .tax {
    border-bottom: 1px solid #dfe0e1;
  }

  .balance-due {
    font-weight: bold;
  }

</style>
