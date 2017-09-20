<template>
  <div>
    <header>
      <h1 class="headline primary--text">Invoice</h1>
      <p class="display-2 grey--text text--darken-1">Create or edit your invoice</p>
      <p class="subheading">Create a new invoice using the form below.</p>
    </header>

    <loader :loading="isLoading"></loader>
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
        <div class="row">
          <div class="col-xs-12 col-md-offset-4 col-md-4">
            <date-picker v-model="invoice.partialDeposit"
                         label="Partial/Deposit"
                         :rules="[$validation.rules.required]">
            </date-picker>
          </div>
          <div class="col-xs-12 col-md-4">
            <text-field v-model="invoice.discount"
                        label="Discount"
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

      </v-card-text>
    </v-card>
  </div>
</template>

<script>
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
        suppliers: [],
        invoice: {
          supplier: null,
          date: null,
          number: null,
          dueDate: null,
          poNumber: null,
          partialDeposit: null,
          discount: null,
          lines: [
            Object.assign({}, defaultLine)
          ]
        }
      }
    },
    methods: {
      getLineTotal(line) {
        return line.unitCost * line.quantity;
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
      }
    }
  }

</script>

<style lang="scss" scoped>

</style>
