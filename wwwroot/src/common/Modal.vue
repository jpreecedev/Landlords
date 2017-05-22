<template>
  <div class="modal fade">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">{{ title }}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <p>{{ message }}</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" @click="$emit('confirmed')">{{ confirmation }}</button>
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'modal',
  props: {
    'title': {
      type: String,
      default: 'Modal title'
    },
    'message': {
      type: String,
      default: 'Enter message here'
    },
    'confirmation': {
      type: String,
      default: 'Save changes'
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
