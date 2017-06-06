import Vue from 'vue'
import VueResource from 'vue-resource'
import App from 'admin/components/App'
import router from './routes'

Vue.config.productionTip = false

Vue.use(VueResource)

new Vue({
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  render: h => h(App)
})
