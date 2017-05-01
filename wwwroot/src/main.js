require('bootstrap')

// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import router from './router'
import store from './store'

import './assets/styles/app.scss'
import App from './components/App'

import './extensions'

Vue.config.productionTip = false
Vue.use(VueResource)

import Auth from './auth'
Vue.use(Auth)

import Interceptors from './interceptors'
Vue.use(Interceptors)

import VeeValidate from 'vee-validate'
Vue.use(VeeValidate)

new Vue({
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
