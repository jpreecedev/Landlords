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
  }

}
