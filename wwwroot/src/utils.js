module.exports = {

   /**
   * Get the error from a response.
   *
   * @param {Response} response The Vue-resource Response that we will try to get errors from.
   */
  getError: function (response) {
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
  getFormValidationErrors: function (response) {
    if (!response) {
      return []
    }
    var result = { status, errors: [] }

    if (response.data && response.data.errors) {
      response.data.errors.forEach(error => {
        if (error.key === 'GenericError') {
          result.status = error.value[0]
          return
        }

        var key = error.key.substring(0, 1).toLowerCase() + error.key.substring(1)
        var messages = []

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
  mapEntity: function (entityData, wrapperName, defaultsOnly) {
    if (!entityData) {
      return {}
    }

    var defaults = {}
    var props = {}

    for (var key in entityData) {
      if (key.substring(0, 7) === 'default') {
        defaults[key.substr(7, 1).toLowerCase() + key.substring(8)] = entityData[key]
      } else {
        props[key] = entityData[key]
      }
    }

    // TODO: Tidy up, spread operator not working

    var returnValue = {}
    for (var d in defaults) {
      returnValue[d] = defaults[d]
    }

    if (!defaultsOnly) {
      returnValue[wrapperName] = {}
      for (var p in props) {
        returnValue[wrapperName][p] = props[p]
      }
    }
    return returnValue
  },

  calculateMonthlyPayment: function (annualInterestRate, mortgageAmount, mortgageLength) {
    if (!annualInterestRate || !mortgageAmount || !mortgageLength) {
      return 0
    }

    var p = mortgageAmount
    var n = mortgageLength * 12
    var i = annualInterestRate / 100 / 12

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

  calculateScore: function (investmentData) {
    var annualYieldScore = this.getPercentageScore(investmentData.annualYield)
    var growthScore = this.getPercentageScore(investmentData.growth)

    var percentageAnnualProfit = (100 / investmentData.outgoings) * investmentData.income
    var actualScore = (percentageAnnualProfit + annualYieldScore + growthScore) / 3

    var scoreDisplay = this.getScoreDisplay(actualScore)

    return {
      profit: investmentData.income - investmentData.outgoings,
      actualScore,
      scoreDisplay
    }
  }

}
