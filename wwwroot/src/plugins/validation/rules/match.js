const match = (fieldName, a, b) => {
  if (!a || !b) {
    return `The ${fieldName} fields do not match`
  }
  return a === b ? true : `The ${fieldName} fields do not match`
}

export default match
