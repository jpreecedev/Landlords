import Connection from 'src/notifications/Connection'

export default {
  install (Vue, options) {
    Vue.prototype.$notifications = Vue.notifications = this

    let url = 'ws://localhost:52812/notifications'
    let enableLogging = false

    this.connection = new Connection(url, enableLogging)
  },
  open () {
    return new Promise((resolve, reject) => {
      if (!this.connection.open) {
        this.connection.connectionMethods.onConnected = () => {
          resolve()
        }
        this.connection.connectionMethods.onError = event => {
          reject(event.error)
        }
        this.connection.start()
      } else {
        resolve()
      }
    })
  },
  get (accessToken, method) {
    return new Promise((resolve, reject) => {
      this.connection.onError = event => {
        reject(event.error)
      }
      this.connection.clientMethods[method] = response => {
        resolve(response)
      }
      this.connection.invoke(method, accessToken, this.connection.connectionId)
    })
  }
}
