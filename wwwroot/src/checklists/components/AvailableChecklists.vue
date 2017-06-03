<template>
  <select v-model="selectedChecklist" class="form-control">
    <option v-for="checklist in checklists" v-bind:value="checklist" v-bind:disabled="checklist.isPropertyMandatory && propertyCount < 1">{{ checklist.origin }}: {{ checklist.name }}</option>
  </select>  
</template>

<script>
export default {
  name: 'available-checklists',
  props: {
    'propertyCount': {
      type: Number,
      default: 0
    },
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
