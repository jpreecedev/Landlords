// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import App from './App'
import router from './router'

import LoginForm from './components/LoginForm'
import RegisterForm from './components/RegisterForm'

Vue.config.productionTip = false

Vue.use(VueResource)

Vue.component('loginform', LoginForm)
Vue.component('registerform', RegisterForm)

new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: {
    App
  }
})
