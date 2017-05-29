import Checklists from '../Checklists'
import Overview from '../Overview'
import Editor from '../Editor'
import Template from '../Template'

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
      path: 'editor/:checklistId',
      name: 'editor',
      component: Editor
    }, {
      path: 'template/:checklistId?',
      name: 'template',
      component: Template
    }]
  }
]
