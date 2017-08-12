const scrollToBottom = el => {
  let scrollableElement = findAncestor(el, 'scroll')
  scrollableElement.scrollTop = scrollableElement.scrollHeight
}

const findAncestor = (el, cls) => {
  while ((el = el.parentElement) && !el.classList.contains(cls));
  return el
}

const vChatScroll = {
  bind: (el, binding) => {
    (new MutationObserver(e => {
      scrollToBottom(el)
    }).observe(el, {
      childList: true
    }))
  },
  inserted: scrollToBottom
}

export default vChatScroll
