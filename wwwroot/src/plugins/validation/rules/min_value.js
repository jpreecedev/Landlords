const minValue = (value, length) => {
  if (value === undefined || value === null) {
    return 'Please enter a value'
  }
  return String(value) >= length ? true : 'Please enter at least ' + length
}

export default minValue
