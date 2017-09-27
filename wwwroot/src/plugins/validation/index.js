import Vue from 'vue'

import alpha from './rules/alpha'
import email from './rules/email'
import maxLength from './rules/max_length'
import maxValue from './rules/max_value'
import minLength from './rules/min_length'
import minValue from './rules/min_value'
import required from './rules/required'
import match from './rules/match'
import invoiceLineRequired from './rules/invoiceLineRequired'

function filter (arr, testCallback) {
  let matches = []
  if (!Array.isArray(arr)) {
    return matches
  }

  arr.forEach($element => {
    if (testCallback($element)) {
      matches.push($element)
    } else {
      let childResults = filter($element.$children, testCallback)
      if (childResults.length) {
        matches.push(...childResults)
      }
    }
  })
  return matches
}

Vue.prototype.$validation = {
  rules: {
    alpha,
    email,
    max_length: maxLength,
    max_value: maxValue,
    min_length: minLength,
    min_value: minValue,
    match,
    required,
    invoice_line_required: invoiceLineRequired
  },
  validate ($elements) {
    return new Promise((resolve, reject) => {
      let success = true
      let results = filter($elements, $element => {
        return typeof $element.commit === 'function' && typeof $element.validate === 'function'
      })

      if (results) {
        console.log(results)
        results.forEach($element => $element.validate(true))
        success = results.every($element => $element.isValid)
      }

      if (success) {
        resolve()
      } else {
        reject(new Error('Form is invalid'))
      }
    })
  },
  reset ($elements) {
    let results = filter($elements, $element => {
      return typeof $element.reset === 'function'
    })
    if (results) {
      results.forEach($element => $element.reset())
    }
  },
  commit ($elements) {
    let results = filter($elements, $element => {
      return typeof $element.commit === 'function'
    })
    if (results) {
      results.forEach($element => $element.commit())
    }
  }
}
