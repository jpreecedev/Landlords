<template>
  <div>
    <h3>Register</h3>
    <div class="alert alert-danger" v-if="error">
      {{ error }}
    </div>
    <form role="form" class="form-horizontal">
      <fieldset>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="firstName">First name</label>
          <div class="col-9">
            <input v-model="newUser.firstName" class="form-control" id="firstName" name="firstName" type="text" placeholder="Your first name" required="">
          </div>
        </div>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="lastName">Last name</label>
          <div class="col-9">
            <input v-model="newUser.lastName" id="lastName" name="lastName" type="text" placeholder="Your last name" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Text input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="email">E-mail</label>
          <div class="col-9">
            <input v-model="newUser.emailAddress" id="email" name="email" type="text" placeholder="you@email.com" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Password input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="password">Password</label>
          <div class="col-9">
            <input v-model="newUser.password" id="password" name="password" type="password" placeholder="Enter your password" class="form-control input-md" required="">

          </div>
        </div>

        <!-- Password input-->
        <div class="form-group row">
          <label class="col-3 col-form-label" for="repeatPassword">Repeat password </label>
          <div class="col-9">
            <input v-model="newUser.repeatPassword" id="repeatPassword" name="repeatPassword" type="password" placeholder="Repeat your password" class="form-control input-md" required="">
          </div>
        </div>

        <!-- Button -->              
        <div class="text-center">
          <button @click="submit()" v-bind:disabled="registering" id="register" name="register" class="btn btn-primary">Register</button>
        </div>

      </fieldset>
    </form>
  </div>
</template>

<script>
  import utils from '@/utils'

  export default {
    name: 'registerform',
    data () {
      return {
        newUser: {
          firstName: '',
          lastName: '',
          emailAddress: '',
          password: '',
          repeatPassword: ''
        },
        registering: false,
        error: ''
      }
    },
    methods: {
      submit () {
        this.registering = true

        const registrationDetails = {
          firstName: this.newUser.firstName,
          lastName: this.newUser.lastName,
          emailAddress: this.newUser.emailAddress,
          password: this.newUser.password
        }

        this.$http.post('/api/register', registrationDetails).then((response) => {
          this.registering = false
          debugger
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
