<template>
  <div class="row">
    <div class="col-xs-12">
      <div class="panel panel-default">
      <div class="panel-heading">
        <h3 class="panel-title"><strong>Sign In </strong></h3>
      </div>
      <div class="panel-body">
        <form role="form">
          <div class="form-group">
            <label for="emailAddressField">Email Address</label>
            <input type="email" class="form-control" id="emailAddressField" placeholder="Enter email">
          </div>
          <div class="form-group">
            <label for="passwordField">Password</label>
            <input type="password" class="form-control" id="passwordField" placeholder="Password">
          </div>
          <button type="submit" class="btn btn-sm btn-default">Log in</button>
        </form>
      </div>
    </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'loginform'
}
</script>

<style lang="scss" scoped>

</style>
