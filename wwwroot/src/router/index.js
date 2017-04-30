import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  scrollBehavior (to, from, savedPosition) {
    return { x: 0, y: 0 }
  },
  routes: [{
    path: '/',
    name: 'landingpage',
    component: function (resolve) {
      require(['@/components/LandingPage.vue'], resolve)
    }
  },
  {
    path: '/registration',
    name: 'registration',
    component: function (resolve) {
      require(['@/components/Registration.vue'], resolve)
    }
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: function (resolve) {
      require(['@/components/Dashboard.vue'], resolve)
    },
    beforeEnter: guardRoute
  },
  {
    path: '/manager',
    beforeEnter: guardRoute,
    component: function (resolve) {
      require(['@/components/Manager.vue'], resolve)
    },
    children: [{
      path: 'property-details/:propertyId',
      name: 'propertyDetails',
      component: function (resolve) {
        require(['@/components/PropertyDetails.vue'], resolve)
      }
    },
    {
      path: 'property-list',
      name: 'propertyList',
      component: function (resolve) {
        require(['@/components/PropertyList.vue'], resolve)
      }
    }]
  },
  {
    path: '/calculators',
    component: function (resolve) {
      require(['@/components/calculators/Calculators.vue'], resolve)
    },
    children: [
      {
        path: 'rental-yield',
        name: 'rentalYield',
        component: function (resolve) {
          require(['@/components/calculators/RentalYield.vue'], resolve)
        }
      },
      {
        path: 'monthly-payment',
        name: 'monthlyPayment',
        component: function (resolve) {
          require(['@/components/calculators/MonthlyPayment.vue'], resolve)
        }
      },
      {
        path: 'how-much-can-i-borrow',
        name: 'howMuch',
        component: function (resolve) {
          require(['@/components/calculators/HowMuch.vue'], resolve)
        }
      },
      {
        path: 'is-this-property-a-good-investment',
        name: 'goodInvestment',
        component: function (resolve) {
          require(['@/components/calculators/GoodInvestment.vue'], resolve)
        }
      }]
  }]
})

router.beforeEach((to, from, next) => {
  var state = Object.assign(router.app.$options.store.state.app, {
    isLoading: true
  })
  router.app.$options.store.commit('APP_LOADING', state)

  next()
})

router.afterEach((to, from, next) => {
  const state = Object.assign(router.app.$options.store.state.app, {
    isLoading: false
  })
  router.app.$options.store.commit('APP_LOADING', state)
})

function guardRoute (to, from, next) {
  const auth = router.app.$options.store.state.auth

  if (!auth.isLoggedIn) {
    next({
      path: '/registration',
      query: {
        redirect: to.fullPath
      }
    })
  } else {
    next()
  }
}

export default router
