export let MessageType;
(function (MessageType) {
  MessageType[MessageType['Text'] = 0] = 'Text'
  MessageType[MessageType['MethodInvocation'] = 1] = 'MethodInvocation'
  MessageType[MessageType['ConnectionEvent'] = 2] = 'ConnectionEvent'
})(MessageType || (MessageType = {}))
export class Message {}
