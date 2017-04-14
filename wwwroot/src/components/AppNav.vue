<template>
  <nav class="navbar navbar-toggleable-md navbar-inverse bg-inverse fixed-top">
    <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbar-collapse" aria-controls="navbar-collapse" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <router-link to="home" class="navbar-brand">Landlords</router-link>

    <div class="collapse navbar-collapse" id="navbar-collapse">
      <ul class="navbar-nav mr-auto">
        <router-link tag="li" to="/" active-class="active" class="nav-item" exact>
          <a class="nav-link">Dashboard <span class="sr-only">(current)</span></a>
        </router-link>
        <router-link tag="li" to="/registration" active-class="active" class="nav-item" exact>
          <a class="nav-link">Registration</a>
        </router-link>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  data () {
    return {
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
