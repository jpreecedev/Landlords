import Tenants from './Tenants'
import Overview from './Overview'
import Details from './Details'
import { guardRoute } from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/tenants',
    beforeEnter: guardRoute,
    component: Tenants,
    children: [{
      path: '/',
      name: 'tenants-overview',
      component: Overview
    }, {
      path: 'details/:tenantId',
      name: 'tenants-details',
      component: Details
    }]
  }
]
