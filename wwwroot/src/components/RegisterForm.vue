<template>
  <div class="row">
    <div class="col-xs-12">
      <div class="panel panel-default">
        <div class="panel-heading">
          <h3 class="panel-title"><strong>Register</strong></h3>
        </div>
        <div class="panel-body">
          <form role="form" class="form-horizontal">
            <fieldset>

              <!-- Text input-->
              <div class="form-group">
                <label class="col-md-4 control-label" for="firstName">First name</label>
                <div class="col-md-8">
                  <input id="firstName" name="firstName" type="text" placeholder="John" class="form-control input-md" required="">

                </div>
              </div>

              <!-- Text input-->
              <div class="form-group">
                <label class="col-md-4 control-label" for="lastName">Last name</label>
                <div class="col-md-8">
                  <input id="lastName" name="lastName" type="text" placeholder="Smith" class="form-control input-md" required="">

                </div>
              </div>

              <!-- Text input-->
              <div class="form-group">
                <label class="col-md-4 control-label" for="email">E-mail</label>
                <div class="col-md-8">
                  <input id="email" name="email" type="text" placeholder="john.smith@mail.com" class="form-control input-md" required="">

                </div>
              </div>

              <!-- Password input-->
              <div class="form-group">
                <label class="col-md-4 control-label" for="password">Password</label>
                <div class="col-md-8">
                  <input id="password" name="password" type="password" placeholder="Enter your password" class="form-control input-md"
                    required="">

                </div>
              </div>

              <!-- Password input-->
              <div class="form-group">
                <label class="col-md-4 control-label" for="repeatPassword">Repeat password </label>
                <div class="col-md-8">
                  <input id="repeatPassword" name="repeatPassword" type="password" placeholder="Repeat your password" class="form-control input-md"
                    required="">

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
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'registerform'
}
</script>
  
<style scoped>
  
</style>
