import Connection from 'src/notifications/Connection'

export default {
  install (Vue, options) {
    Vue.prototype.$notifications = Vue.notifications = this
  },
  open (accessToken) {
    if (!accessToken) {
      return Promise.reject(new Error('User must be authenticated'))
    }

    let url = `ws://localhost:52812/notifications?access_token=${accessToken}`
    let enableLogging = process.env.NODE_ENV !== 'production'

    if (!this.connection) {
      this.connection = new Connection(url, enableLogging)
    }

    return new Promise((resolve) => {
      this.connection.start()
        .then(() => {
          resolve()
        })
    })
  },
  get (method) {
    return new Promise((resolve, reject) => {
      this.connection.invoke(method, this.connection.connectionId)
      resolve()
    })
  }
}
