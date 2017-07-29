import moment from 'moment'

const notifications = []

const address = {
  street: null,
  townOrCity: null,
  postcode: null,
  countyOrRegion: null,
  country: null,
  yearsAtAddress: 0,
  monthsAtAddress: 0
}

const contact = {
  name: null,
  relationship: null,
  mainContactNumber: null,
  secondaryContactNumber: null
}

const tenant = {
  title: null,
  firstName: null,
  lastName: null,
  mainContactNumber: null,
  secondaryContactNumber: null,
  emailAddress: null,
  dateOfBirth: '1970-01-01',
  isLeadTenant: true,
  isAdult: true,
  addresses: [Object.assign({}, address)],
  contacts: [Object.assign({}, contact)],
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

const newTenancy = {
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
  tenants: [Object.assign({}, tenant)]
}

export default {
  notifications,
  address,
  contact,
  tenant,
  newTenancy,
  user: {},
  permissions: {},
  auth: {
    accessToken: null,
    isLoggedIn: false,
    refreshToken: null
  }
}
