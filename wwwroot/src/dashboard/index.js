import AgencyDashboard from './components/Agency'
import LandlordDashboard from './components/Landlord'
import TenantDashboard from './components/Tenant'
import AdministratorDashboard from './components/Administrator'

export default function install (Vue) {
  Vue.component('agency-dashboard', AgencyDashboard)
  Vue.component('landlord-dashboard', LandlordDashboard)
  Vue.component('tenant-dashboard', TenantDashboard)
  Vue.component('administrator-dashboard', AdministratorDashboard)
}
