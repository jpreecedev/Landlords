import Vue from 'vue'
import VueResource from 'vue-resource'
import store from 'store'
import App from 'root/components/App'
import AuthService from 'services/auth.service'
import NotificationsService from 'services/notifications.service'
import Interceptors from 'src/interceptors'
import Autocomplete from 'v-autocomplete'
import router from './routes'
import Vuetify from 'vuetify'
import VueChatScroll from 'vue-chat-scroll'

import CommonComponents from 'src/common'

import Journeys from 'src/journeys'

import 'src/plugins'
import 'src/extensions'
import 'src/filters'
import 'v-autocomplete/dist/v-autocomplete.css'

import 'vuetify/dist/vuetify.min.css'
import 'assets/styles/app.scss'

Vue.config.productionTip = false

Vue.use(VueResource)
Vue.use(AuthService)
Vue.use(Interceptors)

Vue.use(Autocomplete)
Vue.use(Vuetify)

Vue.use(CommonComponents)
Vue.use(Journeys)
Vue.use(NotificationsService)
Vue.use(VueChatScroll)

new Vue({
  el: '#app',
  created () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
