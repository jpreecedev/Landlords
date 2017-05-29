<template>
  <div class="card-block">
    <div class="form-group row">
      <div class="col">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'actioned'">Date actioned</label>
        <datepicker v-model="checklistItem.actioned" id="checklistItem.key + 'actioned'" name="checklistItem.key + 'actioned'" placeholder="Select date..." input-class="form-control"></datepicker>
      </div>
    </div>
  </div>
</template>

<script>
import Datepicker from 'vuejs-datepicker'

export default {
  name: 'date-of-action',
  components: { Datepicker },
  props: {
    'checklistItem': {
      type: Object,
      default: null
    }
  },
  data () {
    return {
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
