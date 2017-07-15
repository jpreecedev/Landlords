module.exports = {

  /**
   * Get the error from a response.
   *
   * @param {Response} response The Vue-resource Response that we will try to get errors from.
   */
  getError (response) {
    if (response.bodyText) {
      return response.bodyText
    }
    debugger
  },

  /**
   * Get validation errors from the server response
   *
   * @param {Response} response The Vue-resource Response that we will try to get errors from.
   */
  getFormValidationErrors (response) {
    if (!response) {
      return []
    }
    let result = { status, errors: [] }

    if (response.data && response.data.errors) {
      response.data.errors.forEach(error => {
        if (error.key === 'GenericError') {
          result.status = error.value[0]
          return
        }

        let key = error.key
        error.key.split('[').forEach(item => {
          let squareBracket = item.indexOf(']')
          if (squareBracket === -1) {
            key = key.substring(0, 1).toLowerCase() + error.key.substring(1)
            if (key.indexOf('.') > -1) {
              key = key.split('.').map(dotItem => dotItem.substring(0, 1).toLowerCase() + dotItem.substring(1)).join('.')
            }
          } else {
            key = key.replace(item, item.substring(squareBracket + 3, 1).toLowerCase() + item.substring(squareBracket + 4))
          }
        })

        let messages = []

        error.value.forEach(validationMessage => {
          messages.push(validationMessage)
        })

        result.errors.push({ key, messages })
      })
    }
    if (response.status === 500 && response.statusText) {
      result.status = response.statusText
    }

    return result
  },

  /**
   * Maps the entity data so it can be displayed on the client
   */
  mapEntity (entityData, wrapperName, defaultsOnly) {
    if (!entityData) {
      return {}
    }

    let defaults = {}
    let props = {}

    for (let key in entityData) {
      if (key.substring(0, 7) === 'default') {
        defaults[key.substr(7, 1).toLowerCase() + key.substring(8)] = entityData[key]
      } else {
        props[key] = entityData[key]
      }
    }

    // TODO: Tidy up, spread operator not working

    let returnValue = {}
    for (let d in defaults) {
      returnValue[d] = defaults[d]
    }

    if (!defaultsOnly) {
      returnValue[wrapperName] = {}
      for (let p in props) {
        returnValue[wrapperName][p] = props[p]
      }
    }

    return returnValue
  },

  calculateMonthlyPayment (annualInterestRate, mortgageAmount, mortgageLength) {
    if (!annualInterestRate || !mortgageAmount || !mortgageLength) {
      return 0
    }

    let p = mortgageAmount
    let n = mortgageLength * 12
    let i = annualInterestRate / 100 / 12

    return parseFloat(p * i * (Math.pow(1 + i, n)) / (Math.pow(1 + i, n) - 1)).toFixed(2)
  },

  getPercentageScore (annualYield) {
    if (annualYield >= 10) {
      return 100
    }
    if (annualYield >= 7 && annualYield < 10) {
      return 80
    }
    if (annualYield >= 5 && annualYield < 7) {
      return 60
    }
    if (annualYield >= 2 && annualYield < 5) {
      return 30
    }
    return 0
  },

  getScoreDisplay (score) {
    if (score >= 80) {
      return 'Excellent'
    }
    if (score >= 50 && score < 80) {
      return 'Good'
    }
    if (score >= 30 && score < 50) {
      return 'Below average'
    }
    return 'Poor'
  },

  calculateScore (investmentData) {
    let annualYieldScore = this.getPercentageScore(investmentData.annualYield)
    let growthScore = this.getPercentageScore(investmentData.growth)

    let percentageAnnualProfit = (100 / investmentData.outgoings) * investmentData.income
    let actualScore = (percentageAnnualProfit + annualYieldScore + growthScore) / 3

    let scoreDisplay = this.getScoreDisplay(actualScore)

    return {
      profit: investmentData.income - investmentData.outgoings,
      actualScore,
      scoreDisplay
    }
  },

  pad (input) {
    if (!input) {
      return '00'
    }
    if (input.toString().length === 1) {
      return '0' + input
    }
    return input
  },

  getTimesForSelectList () {
    let result = []

    for (let i = 0; i < 24; i++) {
      for (let j = 0; j < 60; j += 15) {
        result.push({
          value: `1970-01-01T${this.pad(i) + ':' + this.pad(j) + ':00'}`,
          text: this.pad(i) + ':' + this.pad(j)
        })
      }
    }

    return result
  }
}
