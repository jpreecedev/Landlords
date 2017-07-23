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
  },
  validate ($elements) {
    return new Promise((resolve, reject) => {
      $elements.forEach($element => {
        if ($element.validate) {
          $element.validate(true)
        }
      })

      let success = $elements.every($element => {
        if (!$element._computedWatchers) {
          return true
        }

        let ownProperties = Object.getOwnPropertyNames($element._computedWatchers)
        let hasIsValid = ownProperties.some(item => {
          return item === 'isValid'
        })

        if (hasIsValid) {
          return $element.isValid
        }

        return true
      })

      if (success) {
        resolve()
      } else {
        reject(new Error('Form is invalid'))
      }
    })
  }
}
