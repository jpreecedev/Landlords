import Vue from 'vue'

import alpha from './rules/alpha'
import email from './rules/email'
import maxLength from './rules/max_length'
import maxValue from './rules/max_value'
import minLength from './rules/min_length'
import minValue from './rules/min_value'
import required from './rules/required'

Vue.prototype.$validation = {
  rules: {
    alpha,
    email,
    max_length: maxLength,
    max_value: maxValue,
    min_length: minLength,
    min_value: minValue,
    required
  }
}
