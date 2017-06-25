const maxLength = (value, length) => {
  if (value === undefined || value === null) {
    return 'This field is required'
  }
  return String(value).length <= length ? true : 'Please enter no more than ' + length + ' characters in length'
}

export default maxLength
