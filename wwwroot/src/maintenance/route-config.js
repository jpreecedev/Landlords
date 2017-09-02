import View from './View'
import Overview from './Overview'
import { guardRoute } from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/maintenance',
    beforeEnter: guardRoute,
    component: View,
    children: [{
      path: '/',
      name: 'maintenance-request-overview',
      component: Overview
    }]
  }
]
