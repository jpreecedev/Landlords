import Vue from 'vue'
import router from 'root/routes'
import store from 'store'
import utils from '../utils'

const LOGIN_URL = '/token'
const PERMISSIONS_URL = '/api/permissions'

const AUTH_BASIC_HEADERS = {
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded'
  },
  emulateJSON: true
}

/**
 * Auth Plugin
 *
 * (see https://vuejs.org/v2/guide/plugins.html for more info on Vue.js plugins)
 *
 * Handles login and token authentication using OAuth2.
 */
export default {
  /**
   * Install the Auth class.
   *
   * Creates a Vue-resource http interceptor to handle automatically adding auth headers
   * and refreshing tokens. Then attaches this object to the global Vue (as Vue.auth).
   *
   * @param {Object} Vue The global Vue.
   * @param {Object} options Any options we want to have in our plugin.
   * @return {void}
   */
  install (Vue, options) {
    Vue.http.interceptors.push((request, next) => {
      const token = store.state.auth.accessToken
      const hasAuthHeader = request.headers.has('Authorization')

      if (token && !hasAuthHeader) {
        this._setAuthHeader(request)
      }

      next((response) => {
        if (this._isInvalidToken(response)) {
          this.logout(true)
          return response
        }
      })
    })

    Vue.prototype.$auth = Vue.auth = this
  },

  /**
   * Login
   *
   * @param {Object.<string>} creds The username and password for logging in.
   * @return {Promise}
   */
  login (creds) {
    const body = 'username=' + creds.username + '&password=' + creds.password

    return new Promise((resolve, reject) => {
      Vue.http.post(LOGIN_URL, body, AUTH_BASIC_HEADERS)
        .then(postResponse => {
          this._storeToken(postResponse)

          Vue.http.get(PERMISSIONS_URL)
            .then(response => {
              this._storePermissions(response)
              resolve(response)
            })
            .catch(response => {
              reject(this._parseValidationMessages(response))
            })
        })
        .catch(postResponse => {
          reject(this._parseValidationMessages(postResponse))
        })
    })
  },

  /**
   * Logout
   *
   * Clear all data in our Vuex store (which resets logged-in status) and redirect back
   * to login form.
   *
   * @param {Expired} expired States if the session expired or not
   * @return {void}
   */
  logout (expired) {
    store.commit('CLEAR_ALL_DATA')

    let query
    if (expired) {
      query = {
        expired: true
      }
    } else {
      query = {
        loggedOut: true
      }
    }

    router.push({
      name: 'registration',
      query
    })
  },

  /**
   * Set the Authorization header on a Vue-resource Request.
   *
   * @param {Request} request The Vue-Resource Request instance to set the header on.
   * @return {void}
   */
  _setAuthHeader (request) {
    request.headers.set('Authorization', 'Bearer ' + store.state.auth.accessToken)
    // The demo Oauth2 server we are using requires this param, but normally you only set the header.
    /* eslint-disable camelcase */
    request.params.access_token = store.state.auth.accessToken
  },

  /**
   * Retry the original request.
   *
   * Let's retry the user's original target request that had recieved a invalid token response
   * (which we fixed with a token refresh).
   *
   * @param {Request} request The Vue-resource Request instance to use to repeat an http call.
   * @return {Promise}
   */
  _retry (request) {
    this.setAuthHeader(request)
    return Vue.http(request)
      .then((response) => {
        return response
      })
      .catch((response) => {
        return response
      })
  },

  /**
   * Store tokens
   *
   * Update the Vuex store with the access/refresh tokens received from the response from
   * the Oauth2 server.
   *
   * @private
   * @param {Response} response Vue-resource Response instance from an OAuth2 server.
   *      that contains our tokens.
   * @return {void}
   */
  _storeToken (response) {
    const auth = store.state.auth
    const user = store.state.user

    auth.isLoggedIn = true
    auth.accessToken = response.body.access_token
    auth.refreshToken = response.body.refresh_token

    user.name = response.body.name

    store.commit('UPDATE_AUTH', auth)
    store.commit('UPDATE_USER', user)
  },

  _storePermissions (response) {
    for (let key in store.state.permissions) {
      Vue.delete(store.state.permissions, key)
    }

    response.data.forEach(function (element) {
      Vue.set(store.state.permissions, element.routeId, element)
    })
    store.commit('UPDATE_PERMISSIONS', store.state.permissions)
  },

  /**
   * Check if the Vue-resource Response is an invalid token response.
   *
   * @private
   * @param {Response} response The Vue-resource Response instance received from an http call.
   * @return {boolean}
   */
  _isInvalidToken (response) {
    const status = response.status
    const error = response.statusText

    return (status === 401 && error === 'Unauthorized')
  },

  _parseValidationMessages (response) {
    let errors = []
    let validationResult = utils.getFormValidationErrors(response)

    validationResult.errors.forEach(validationError => {
      errors.push({
        key: validationError.key,
        message: validationError.messages[0]
      })
    })

    if (validationResult.status) {
      errors.push({
        key: 'GenericError',
        message: validationResult.status
      })
    }

    return errors
  }
}
