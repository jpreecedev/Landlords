<template>
  <div>
    <h3>Register</h3>
    <form role="form" class="form-horizontal">
      <fieldset>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="firstName">First name</label>
          <div class="col-9">
            <input class="form-control" id="firstName" name="firstName" type="text" placeholder="John" required="">
          </div>
        </div>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="lastName">Last name</label>
          <div class="col-9">
            <input id="lastName" name="lastName" type="text" placeholder="Smith" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="email">E-mail</label>
          <div class="col-9">
            <input id="email" name="email" type="text" placeholder="john.smith@mail.com" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Password input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="password">Password</label>
          <div class="col-9">
            <input id="password" name="password" type="password" placeholder="Enter your password" class="form-control input-md" required="">

          </div>
        </div>

        <!-- Password input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="repeatPassword">Repeat password </label>
          <div class="col-9">
            <input id="repeatPassword" name="repeatPassword" type="password" placeholder="Repeat your password" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Button -->              
        <div class="text-center">
          <button id="register" name="register" class="btn btn-primary">Register</button>
          <button id="login" name="login" class="btn btn-secondary">Go to login page</button>
        </div>

      </fieldset>
    </form>
  </div>
</template>

<script>
export default {
  name: 'registerform'
}
</script>
  
<style lang="scss" scoped>
  
  h3 {
    text-align: center;
  }
  
</style>
