<template>
  <div class="row">
    <div class="col-12 col-md-5">
      <loginform />
    </div>
    <div class="col-12 col-md-7">
      <registerform />
    </div>
  </div>
</template>

<script>

  import LoginForm from './common/LoginForm.vue'
  import RegisterForm from './common/RegisterForm.vue'

  export default {
    name: 'registration',
    components: {
      'loginform': LoginForm,
      'registerform': RegisterForm
    }
  }

</script>
  
<style lang="scss" scoped>
  
</style>
