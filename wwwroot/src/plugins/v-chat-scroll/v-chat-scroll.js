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
    let timeout
    let scrolled = false

    el.addEventListener('scroll', e => {
      if (timeout) window.clearTimeout(timeout)
      timeout = window.setTimeout(function () {
        scrolled = el.scrollTop + el.clientHeight + 1 < el.scrollHeight
      }, 200)
    });

    (new MutationObserver(e => {
      let config = binding.value || {}
      let pause = config.always === false && scrolled
      if (pause || e[e.length - 1].addedNodes.length !== 1) return
      scrollToBottom(el)
    }).observe(el, {
      childList: true
    }))
  },
  inserted: scrollToBottom
}

export default vChatScroll
