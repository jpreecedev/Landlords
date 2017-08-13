import vChatScroll from './v-chat-scroll.js'

let VueChatScroll = {
  install: (Vue, options) => {
    Vue.directive('chat-scroll', vChatScroll)
  }
}

export default VueChatScroll

if (typeof window !== 'undefined' && window.Vue) {
  window.Vue.use(VueChatScroll)
}
