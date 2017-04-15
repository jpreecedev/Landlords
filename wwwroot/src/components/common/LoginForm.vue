<template>
  <div onload="load()">
    <h3>Sign In</h3>
    <form role="form">
      <div class="form-group row">
        <label for="emailAddressField" class="col-3">Email Address</label>
        <div class="col-9">
          <input type="email" class="form-control" id="emailAddressField" placeholder="Enter email">
        </div>
      </div>
      <div class="form-group row">
        <label for="passwordField" class="col-3">Password</label>
        <div class="col-9">
          <input type="password" class="form-control" id="passwordField" placeholder="Password">
        </div>
      </div>
      <button type="button" v-on:click="trigger()" class="btn btn-sm btn-default">Log in</button>
    </form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'loginform',
  methods: {
    trigger: function () {
      const credentials = {
        username: 'TEST',
        password: 'TEST123'
      }

      debugger

      this.$auth.login(credentials).then((response) => {
        debugger
        this.$http.get('/api/PropertyOverview').then(response => {
          debugger
        })
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
