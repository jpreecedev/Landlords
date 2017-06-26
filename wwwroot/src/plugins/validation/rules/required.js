const required = value => {
  if (Array.isArray(value)) {
    return !!value.length
  }

  if (value === undefined || value === null) {
    return 'This field is required'
  }

  return String(value).trim().length ? true : 'This field is required'
}

export default required
