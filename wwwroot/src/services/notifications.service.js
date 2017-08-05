import Connection from 'src/notifications/Connection'

export default {
  install (Vue, options) {
    Vue.prototype.$notifications = Vue.notifications = this
  },
  open (accessToken) {
    let url = `ws://localhost:52812/notifications?access_token=${accessToken}`
    let enableLogging = process.env.NODE_ENV !== 'production'
    this.connection = new Connection(url, enableLogging)

    return new Promise((resolve) => {
      this.connection.start()
        .then(() => {
          resolve()
        })
    })
  },
  get (method) {
    return new Promise((resolve, reject) => {
      this.connection.clientMethods[method] = response => {
        resolve(response)
      }
      this.connection.invoke(method, this.connection.connectionId)
    })
  },
  listen (method) {
    return new Promise((resolve, reject) => {
      this.connection.clientMethods[method] = response => {
        resolve(response)
      }
    })
  }
}
