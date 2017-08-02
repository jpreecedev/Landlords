import View from './View'
import Chat from './Chat'
import guardRoute from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/conversations',
    beforeEnter: guardRoute,
    component: View,
    children: [{
      path: '/',
      name: 'chat',
      component: Chat
    }]
  }
]
