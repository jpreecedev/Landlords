require('bootstrap')

// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import router from './router'
import store from './store'

import './assets/styles/app.scss'
import App from './components/App'

Vue.config.productionTip = false
Vue.use(VueResource)

import Auth from './auth'
Vue.use(Auth)

new Vue({
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
