<template>
  <div>
    <header>
      <h1 class="headline primary--text">Invoice</h1>
      <p class="display-2 grey--text text--darken-1">
        {{ this.$route.params.invoiceId ? 'Make changes to your invoice' : 'Create your invoice' }}
      </p>
      <p class="subheading">
        {{ this.$route.params.invoiceId ? 'Edit you ' : 'Create a new ' }} invoice using the form below.</p>
    </header>

    <loader :loading="isLoading"></loader>
    <transition appear name="fade">
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate
            v-if="!isLoading">
        <fieldset :disabled="!permissions.IN_SaveInvoice">
          <v-card>
            <v-card-text>
              <v-container grid-list-md>
                <v-layout>
                  <v-flex xs12 md4>
                    <supplier v-model="invoice.supplier"
                              @supplierAdded = "supplierAdded"
                              @input="supplierChanged"
                              :canAddSupplier = "permissions.SU_SaveSupplier == true"
                              :suppliers="suppliers">
                    </supplier>
                  </v-flex>
                  <v-flex xs12 md4>
                    <date-picker v-model="invoice.date"
                                label="Invoice date">
                    </date-picker>
                  </v-flex>
                  <v-flex xs12 md4>
                    <text-field v-model="invoice.number"
                                label="Invoice Number"
                                :rules="[$validation.rules.required]">
                    </text-field>
                  </v-flex>
                </v-layout>

                <v-layout>
                  <v-flex xs12 md4 offset-md4>
                    <date-picker v-model="invoice.dueDate"
                                label="Due date">
                    </date-picker>
                  </v-flex>
                  <v-flex xs12 md4>
                    <text-field v-model="invoice.poNumber"
                                label="Purchase Order Number">
                    </text-field>
                  </v-flex>
                </v-layout>

                <table class="app-table elevation-1">
                  <thead>
                    <tr>
                      <th></th>
                      <th>Item</th>
                      <th>Description</th>
                      <th>Unit Cost</th>
                      <th>Quantity</th>
                      <th>Line Total</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(line, index) in filteredInvoiceLines"
                        :key="line.id">
                        <td>
                          <v-btn outline fab class="red--text btn--xsmall"
                                @click="deleteInvoiceLine(line)">
                            <v-icon dark>remove</v-icon>
                          </v-btn>
                        </td>
                      <td>
                        <text-field v-model="line.item"
                                    @blur="manageAdditionalLines()"
                                    :rules="[() => $validation.rules.invoice_line_required(hasBeenSubmitted, line)]"
                                    :box="true">
                        </text-field>
                      </td>
                      <td>
                        <text-field v-model="line.description"
                                    @blur="manageAdditionalLines()"
                                    :multiline="true"
                                    :rows="1"
                                    :rules="[() => $validation.rules.invoice_line_required(hasBeenSubmitted, line)]"
                                    :box="true">
                        </text-field>
                      </td>
                      <td>
                        <text-field v-model="line.unitCost"
                                    @blur="manageAdditionalLines()"
                                    :value="line.unitCost"
                                    :rules="[() => $validation.rules.invoice_line_required(hasBeenSubmitted, line), $validation.rules.min_value(line.unitCost, 0), $validation.rules.max_value(line.unitCost, 1000000)]"
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
                                    @blur="manageAdditionalLines()"
                                    :value="line.quantity"
                                    :rules="[() => $validation.rules.invoice_line_required(hasBeenSubmitted, line), $validation.rules.min_value(line.quantity, 0), $validation.rules.max_value(line.quantity, 10000)]"
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

                <v-layout wrap>
                  <v-flex xs12 md4 offset-md8 class="sub-total">
                    <v-layout>
                      <v-flex xs6>
                        Sub Total
                      </v-flex>
                      <v-flex xs6 class="text-right">
                        {{ getSubTotal() | currency('£') }}
                      </v-flex>
                    </v-layout>
                  </v-flex>
                  <v-flex xs12 md4 offset-md8 class="tax">
                    <v-layout>
                      <v-flex xs6>
                        Tax
                      </v-flex>
                      <v-flex xs6 class="text-right">
                        {{ getVAT() | currency('£') }}
                      </v-flex>
                    </v-layout>
                  </v-flex>
                  <v-flex xs12 md4 offset-md8 class="balance-due">
                    <v-layout>
                      <v-flex xs6>
                        Balance due
                      </v-flex>
                      <v-flex xs6 class="text-right">
                        {{ getBalanceDue() | currency('£') }}
                      </v-flex>
                    </v-layout>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
          </v-card>

          <v-layout wrap class="mt-3">
            <v-flex>
              <v-btn primary class="no-left-margin" v-if="permissions.IN_SaveInvoice" type="submit" :loading="isSaving">Save</v-btn>
              <v-btn flat  v-if="permissions.IN_SaveInvoice" @click="reset()">Reset</v-btn>
            </v-flex>
          </v-layout>
        </fieldset>
      </form>
    </transition>
  </div>
