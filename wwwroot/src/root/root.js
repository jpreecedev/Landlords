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
import Vuetify from 'vuetify'

import CommonComponents from 'src/common'
import Journeys from 'journeys'

import 'src/extensions'
import 'src/filters'
import 'v-autocomplete/dist/v-autocomplete.css'

import 'vuetify/dist/vuetify.min.css'
import 'assets/styles/app.scss'

Vue.config.productionTip = false

Vue.use(VueResource)
Vue.use(AuthService)
Vue.use(Interceptors)
Vue.use(VeeValidate, {
  errorBagName: 'errorBag'
})
Vue.use(Autocomplete)

Vue.use(VueMaterial.MdCore)
Vue.use(VueMaterial.MdButton)
Vue.use(VueMaterial.MdCheckbox)
Vue.use(VueMaterial.MdRadio)
Vue.use(VueMaterial.MdSelect)
Vue.use(VueMaterial.MdMenu)
Vue.use(VueMaterial.MdList)
Vue.use(VueMaterial.MdBackdrop)
Vue.use(VueMaterial.MdInputContainer)
Vue.use(VueMaterial.MdIcon)
Vue.use(VueMaterial.MdToolbar)
Vue.use(VueMaterial.MdLayout)
Vue.use(VueMaterial.MdAvatar)
Vue.use(VueMaterial.MdTable)
Vue.use(VueMaterial.MdCard)
Vue.use(VueMaterial.MdWhiteframe)
Vue.use(VueMaterial.MdFile)
Vue.use(VueMaterial.MdProgress)
Vue.use(VueMaterial.MdDialog)
Vue.use(VueMaterial.MdTabs)

Vue.use(Vuetify)

Vue.use(CommonComponents)
Vue.use(Journeys)

new Vue({
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
