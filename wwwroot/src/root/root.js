import Vue from 'vue'
import VueResource from 'vue-resource'
import store from 'store'
import App from 'root/components/App'
import AuthService from 'services/auth.service'
import Interceptors from 'src/interceptors'
import VeeValidate from 'vee-validate'
import Autocomplete from 'v-autocomplete'
import router from './routes'
import VueMaterial from 'vue-material'

import 'vue-material/dist/vue-material.css'
import 'src/extensions'
import 'assets/styles/app.scss'
import 'v-autocomplete/dist/v-autocomplete.css'

require('bootstrap')

Vue.config.productionTip = false

Vue.use(VueResource)
Vue.use(AuthService)
Vue.use(Interceptors)
Vue.use(VeeValidate)
Vue.use(Autocomplete)

Vue.use(VueMaterial.MdCore)
Vue.use(VueMaterial.MdButton)

new Vue({
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
