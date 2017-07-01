import Vue from 'vue'

export default {

  upload (formData, url, progress) {
    return Vue.http.post(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      },
      progress (e) {
        if (e.lengthComputable && progress) {
          progress(((e.loaded / e.total) * 100).toFixed(0))
        }
      }
    })
      .then((response) => Promise.resolve(response.data))
      .catch((error) => Promise.reject(error))
  }

}
