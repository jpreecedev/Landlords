<template>
  <div class="row"
       v-if="checklistItem.payload">
    <div class="col-xs-12 col-md-4">
      <date-picker v-model="checklistItem.payload.actioned"
                   @input="save()"
                   label="Actioned">
      </date-picker>
    </div>
    <div class="col-xs-12">
      <text-field v-model="checklistItem.payload.comments"
                  :multiline="true"
                  :rows="1"
                  @blur="save()"
                  label="Comments">
      </text-field>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
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
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  mounted () {
    if (!this.checklistItem.payload) {
      this.checklistItem.payload = {
        comments: null,
        actioned: null
      }
    }
  },
  methods: {
    save () {
      if (this.permissions.CI_ApplyTemplate) {
        this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, this.checklistItem.payload)
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
