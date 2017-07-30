import Connection from 'src/notifications/Connection'

export default {
  install (Vue, options) {
    Vue.prototype.$notifications = Vue.notifications = this

    let url = 'ws://localhost:52812/notifications'
    let enableLogging = process.env.NODE_ENV !== 'production'

    this.connection = new Connection(url, enableLogging)
  },
  open () {
    return new Promise((resolve) => {
      this.connection.start()
        .then(() => {
          resolve()
        })
    })
  },
  get (accessToken, method) {
    return new Promise((resolve, reject) => {
      this.connection.clientMethods[method] = response => {
        resolve(response)
      }
      this.connection.invoke(method, accessToken, this.connection.connectionId)
    })
  }
}
