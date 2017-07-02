<template>
  <div class="row">
    <div class="col-xs-12">
      <v-text-field v-model="checklistItem.payload.comments"
                    :multi-line="true"
                    :rows="1"
                    :auto-grow="true"
                    @blur="save()"
                    label="Comments">
      </v-text-field>
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
