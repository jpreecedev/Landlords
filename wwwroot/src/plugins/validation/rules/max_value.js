const maxValue = (value, length) => {
  if (value === undefined || value === null) {
    return 'Please enter a value'
  }
  return String(value) <= length ? true : 'Please enter no more than ' + length
}

export default maxValue
