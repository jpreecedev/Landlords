export default {

  install (Vue) {
    Vue.http.interceptors.push((request, next) => {
      var csrfCookie = this.getCookie('XSRF-TOKEN')
      if (csrfCookie) {
        request.headers.set('X-XSRF-TOKEN', csrfCookie)
      }
      next()
    })
  },
  getCookie (name) {
    var value = '; ' + document.cookie
    var parts = value.split('; ' + name + '=')
    if (parts.length === 2) {
      return parts.pop().split(';').shift()
    }
  }
}
