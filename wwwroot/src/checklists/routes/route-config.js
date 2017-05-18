import Checklists from '../Checklists'
import Overview from '../Overview'
import NewTenantMoveIn from '../NewTenantMoveIn'

import guardRoute from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/checklists',
    beforeEnter: guardRoute,
    component: Checklists,
    children: [{
      path: '/',
      name: 'overview',
      component: Overview
    }, {
      path: 'new-tenant-move-in/:checklistId',
      name: 'newTenantMoveIn',
      component: NewTenantMoveIn
    }]
  }
]
