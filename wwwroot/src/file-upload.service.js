import Vue from 'vue'

export default {
  upload: (formData, url) => {
    return Vue.http.post(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
    .then((response) => Promise.resolve(response.data))
    .catch((error) => Promise.reject(error))
  }
}
