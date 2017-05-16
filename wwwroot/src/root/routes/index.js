import Vue from 'vue'
import Router from 'vue-router'
import { RouteConfig as RootRouteConfig } from './route-config'
import { RouteConfig as ChecklistsRouteConfig } from 'checklists/routes/route-config'

Vue.use(Router)

const router = new Router({
  base: '/',
  mode: 'history',
  linkActiveClass: 'active',
  routes: [...RootRouteConfig, ...ChecklistsRouteConfig]
})

export default router
