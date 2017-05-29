<template>
  <div class="card-block">
    <div class="form-group row">
      <div class="col">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'comments'">Comments</label>
        <textarea v-model="checklistItem.comments" class="form-control" v-bind:id="checklistItem.key + 'comments'" placeholder="Enter any comments here" v-bind:name="checklistItem.key + 'comments'" type="text" rows="4"></textarea>                    
      </div>
      <div class="col">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'actioned'">Date actioned</label>
        <datepicker v-model="checklistItem.actioned" id="checklistItem.key + 'actioned'" name="checklistItem.key + 'actioned'" placeholder="Select date..." input-class="form-control"></datepicker>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <p class="mb-2">Upload one or more supporting documents</p>
        <label class="custom-file">
          <input type="file" id="file" class="pointer custom-file-input">
          <span class="custom-file-control"></span>
        </label>
      </div>
    </div>
  </div>
</template>

<script>
import Datepicker from 'vuejs-datepicker'

export default {
  name: 'document-upload',
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
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