</template>

<script>
  import { mapState } from 'vuex'
  import utils from 'utils'

  const getDefaultLine = () => {
    return {
      id: utils.createNewGuid(),
      item: null,
      description: null,
      unitCost: 0,
      quantity: 0,
      isDeleted: false
    }
  }

  export default {
    name: 'invoice',
    data () {
      return {
        isLoading: false,
        isSaving: false,
        hasBeenSubmitted: false,
        suppliers: [],
        invoice: {
          supplier: null,
          date: null,
          number: null,
          dueDate: null,
          poNumber: null,
          lines: [
            Object.assign({}, getDefaultLine()),
            Object.assign({}, getDefaultLine())
          ]
        }
      }
    },
    computed: {
      ...mapState({
        permissions: state => state.permissions
      }),
      filteredInvoiceLines () {
        return this.invoice.lines.filter(line => !line.isDeleted)
      }
    },
    mounted () {
      if (this.$route.params.invoiceId) {
        this.isLoading = true
        this.$http.get(`/api/finances/invoices/${this.$route.params.invoiceId}`)
          .then(response => {
            Object.assign(this, utils.mapEntity(response.data, 'invoice', false))
            this.manageAdditionalLines()
          })
          .finally(() => {
            this.$validation.commit(this.$children)
            this.isLoading = false
          })
      } else {
        this.$validation.commit(this.$children)
      }

      this.$http.get(`/api/finances/suppliers`)
        .then(response => {
          this.suppliers = response.data
        })
    },
    methods: {
      getLineTotal (line) {
        line.total = Number.parseFloat(line.unitCost) * Number.parseFloat(line.quantity)
        return line.total
      },
      getSubTotal () {
        let subTotal = 0
        this.invoice.lines.forEach(line => {
          line.subTotal = this.getLineTotal(line)
          subTotal += line.subTotal
        })
        this.invoice.subTotal = subTotal
        return subTotal
      },
      getVAT () {
        let vat = 0
        this.invoice.lines.forEach(line => {
          line.vat = this.getLineTotal(line) * 0.2
          vat += line.vat
        })
        this.invoice.vat = vat
        return vat
      },
      getBalanceDue () {
        let balanceDue = this.getSubTotal() + this.getVAT()
        this.invoice.total = balanceDue
        return balanceDue
      },
      isLineEmpty (line) {
        return !(line.item || line.description || line.unitCost || line.quantity != 0) /* eslint eqeqeq: "off" */
      },
      deleteInvoiceLine (line) {
        if (this.invoice.lines.indexOf(line) === 0) {
          Object.assign(line, getDefaultLine())
        } else {
          line.isDeleted = true
        }
      },
      manageAdditionalLines () {
        if (this.invoice.lines.length === 1) {
          this.invoice.lines.push(getDefaultLine())
        }

        let lastLine = this.invoice.lines[this.invoice.lines.length - 1]
        let secondLastLine = this.invoice.lines[this.invoice.lines.length - 2]

        if (!this.isLineEmpty(lastLine)) {
          this.invoice.lines.push(getDefaultLine())
        } else if (this.isLineEmpty(lastLine) && this.isLineEmpty(secondLastLine)) {
          this.invoice.lines.splice(this.invoice.lines.length - 1, 1)
        }
      },
      supplierAdded (newSupplier) {
        this.suppliers.push(newSupplier)
      },
      supplierChanged (newSupplier) {
        this.invoice.supplier = newSupplier
      },
      validateBeforeSubmit () {
        this.hasBeenSubmitted = true
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
  }

  .sub-total, .tax {
    border-bottom: 1px solid #dfe0e1;
  }

  .balance-due {
    font-weight: bold;
  }

</style>
