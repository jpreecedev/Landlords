import Tenancy from './Tenancy'
import Overview from './Overview'
import Details from './Details'
import guardRoute from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/tenancies',
    beforeEnter: guardRoute,
    component: Tenancy,
    children: [{
      path: '/',
      name: 'tenancy-overview',
      component: Overview
    }, {
      path: 'details/:tenancyId',
      name: 'tenancy-details',
      component: Details
    }]
  }
]
