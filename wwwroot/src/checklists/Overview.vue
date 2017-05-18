<template>
  <main class="container">
    <div>
      <h1>Overview</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div class="mt-5">
      <h2>Your checklists</h2>
      <p>You have not created any checklists yet.</p>
    </div>
    <div class="mt-5">
      <h2>Available checklists</h2>
      <select v-if="availableChecklists && availableChecklists.length">
        <option v-for="checklist in availableChecklists" v-bind:value="checklist.id">{{ checklist. }}</option>
      </select>
    </div>
  </main>
</template>

<script>
  export default {
    name: 'overview',
    data () {
      return {
        availableChecklists: []
      }
    }
  }
</script>
  
<style lang="scss" scoped>
  
</style>
