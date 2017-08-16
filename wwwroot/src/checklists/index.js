import Accordion from './components/Accordion'
import Dialog from './components/Dialog'
import DocumentUpload from './components/DocumentUpload'
import CommentsDateOfAction from './components/CommentsAndDateOfAction'
import CommentsOnly from './components/CommentsOnly'
import DateOfAction from './components/DateOfAction'

export default function install (Vue) {
  Vue.component('accordion', Accordion)
  Vue.component('checklist-dialog', Dialog)
  Vue.component('document-upload', DocumentUpload)
  Vue.component('comments-date-of-action', CommentsDateOfAction)
  Vue.component('comments-only', CommentsOnly)
  Vue.component('date-of-action', DateOfAction)
}
