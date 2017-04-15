import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/registration',
      name: 'registration',
      component: function (resolve) {
        require(['@/components/Registration.vue'], resolve)
      }
    },
    {
      path: '/',
      name: 'dashboard',
      component: function (resolve) {
        require(['@/components/Dashboard.vue'], resolve)
      },
      beforeEnter: guardRoute
    }
  ]
})

function guardRoute (to, from, next) {
  const auth = router.app.$options.store.state.auth

  if (!auth.isLoggedIn) {
    next({
      path: '/registration',
      query: { redirect: to.fullPath }
    })
  } else {
    next()
  }
}

export default router
