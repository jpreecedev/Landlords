import wizard from './wizard'
import portfolioPropertiesSelectList from './PortfolioPropertiesSelectList'

export default function install (Vue) {
  Vue.component('wizard', wizard)
  Vue.component('portfolio-properties', portfolioPropertiesSelectList)
}
