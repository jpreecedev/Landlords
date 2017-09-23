import AccountDisplay from './components/AccountDisplay'
import AddSupplierDialog from './components/AddSupplierDialog'
import Supplier from './components/Supplier'

export default function install (Vue) {
  Vue.component('account-display', AccountDisplay)
  Vue.component('add-supplier-dialog', AddSupplierDialog)
  Vue.component('supplier', Supplier)
}
