import Vue from 'vue'
import Vuex from 'vuex'
import { state } from './state'
import * as getters from './getters'
import * as mutations from './mutations'
import plugins from './plugins'

Vue.use(Vuex)

const store = new Vuex.Store({
  state,
  getters,
  mutations,
  plugins
})

export default store
