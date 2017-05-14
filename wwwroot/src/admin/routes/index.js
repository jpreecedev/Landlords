import Vue from 'vue'
import Router from 'vue-router'
import { RouteConfig } from './route-config'

Vue.use(Router)

const router = new Router({
  base: '/admin',
  mode: 'history',
  linkActiveClass: 'active',
  routes: RouteConfig
})

export default router
