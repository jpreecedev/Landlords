<template>
  <main class="container">
    <div>
      <h1>Create new checklist template</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <permissions-warning :permission="permissions.CL_CreateTemplate" />
  </main>
</template>

<script>
  import PermissionsWarning from 'common/PermissionsWarning'

  export default {
    name: 'overview',
    components: { PermissionsWarning },
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
