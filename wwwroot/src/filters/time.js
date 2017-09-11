import moment from 'moment'
import Vue from 'vue'

Vue.filter('formatTime', function (value) {
  if (value) {
    return moment(String(value)).format('HH:mm')
  }
})
