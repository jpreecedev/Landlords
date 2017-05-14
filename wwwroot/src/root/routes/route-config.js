import LandingPage from 'components/LandingPage'
import Registration from 'components/Registration'
import Permissions from 'components/Permissions'
import Dashboard from 'components/Dashboard'
import Profile from 'components/Profile'
import Agency from 'components/Agency'
import LandlordList from 'components/LandlordList'
import Manager from 'components/Manager'
import PropertyDetails from 'components/PropertyDetails'
import PropertyList from 'components/PropertyList'
import Calculators from 'components/Calculators/Calculators'
import GoodInvestment from 'components/Calculators/GoodInvestment'
import HowMuch from 'components/Calculators/HowMuch'
import MonthlyPayment from 'components/Calculators/MonthlyPayment'
import RentalYield from 'components/Calculators/RentalYield'

import guardRoute from './route-guards'

export const RouteConfig = [
  {
    path: '/',
    name: 'landingpage',
    component: LandingPage
  },
  {
    path: '/registration',
    name: 'registration',
    component: Registration
  },
  {
    path: '/permissions',
    name: 'permissions',
    component: Permissions
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    beforeEnter: guardRoute
  },
  {
    path: '/profile',
    name: 'profile',
    component: Profile,
    beforeEnter: guardRoute
  },
  {
    path: '/agency',
    beforeEnter: guardRoute,
    component: Agency,
    children: [{
      path: 'landlord-list',
      name: 'landlordList',
      component: LandlordList
    }]
  },
  {
    path: '/manager',
    beforeEnter: guardRoute,
    component: Manager,
    children: [{
      path: 'property-details/:propertyId',
      name: 'propertyDetails',
      component: PropertyDetails
    },
    {
      path: 'property-list',
      name: 'propertyList',
      component: PropertyList
    }]
  },
  {
    path: '/calculators',
    component: Calculators,
    children: [
      {
        path: 'rental-yield',
        name: 'rentalYield',
        component: RentalYield
      },
      {
        path: 'monthly-payment',
        name: 'monthlyPayment',
        component: MonthlyPayment
      },
      {
        path: 'how-much-can-i-borrow',
        name: 'howMuch',
        component: HowMuch
      },
      {
        path: 'is-this-property-a-good-investment',
        name: 'goodInvestment',
        component: GoodInvestment
      }]
  }
]
