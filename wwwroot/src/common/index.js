import PermissionsWarning from './PermissionsWarning'
import ValidationSummary from './ValidationSummary'
import FileUpload from './FileUpload'
import Pricing from './Pricing'
import Loader from './Loader'
import TextField from './TextField'
import SelectField from './SelectField'
import DatePicker from './DatePicker'
import Notification from './Notification'
import NotificationIcon from './NotificationIcon'
import Timeline from './Timeline'
import Help from './Help'

export default function install (Vue) {
  Vue.component('permissions-warning', PermissionsWarning)
  Vue.component('validation-summary', ValidationSummary)
  Vue.component('file-upload', FileUpload)
  Vue.component('pricing', Pricing)
  Vue.component('loader', Loader)
  Vue.component('text-field', TextField)
  Vue.component('select-list', SelectField)
  Vue.component('date-picker', DatePicker)
  Vue.component('notification', Notification)
  Vue.component('notification-icon', NotificationIcon)
  Vue.component('timeline', Timeline)
  Vue.component('help', Help)
}
