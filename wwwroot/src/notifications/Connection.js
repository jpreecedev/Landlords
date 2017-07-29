import { InvocationDescriptor } from './invocation-descriptor'
import { MessageType } from './message'

export default class Connection {
  constructor (url, enableLogging = false) {
    this.enableLogging = false
    this.clientMethods = {}
    this.connectionMethods = {}
    this.open = false
    this.url = url
    this.enableLogging = enableLogging
    this.connectionMethods['onConnected'] = () => {
      if (this.enableLogging) {
        console.log('Connected! connectionId: ' + this.connectionId)
      }
    }
    this.connectionMethods['onDisconnected'] = () => {
      if (this.enableLogging) {
        console.log('Connection closed from: ' + this.url)
      }
    }
    this.connectionMethods['onOpen'] = (socketOpenedEvent) => {
      if (this.enableLogging) {
        this.open = true
        console.log('WebSockets connection opened!', socketOpenedEvent)
      }
    }
    this.connectionMethods['onError'] = (event) => {
      if (this.enableLogging) {
        console.log('Websockets encountered an error!', event)
      }
    }
  }
  start () {
    this.socket = new WebSocket(this.url)
    this.socket.onopen = (event) => {
      this.connectionMethods['onOpen'].apply(this, event)
    }
    this.socket.onmessage = (event) => {
      this.message = JSON.parse(event.data)
      if (this.message.messageType === MessageType.Text) {
        if (this.enableLogging) {
          console.log('Text message received. Message: ' + this.message.data)
        }
      } else if (this.message.messageType === MessageType.MethodInvocation) {
        let invocationDescriptor = JSON.parse(this.message.data)
        this.clientMethods[invocationDescriptor.methodName].apply(this, invocationDescriptor.arguments)
      } else if (this.message.messageType === MessageType.ConnectionEvent) {
        this.connectionId = this.message.data
        this.connectionMethods['onConnected'].apply(this)
      }
    }
    this.socket.onclose = (event) => {
      this.connectionMethods['onDisconnected'].apply(this)
    }
    this.socket.onerror = (event) => {
      if (this.enableLogging) {
        console.log('Error data: ' + event.error)
        this.connectionMethods['onError'].apply(this, event)
      }
    }
  }
  invoke (methodName, ...args) {
    let invocationDescriptor = new InvocationDescriptor(methodName, args)
    if (this.enableLogging) {
      console.log(invocationDescriptor)
    }
    this.socket.send(JSON.stringify(invocationDescriptor))
  }
}
