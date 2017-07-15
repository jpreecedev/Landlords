export default {

  install (Vue) {
    Vue.http.interceptors.push((request, next) => {
      let csrfCookie = this.getCookie('XSRF-TOKEN')
      if (csrfCookie) {
        request.headers.set('X-XSRF-TOKEN', csrfCookie)
      }
      next()
    })
  },
  getCookie (name) {
    let value = '; ' + document.cookie
    let parts = value.split('; ' + name + '=')
    if (parts.length === 2) {
      return parts.pop().split(';').shift()
    }
  }
}
