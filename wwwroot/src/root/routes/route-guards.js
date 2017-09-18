import router from './index'

export function guardRoute (to, from, next) {
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

export function redirectIfManager (to, from, next) {
  const auth = router.app.$options.store.state.auth

  if (!auth.isLoggedIn) {
    next({
      path: '/registration',
      query: {
        redirect: to.fullPath
      }
    })
  } else if ((auth.role === 'Landlord' || auth.role === 'Agency') && (to.name === 'maintenance-request-overview')) {
    next({
      path: '/maintenance/management'
    })
  } else {
    next()
  }
}
