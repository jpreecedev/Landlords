<template>
  <select v-model="selectedChecklist" class="form-control">
    <option v-for="checklist in checklists" v-bind:value="checklist">{{ checklist.origin }}: {{ checklist.name }}</option>
  </select>  
</template>

<script>
export default {
  name: 'available-checklists',
  props: {
    'selectedChecklist': {
      type: Object,
      default: null
    },
    'checklists': {
      type: Array,
      default: []
    }
  },
  data () {
    return {
    }
  }
}
</script>
