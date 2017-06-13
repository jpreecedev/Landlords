import startTenancy from './StartTenancy'
import Announcement from './StartTenancy/Announcement'
import Home from './StartTenancy/Home'
import Payments from './StartTenancy/Payments'
import Tenants from './StartTenancy/Tenants'

export default function install (Vue) {
  Vue.component('starttenancy', startTenancy)
  Vue.component('announcement', Announcement)
  Vue.component('starttenancy-home', Home)
  Vue.component('payments', Payments)
  Vue.component('tenants', Tenants)
}
