const maxValue = (value, length) => {
  if (value === undefined || value === null) {
    return 'This field is required'
  }
  return String(value) <= length ? true : 'Please enter no more than ' + length
}

export default maxValue
