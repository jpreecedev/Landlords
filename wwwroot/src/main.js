// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.

import Vue from 'vue'
import VueResource from 'vue-resource'
import router from './router'
import store from './store'
import './assets/styles/app.scss'
import App from './components/App'
import './extensions'
import Auth from './auth'
import Interceptors from './interceptors'
import VeeValidate from 'vee-validate'

require('bootstrap')

Vue.config.productionTip = false

Vue.use(VueResource)
Vue.use(Auth)
Vue.use(Interceptors)
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
