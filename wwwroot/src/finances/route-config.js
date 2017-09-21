import View from './View'
import AccountsOverview from './AccountsOverview'
import AccountDetails from './AccountDetails'
import Transactions from './Transactions'
import Invoice from './Invoice'
import InvoicesOverview from './InvoicesOverview'
import { guardRoute } from 'root/routes/route-guards'

export const RouteConfig = [
  {
    path: '/finances',
    beforeEnter: guardRoute,
    component: View,
    children: [{
      path: '/',
      name: 'accounts-overview',
      component: AccountsOverview
    }, {
      path: 'account-details/:accountId',
      name: 'accounts-details',
      component: AccountDetails
    },
    {
      path: 'transactions/:accountId',
      name: 'transactions',
      component: Transactions
    },
    {
      path: 'invoice/:invoiceId?',
      name: 'invoice',
      component: Invoice
    },
    {
      path: 'invoices',
      name: 'invoices',
      component: InvoicesOverview
    }]
  }
]
