import Accounts from './Accounts'
import Overview from './Overview'
import Details from './Details'
import Transactions from './Transactions'
import { guardRoute } from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/accounts',
    beforeEnter: guardRoute,
    component: Accounts,
    children: [{
      path: '/',
      name: 'accounts-overview',
      component: Overview
    }, {
      path: 'details/:accountId',
      name: 'accounts-details',
      component: Details
    },
    {
      path: 'transactions/:accountId',
      name: 'transactions',
      component: Transactions
    }]
  }
]
