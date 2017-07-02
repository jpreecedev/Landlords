import PermissionsWarning from './PermissionsWarning'
import ValidationSummary from './ValidationSummary'
import FileUpload from './FileUpload'

export default function install (Vue) {
  Vue.component('permissions-warning', PermissionsWarning)
  Vue.component('validation-summary', ValidationSummary)
  Vue.component('file-upload', FileUpload)
}
