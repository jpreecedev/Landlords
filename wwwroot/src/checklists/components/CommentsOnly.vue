<template>
  <div class="card-block">
    <div class="form-group row">
      <div class="col">
        <label class="col-form-label" v-bind:for="checklistItem.key + 'comments'">Comments</label>
        <textarea v-model="checklistItem.payload.comments" @blur="save()" class="form-control" v-bind:id="checklistItem.key + 'comments'" placeholder="Enter any comments here" v-bind:name="checklistItem.key + 'comments'" type="text" rows="4"></textarea>                    
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'comments-only',
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
        comments: null
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
