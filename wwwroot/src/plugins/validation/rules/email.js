import validator from 'validator'

const email = value => {
  if (validator.isEmail(String(value))) {
    return true
  }
  return 'Please enter a valid email address'
}

export default email
