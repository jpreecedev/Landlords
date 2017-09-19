import Vue from 'vue'
import Router from 'vue-router'
import { RouteConfig as RootRouteConfig } from './route-config'
import { RouteConfig as ChecklistsRouteConfig } from '../../checklists/route-config'
import { RouteConfig as FinancesRouteConfig } from '../../finances/route-config'
import { RouteConfig as TenantsRouteConfig } from '../../tenants/route-config'
import { RouteConfig as TenanciesRouteConfig } from '../../tenancies/route-config'
import { RouteConfig as ConversationRouteConfig } from '../../conversations/route-config'
import { RouteConfig as MaintenanceRequestRouteConfig } from '../../maintenance/route-config'

Vue.use(Router)

const router = new Router({
  base: '/',
  mode: 'history',
  linkActiveClass: 'active',
  routes: [
    ...RootRouteConfig,
    ...ChecklistsRouteConfig,
    ...FinancesRouteConfig,
    ...TenantsRouteConfig,
    ...TenanciesRouteConfig,
    ...ConversationRouteConfig,
    ...MaintenanceRequestRouteConfig
  ]
})

export default router
