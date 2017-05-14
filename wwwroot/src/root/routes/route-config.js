import LandingPage from 'root/components/LandingPage'
import Registration from 'root/components/Registration'
import Permissions from 'root/components/Permissions'
import Dashboard from 'root/components/Dashboard'
import Profile from 'root/components/Profile'
import Agency from 'root/components/Agency'
import LandlordList from 'root/components/LandlordList'
import Manager from 'root/components/Manager'
import PropertyDetails from 'root/components/PropertyDetails'
import PropertyList from 'root/components/PropertyList'
import Calculators from 'root/components/Calculators/Calculators'
import GoodInvestment from 'root/components/Calculators/GoodInvestment'
import HowMuch from 'root/components/Calculators/HowMuch'
import MonthlyPayment from 'root/components/Calculators/MonthlyPayment'
import RentalYield from 'root/components/Calculators/RentalYield'

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
