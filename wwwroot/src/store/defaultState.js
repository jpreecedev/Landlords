import moment from 'moment'

const notifications = [
  { id: 'abc123', propertyDetailsId: '132b929e-834a-4bb2-eec9-08d4ab1fb30f', type: 'Notice', message: 'Rent is due' },
  { id: 'abc123', propertyDetailsId: '132b929e-834a-4bb2-eec9-08d4ab1fb30f', type: 'Important', message: 'Your tenant has messaged you' },
  { id: 'abc123', propertyDetailsId: '132b929e-834a-4bb2-eec9-08d4ab1fb30f', type: 'Immediate', message: 'Your boiler needs an inspection' },
  { id: 'abc123', propertyDetailsId: '132b929e-834a-4bb2-eec9-08d4ab1fb30f', type: 'Overdue', message: 'Rent is overdue' }
]

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
  newTenancy
}
