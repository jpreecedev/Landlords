<template>
  <div class="supplier-container">
    <select-list :items="suppliers"
                 :action="canAddSupplier && addSupplier"
                 :value="value"
                 :rules="[$validation.rules.required]"
                 :returnObject="true"
                 @input="updateField"
                 v-if="suppliers && suppliers.length"
                 label="Supplier"
                 actionLabel="Add Supplier">
    </select-list>

    <div class="action-container">
      <p v-if="!suppliers || !suppliers.length">
        There are no suppliers to choose from.
      </p>
      <v-btn class="no-left-margin"
              @click.stop="addSupplier()">
        Add Supplier
      </v-btn>
    </div>

    <add-supplier-dialog v-on:closed="closeAddSupplierDialog"
                         :isOpen="isAddSupplierDialogOpen">
    </add-supplier-dialog>
  </div>
</template>

<script>
  export default {
    name: 'supplier',
    props: {
      'suppliers': {
        type: Array,
        default: () => [],
        required: true
      },
      'canAddSupplier': {
        type: Boolean,
        default: false,
        required: true
      },
      'value': {
        type: Object,
        default: null
      }
    },
    data () {
      return {
        isAddSupplierDialogOpen: false
      }
    },
    methods: {
      updateField (newValue) {
        this.$emit('input', newValue)
      },
      addSupplier () {
        this.isAddSupplierDialogOpen = true
      },
      closeAddSupplierDialog (newSupplier) {
        this.isAddSupplierDialogOpen = false

        if (newSupplier) {
          this.isSaving = true
          this.$http.post('/api/finances/suppliers', newSupplier)
            .then(response => {
              this.$emit('supplierAdded', response.data)
              this.selectedSupplier = this.suppliers[this.suppliers.length - 1]
            })
            .finally(() => {
              this.isSaving = false
            })
        }
      }
    }
  }
</script>

<style lang="scss" scoped>

  @import 'src/assets/styles/mixins';

  .supplier-container {
    position: relative;
  }

  .action-container {
    text-align: center;
    position: absolute;
    width: 100%;

    p {
      margin: 16px 0;
    }

    button {
      margin: 0 auto;
    }
  }

</style>
