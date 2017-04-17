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
    if (!response || !response.data || !response.data.errors) {
      return []
    }

    var result = {}
    response.data.errors.forEach(error => {
      var key = error.key.substring(0, 1).toLowerCase() + error.key.substring(1)
      var messages = []

      error.value.forEach(validationMessage => {
        messages.push(validationMessage)
      })

      result[key] = { messages }
    })
    return result
  }

}
