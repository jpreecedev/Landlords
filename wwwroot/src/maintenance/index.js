import Dialog from './components/Dialog'
import AddEventDialog from './components/AddEventDialog'

export default function install (Vue) {
  Vue.component('maintenance-dialog', Dialog)
  Vue.component('add-event-dialog', AddEventDialog)
}
