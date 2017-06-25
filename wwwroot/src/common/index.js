import PermissionsWarning from './PermissionsWarning'
import ValidationSummary from './ValidationSummary'

export default function install (Vue) {
  Vue.component('permissions-warning', PermissionsWarning)
  Vue.component('validation-summary', ValidationSummary)
}
