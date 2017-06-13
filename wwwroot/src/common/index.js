import Modal from './Modal'
import portfolioPropertiesSelectList from './PortfolioPropertiesSelectList'

export default function install (Vue) {
  Vue.component('modal', Modal)
  Vue.component('portfolio-properties', portfolioPropertiesSelectList)
}
