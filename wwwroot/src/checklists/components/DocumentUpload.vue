<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <md-input-container>
          <label v-bind:for="checklistItem.key + 'comments'">Comments</label>
          <md-textarea v-model="checklistItem.comments" v-bind:id="checklistItem.key + 'comments'" placeholder="Enter any comments here" v-bind:name="checklistItem.key + 'comments'"></md-textarea>                    
        </md-input-container>
      </div>
      <div class="col-xs-12 col-md-6">
        <md-input-container>
          <label v-bind:for="checklistItem.key + 'actioned'">Actioned</label>
          <md-input v-model="checklistItem.actioned" id="checklistItem.key + 'actioned'" name="checklistItem.key + 'actioned'" type="date"></md-input>
        </md-input-container>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12">
        <md-input-container>
          <md-file v-model="file" placeholder="Browse for a file on your computer" />
        </md-input-container>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'document-upload',
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
  data () {
    return {
      file: null,
      permissions: this.$store.state.permissions
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
