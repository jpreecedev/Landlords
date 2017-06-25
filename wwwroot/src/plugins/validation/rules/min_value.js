const minValue = (value, length) => {
  if (value === undefined || value === null) {
    return 'This field is required'
  }
  return String(value) >= length ? true : 'Please enter at least ' + length
}

export default minValue
