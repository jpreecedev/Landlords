import View from './View'
import Overview from './Overview'
import ManagementOverview from './ManagementOverview'
import { redirectIfManager } from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/maintenance',
    beforeEnter: redirectIfManager,
    component: View,
    children: [{
      path: '/',
      name: 'maintenance-request-overview',
      component: Overview
    }, {
      path: 'management',
      name: 'management-overview',
      component: ManagementOverview
    }]
  }
]
