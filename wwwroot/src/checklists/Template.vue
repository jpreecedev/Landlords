<template>
  <main class="container">
    <div>
      <h1>Create new checklist template</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div v-if="!permissions.CL_CreateTemplate" class="alert alert-danger">
      Sorry this resource is currently unavailable
    </div>
  </main>
</template>

<script>
  export default {
    name: 'overview',
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
