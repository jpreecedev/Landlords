import defaultState from './defaultState'

export const UPDATE_AUTH = (state, auth) => {
  state.auth = auth
}

export const UPDATE_USER = (state, user) => {
  state.user = user
}

export const APPNAV_SEARCH = (state, searchData) => {
  state.appnav = searchData
}

export const UPDATE_PERMISSIONS = (state, permissions) => {
  state.permissions = permissions
}

export const CLEAR_ALL_DATA = (state) => {
  Object.keys(defaultState).forEach(key => { state[key] = defaultState[key] })
}

export const TENANT_ADD_TENANT = (state, isAdult) => {
  let item = Object.assign({}, defaultState.tenant, { isLeadTenant: false, isAdult: isAdult })
  if (!isAdult) {
    item.addresses = null
    item.contacts = null
  }
  state.newTenancy.tenants.push(item)
}

export const TENANT_REMOVE_TENANT = (state, index) => {
  state.newTenancy.tenants.splice(index, 1)
}

export const TENANT_UPDATE_FIELD = (state, tenant) => {
  state.newTenancy.tenants.map((item, index) => {
    if (index !== tenant.index) {
      return item
    }

    item[tenant.field] = tenant[tenant.field]
    return item
  })
}

export const TENANCY_UPDATE = (state, obj) => {
  state.newTenancy.tenancy[obj.field] = obj[obj.field]
}

export const TENANCY_UPDATE_END_DATE = (state, endDate) => {
  state.newTenancy.tenancy.endDate = endDate
}

export const TENANT_ADD_ADDRESS = (state, tenantIndex) => {
  state.newTenancy.tenants[tenantIndex].addresses.push(Object.assign({}, defaultState.address))
}

export const TENANT_ADD_CONTACT = (state, tenantIndex) => {
  state.newTenancy.tenants[tenantIndex].contacts.push(Object.assign({}, defaultState.contact))
}

export const TENANT_DELETE_CONTACT = (state, obj) => {
  state.newTenancy.tenants[obj.tenantIndex].contacts.splice(obj.contactIndex, 1)
}

export const TENANT_UPDATE_ADDRESS = (state, obj) => {
  state.newTenancy.tenants[obj.tenantIndex].addresses[obj.addressIndex][obj.field] = obj.address[obj.field]
}

export const TENANT_UPDATE_CONTACT = (state, obj) => {
  state.newTenancy.tenants[obj.tenantIndex].contacts[obj.contactIndex][obj.field] = obj.contact[obj.field]
}

export const TENANCY_UPDATE_RENTAL_AMOUNT = (state, rentalAmount) => {
  state.newTenancy.tenancy.rentalAmount = Number.parseFloat(rentalAmount)
}

export const TENANCY_UPDATE_RENTAL_FREQUENCY = (state, rentalFrequency) => {
  state.newTenancy.tenancy.rentalFrequency = rentalFrequency
}

export const TENANCY_UPDATE_RENTAL_PAYMENT_REFERENCE = (state, rentalPaymentReference) => {
  state.newTenancy.tenancy.rentalPaymentReference = rentalPaymentReference
}

export const TENANCY_NEXT_STEP = (state, newTenancy) => {
  state.newTenancy.step = Number(newTenancy.step) + 1
}

export const TENANCY_PREVIOUS_STEP = (state, newTenancy) => {
  state.newTenancy.step = Number(newTenancy.step) - 1
}

export const UPDATE_NOTIFICATIONS = (state, notifications) => {
  state.notifications = notifications
}

export const SET_PROPERTIES = (state, properties) => {
  state.properties = properties
}
