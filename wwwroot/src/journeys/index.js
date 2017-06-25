import startTenancy from './StartTenancy'
import Announcement from './StartTenancy/Announcement'
import Property from './StartTenancy/Property'
import Payments from './StartTenancy/Payments'
import Tenants from './StartTenancy/Tenants'
import Referencing from './StartTenancy/Referencing'
import TenantType from './StartTenancy/components/TenantType'

export default function install (Vue) {
  Vue.component('starttenancy', startTenancy)
  Vue.component('announcement', Announcement)
  Vue.component('property', Property)
  Vue.component('payments', Payments)
  Vue.component('tenants', Tenants)
  Vue.component('referencing', Referencing)
  Vue.component('tenant-type', TenantType)
}
