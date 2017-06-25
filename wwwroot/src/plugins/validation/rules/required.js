const required = value => {
  if (Array.isArray(value)) {
    return !!value.length
  }

  if (value === undefined || value === null) {
    return 'This field is required'
  }

  return String(value).trim().length ? true : 'Please enter a valid value into this field'
}

export default required
