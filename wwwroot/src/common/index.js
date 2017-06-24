import portfolioPropertiesSelectList from './PortfolioPropertiesSelectList'
import ValidationGroup from './ValidationGroup'

export default function install (Vue) {
  Vue.component('portfolio-properties', portfolioPropertiesSelectList)
  Vue.component('validation-group', ValidationGroup)
}
