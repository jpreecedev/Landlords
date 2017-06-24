import Vue from 'Vue'

let newTenancy = {
  step: 1,
  tenancy: {
    propertyDetails: {}
  },
  tenants: [{
    dateOfBirth: '1970-01-01',
    isLeadTenant: true,
    isAdult: true,
    address: {}
  }]
}

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

/**
 * Clear each property, one by one, so reactivity still works.
 *
 * (ie. clear out state.auth.isLoggedIn so Navbar component automatically reacts to logged out state,
 * and the Navbar menu adjusts accordingly)
 *
 * TODO: use a common import of default state to reset these values with.
 */
export const CLEAR_ALL_DATA = (state) => {
  // Auth
  state.auth.isLoggedIn = false
  state.auth.accessToken = null
  state.auth.refreshToken = null

  // Permissions
  for (var key in state.permissions) {
    Vue.delete(state.permissions, key)
  }

  // New tenancy journey
  state.newTenancy = newTenancy

  // User
  state.user.name = ''
}

export const TENANT_ADD_TENANT = (state, tenant) => {
  state.newTenancy.tenants.push(tenant)
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

export const TENANT_UPDATE_ADDRESS = (state, tenant) => {
  state.newTenancy.tenants[tenant.index].address = tenant.address
}

export const TENANCY_SELECTED_PROPERTY = (state, propertyId) => {
  state.newTenancy.tenancy.propertyDetails.id = propertyId
}

export const TENANCY_SELECTED_TENANCY_TYPE = (state, tenancyType) => {
  state.newTenancy.tenancy.tenancyType = tenancyType
}

export const TENANCY_SELECTED_START_DATE = (state, startDate) => {
  state.newTenancy.tenancy.startDate = startDate
}

export const TENANCY_SELECTED_END_DATE = (state, endDate) => {
  state.newTenancy.tenancy.endDate = endDate
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
