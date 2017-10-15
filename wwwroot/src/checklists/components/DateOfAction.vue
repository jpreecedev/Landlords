<template>
  <div class="card-block"
       v-if="checklistItem.payload">
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <date-picker v-model="checklistItem.payload.actioned"
                     @input="save()"
                     label="Actioned">
        </date-picker>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'date-of-action',
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
