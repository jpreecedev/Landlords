/* eslint eqeqeq: "off" */

const invoiceLineRequired = (hasFormBeenSubmitted, value) => {
  if (!hasFormBeenSubmitted) {
    return true
  }

  if (value === undefined || value === null) {
    return 'This field is required'
  }

  let isCompletelyEmpty = !value.item && !value.description && !value.unitCost
  if (isCompletelyEmpty) {
    return true
  }

  let isPartiallyEmpty = !value.item || !value.description || !value.unitCost

  return isPartiallyEmpty ? 'This field is required' : true
}

export default invoiceLineRequired
