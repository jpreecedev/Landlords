<template>
  <div class="card-block">
    <div class="form-group row">
      <div class="col-6">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'actioned'">Date actioned</label>
        <datepicker v-model="checklistItem.payload" @selected="save()" id="checklistItem.key + 'actioned'" name="checklistItem.key + 'actioned'" placeholder="Select date..." input-class="form-control"></datepicker>
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
    'checklistId': {
      type: String,
      default: null
    },
    'checklistItem': {
      type: Object,
      default: null
    }
  },
  methods: {
    save: function () {
      this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, {
        payload: this.checklistItem.payload
      })
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
