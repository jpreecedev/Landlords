<template>
  <div class="card-block">
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <date-picker v-model="checklistItem.payload"
                     @input="save()"
                     label="Actioned">
        </date-picker>
      </div>
    </div>
  </div>
</template>

<script>
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
  data () {
    return {
      permissions: this.$store.state.permissions
    }
  },
  methods: {
    save () {
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
