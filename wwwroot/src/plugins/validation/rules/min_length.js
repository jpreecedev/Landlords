const minLength = (value, length) => {
  if (value === undefined || value === null) {
    return 'Please enter a value'
  }
  return String(value).length >= length ? true : 'Please enter at least ' + length + ' characters in length'
}

export default minLength
