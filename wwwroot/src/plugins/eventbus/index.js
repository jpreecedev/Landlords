import Vue from 'vue'
let EventBus = new Vue()

Vue.prototype.$bus = Vue.bus = {
  $emit (event, args) {
    EventBus.$emit(event, args)
  },
  $on (event, callback) {
    EventBus.$on(event, callback)
  }
}
