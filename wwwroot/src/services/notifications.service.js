import Vue from 'vue'
import Connection from 'src/notifications/Connection'

var connection = null

function startConnection () {
  return new Promise((resolve) => {
    connection.start()
      .then(() => {
        resolve()
      })
  })
};

var notifications = {
  install (Vue, options) {
    Vue.prototype.$notifications = Vue.notifications = this
  },
  open (accessToken) {
    if (!accessToken) {
      return Promise.reject(new Error('User must be authenticated'))
    }

    let url = `ws://localhost:52812/notifications?access_token=${accessToken}`
    let enableLogging = process.env.NODE_ENV !== 'production'

    if (!connection) {
      connection = new Connection(url, enableLogging)
      return startConnection()
    } else {
      if (!connection.socket) {
        return startConnection()
      } else {
        switch (connection.socket.readyState) {
          case 0: // connecting
            return connection.startPromise
          case 1: // open
            return Promise.resolve()
          default:
            return startConnection()
        }
      }
    }
  },
  invoke (method) {
    return new Promise((resolve, reject) => {
      connection.invoke(method, connection.connectionId)
      Vue.bus.$on(method, resolve)
    })
  }
}

export default notifications
