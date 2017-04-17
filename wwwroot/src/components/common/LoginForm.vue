<template>
  <div>
    <h3>Sign In</h3>
    <div class="alert alert-danger" v-if="error">
      {{ error }}
    </div>
    <form role="form">
      <div class="form-group row">
        <label for="emailAddressField" class="col-3">Email Address</label>
        <div class="col-9">
          <input type="email" class="form-control" id="emailAddressField" placeholder="Enter your email" v-model="credentials.username">
        </div>
      </div>
      <div class="form-group row">
        <label for="passwordField" class="col-3">Password</label>
        <div class="col-9">
          <input type="password" class="form-control" id="passwordField" placeholder="Password" v-model="credentials.password">
        </div>
      </div>
      <button @click="submit()" v-bind:disabled="loggingIn" class="btn btn-primary">Log in</button>
    </form>
    </div>
  </div>
</template>

<script>

import utils from '@/utils'

export default {
  name: 'loginform',
  data () {
    return {
      credentials: {
        username: '',
        password: ''
      },
      loggingIn: false,
      error: ''
    }
  },
  methods: {
    submit () {
      this.loggingIn = true

      const credentials = {
        username: this.credentials.username,
        password: this.credentials.password
      }

      this.$auth.login(credentials, 'dashboard').then((response) => {
        this.loggingIn = false
        this.error = utils.getError(response)
      })
    }
  }
}
</script>

<style lang="scss" scoped>

  h3 {
    text-align: center;
  }

</style>
