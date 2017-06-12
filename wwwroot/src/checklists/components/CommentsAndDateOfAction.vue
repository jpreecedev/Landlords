<template>
  <div class="row">
    <div class="col-xs-12 col-md-6">
      <md-input-container>
        <label v-bind:for="checklistItem.key + 'comments'">Comments</label>
        <md-textarea v-model="checklistItem.payload.comments" @blur="save()" class="form-control" v-bind:id="checklistItem.key + 'comments'" v-bind:name="checklistItem.key + 'comments'" type="text"></md-textarea>                    
      </md-input-container>
    </div>
    <div class="col-xs-12 col-md-6">
      <md-input-container>
        <label v-bind:for="checklistItem.key + 'actioned'">Actioned</label>
        <md-input v-model="checklistItem.payload.actioned" @input="save()" :id="checklistItem.key + 'actioned'" :name="checklistItem.key + 'actioned'" type="date" />
      </md-input-container>
    </div>
  </div>
</template>

<script>
export default {
  name: 'comments-date-of-action',
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
      permissions: this.$store.state.permissions
    }
  },
  created () {
    if (!this.checklistItem.payload) {
      this.checklistItem.payload = {
        comments: null,
        actioned: null
      }
    } else {
      this.checklistItem.payload = JSON.parse(this.checklistItem.payload)
    }
  },
  methods: {
    save: function () {
      if (this.permissions.CI_ApplyTemplate) {
        this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, this.checklistItem.payload)
      }
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
