import moment from 'moment'
import Vue from 'vue'

Vue.filter('formatDateTime', function (value) {
  if (value) {
    return moment(String(value)).format('HH:mm DD/MM/YYYY')
  }
})
