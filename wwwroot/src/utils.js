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
  }

}
