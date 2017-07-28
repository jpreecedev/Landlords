import moment from 'moment'

// Set the key we'll use in local storage.
// Go to Chrome dev tools, application tab, click "Local Storage" and "http://localhost:8080"
// and you'll see this key set below (if logged in):
export const STORAGE_KEY = 'landlords'

let notifications = [
  { id: 'abc123', type: 'Important', message: 'Your tenant has messaged you' },
  { id: 'abc123', type: 'Notice', message: 'Rent is due' },
  { id: 'abc123', type: 'Immediate', message: 'Your boiler needs an inspection' },
  { id: 'abc123', type: 'Overdue', message: 'Rent is overdue' }
]
let defaultAddress = {
  street: null,
  townOrCity: null,
  postcode: null,
  country: null,
  yearsAtAddress: null,
  monthsAtAddress: null
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

let syncedData = {
  auth: {
    isLoggedIn: false,
    accessToken: null,
    refreshToken: null
  },
  permissions: {},
  user: {
    name: null
  }
}

let notSyncedData = {
  notifications: notifications,
  newTenancy: newTenancy
}

// Sync with local storage.
if (localStorage.getItem(STORAGE_KEY)) {
  syncedData = JSON.parse(localStorage.getItem(STORAGE_KEY))
}

// Merge data and export it.
export const state = Object.assign({}, syncedData, notSyncedData)
