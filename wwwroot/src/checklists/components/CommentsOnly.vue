<template>
  <div class="card-block">
    <div class="form-group row">
      <div class="col">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'comments'">Comments</label>
        <textarea v-model="checklistItem.comments" class="form-control" v-bind:id="checklistItem.key + 'comments'" placeholder="Enter any comments here" v-bind:name="checklistItem.key + 'comments'" type="text" rows="4"></textarea>                    
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'comments-only',
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
