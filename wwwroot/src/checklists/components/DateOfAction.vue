<template>
  <div class="card-block">
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <md-input-container>
          <datepicker v-model="checklistItem.payload" @selected="save()" id="checklistItem.key + 'actioned'" name="checklistItem.key + 'actioned'" placeholder="Select date..."></datepicker>
        </md-input-container>
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
  data () {
    return {
      permissions: this.$store.state.permissions
    }
  },
  methods: {
    save: function () {
      if (this.permissions.CI_ApplyTemplate) {
        this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, {
          payload: this.checklistItem.payload
        })
      }
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
