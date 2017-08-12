import Vue from 'vue'
import { InvocationDescriptor } from './invocation-descriptor'
import { MessageType } from './message'

export default class Connection {
  constructor (url, enableLogging = false) {
    this.enableLogging = false
    this.open = false
    this.url = url
    this.socket = null
    this.enableLogging = enableLogging
    this.attempts = 1
    this.startPromise = null
    this.forceKill = false
  }

  attachMessageHandler () {
    this.socket.onmessage = (event) => {
      let message = JSON.parse(event.data)
      if (message.messageType === MessageType.Text) {
        this.log('Text message received. Message: ' + message.data)
      } else if (message.messageType === MessageType.MethodInvocation) {
        let invocationDescriptor = JSON.parse(message.data)
        Vue.bus.$emit(invocationDescriptor.methodName, ...invocationDescriptor.arguments)
      } else if (message.messageType === MessageType.ConnectionEvent) {
        this.connectionId = message.data
      }
      this.onMessage(message)
    }
  }

  log (message, args = null) {
    if (this.enableLogging) {
      console.log(message, args)
    }
  }

  start () {
    this.startPromise = new Promise((resolve) => {
      this.createWebSocket()
        .then(() => {
          this.onMessage = message => {
            if (message.messageType === MessageType.ConnectionEvent) {
              this.log('Connected', message)
              resolve()
            } else {
              this.log(message)
            }
          }
          this.attachMessageHandler()
        })
    })

    return this.startPromise
  }

  invoke (methodName, ...args) {
    let invocationDescriptor = new InvocationDescriptor(methodName, args)
    this.log(invocationDescriptor)
    this.socket.send(JSON.stringify(invocationDescriptor))
  }

  createWebSocket () {
    return new Promise((resolve) => {
      this.createSocketWithBackoff(() => {
        resolve()
      })
    })
  }

  createSocketWithBackoff (done) {
    this.socket = this.getWebSocket(this.url)
    this.log(`Connecting... (${this.attempts})`)

    if (this.socket.readyState === 1) {
      this.log('Connected', this.socket)
      this.attempts = 1
      done()
    }

    this.socket.onopen = () => {
      this.log('Connected', this.socket)
      this.attempts = 1
      done()
    }

    this.socket.onclose = () => {
      if (this.forceKill) {
        this.forceKill = false
        return
      }

      var time = this.generateInterval(this.attempts)

      setTimeout(() => {
        this.attempts++
        this.createSocketWithBackoff(done)
      }, time)
    }
  }

  getWebSocket (url) {
    if (!this.socket || [2, 3].find(item => item === this.socket.readyState)) {
      return new WebSocket(url)
    }
    return this.socket
  }

  generateInterval (k) {
    let maxInterval = (Math.pow(2, k) - 1) * 1000

    if (maxInterval > 30 * 1000) {
      maxInterval = 30 * 1000
    }

    return Math.random() * maxInterval
  }

  kill () {
    this.forceKill = true
    this.socket.close()
    this.log('Closed')
  }
}
