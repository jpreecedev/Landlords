import { InvocationDescriptor } from './invocation-descriptor'
import { MessageType } from './message'

export default class Connection {
  constructor (url, enableLogging = false) {
    this.enableLogging = false
    this.clientMethods = {}
    this.open = false
    this.url = url
    this.socket = null
    this.enableLogging = enableLogging
    this.attempts = 1
  }

  attachMessageHandler () {
    this.socket.onmessage = (event) => {
      let message = JSON.parse(event.data)
      if (message.messageType === MessageType.Text) {
        this.log('Text message received. Message: ' + message.data)
      } else if (message.messageType === MessageType.MethodInvocation) {
        let invocationDescriptor = JSON.parse(message.data)
        this.clientMethods[invocationDescriptor.methodName].apply(this, invocationDescriptor.arguments)
      } else if (message.messageType === MessageType.ConnectionEvent) {
        this.connectionId = message.data
      }
      this.onMessage(message)
    }
  }

  log (message) {
    if (this.enableLogging) {
      console.log(message)
    }
  }

  start () {
    return new Promise((resolve) => {
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
    this.socket = new WebSocket(this.url)
    this.log('Connecting...')

    this.socket.onopen = () => {
      this.attempts = 1
      done()
    }

    this.socket.onclose = () => {
      var time = this.generateInterval(this.attempts)

      setTimeout(() => {
        this.attempts++
        this.createSocketWithBackoff(done)
      }, time)
    }
  }

  generateInterval (k) {
    let maxInterval = (Math.pow(2, k) - 1) * 1000

    if (maxInterval > 30 * 1000) {
      maxInterval = 30 * 1000
    }

    return Math.random() * maxInterval
  }
}
