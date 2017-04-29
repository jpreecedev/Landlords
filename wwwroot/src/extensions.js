/* eslint no-extend-native: off */
Number.prototype.formatWithSeparator = function () {
  return this.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',')
}

String.prototype.toFloat = function () {
  if (!this) {
    return 0
  }

  try {
    return parseFloat(this)
  } catch (x) {
    return 0
  }
}

Number.prototype.toFloat = function () {
  if (!this) {
    return 0
  }
  return this.toString().toFloat()
}
