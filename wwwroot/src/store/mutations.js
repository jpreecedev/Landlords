import Vue from 'Vue'
import moment from 'moment'

let defaultAddress = {
  street: null,
  townOrCity: null,
  postcode: null,
  countyOrRegion: null,
  country: null,
  yearsAtAddress: 0,
  monthsAtAddress: 0
}
let defaultContact = {
  name: null,
  relationship: null,
  mainContactNumber: null,
  secondaryContactNumber: null
}
let defaultTenant = {
  title: null,
  firstName: null,
  lastName: null,
  mainContactNumber: null,
  secondaryContactNumber: null,
  emailAddress: null,
  dateOfBirth: '1970-01-01',
  isLeadTenant: true,
  isAdult: true,
  addresses: [Object.assign({}, defaultAddress)],
  contacts: [Object.assign({}, defaultContact)],
  isSmoker: false,
  hasPets: false,
  employmentType: null,
  occupation: null,
  companyName: null,
  workAddress: null,
  workContactNumber: null,
  drivingLicenseReference: null,
  passportReference: null,
  additionalInformation: null
}

let newTenancy = {
  step: 1,
  tenancy: {
    startDate: moment().format('YYYY-MM-DD'),
    endDate: moment().add(12, 'M').subtract(1, 'day').format('YYYY-MM-DD'),
    rentalAmount: 0,
    rentalFrequency: null,
    rentalPaymentReference: null,
    tenancyType: null,
    propertyDetailsId: null
  },
  tenants: [Object.assign({}, defaultTenant)]
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
  for (let key in state.permissions) {
    Vue.delete(state.permissions, key)
  }

  // New tenancy journey
  state.newTenancy = newTenancy

  // User
  state.user.name = ''
}

export const TENANT_ADD_TENANT = (state, isAdult) => {
  let item = Object.assign({}, defaultTenant, { isLeadTenant: false, isAdult: isAdult })
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
  state.newTenancy.tenants[tenantIndex].addresses.push(Object.assign({}, defaultAddress))
}

export const TENANT_ADD_CONTACT = (state, tenantIndex) => {
  state.newTenancy.tenants[tenantIndex].contacts.push(Object.assign({}, defaultContact))
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
