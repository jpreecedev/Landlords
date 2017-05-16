import Checklists from '../Checklists'
import NewTenantMoveIn from '../NewTenantMoveIn'

import guardRoute from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/checklists',
    beforeEnter: guardRoute,
    component: Checklists,
    children: [{
      path: 'new-tenant-move-in',
      name: 'newTenantMoveIn',
      component: NewTenantMoveIn
    }]
  }
]
