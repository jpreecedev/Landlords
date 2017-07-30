import defaultState from './defaultState'

export const STORAGE_KEY = 'landlords'

let syncedData = {
  auth: {
    isLoggedIn: false,
    accessToken: null,
    refreshToken: null
  },
  permissions: {},
  user: {
    name: null
  }
}

let notSyncedData = {
  notifications: Object.assign([], defaultState.notifications),
  properties: Object.assign([], defaultState.properties),
  newTenancy: Object.assign({}, defaultState.newTenancy)
}

// Sync with local storage.
if (localStorage.getItem(STORAGE_KEY)) {
  syncedData = JSON.parse(localStorage.getItem(STORAGE_KEY))
}

// Merge data and export it.
export const state = Object.assign({}, syncedData, notSyncedData)
