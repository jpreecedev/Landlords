import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
  mode: 'history',
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
    component: function (resolve) {
      require(['@/components/Manager.vue'], resolve)
    },
    children: [{
      path: 'property-details/:propertyId?',
      name: 'propertyDetails',
      component: function (resolve) {
        require(['@/components/PropertyDetails.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: 'property-list',
      name: 'propertyList',
      component: function (resolve) {
        require(['@/components/PropertyList.vue'], resolve)
      },
      beforeEnter: guardRoute
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
