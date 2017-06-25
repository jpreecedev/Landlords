import validator from 'validator'

const alpha = value => {
  if (validator.isAlpha(String(value))) {
    return true
  }
  return 'Please enter only letters'
}

export default alpha
