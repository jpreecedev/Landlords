<template>
  <div id="accordion" v-if="checklist && checklist.checklistItems && checklist.checklistItems.length" role="tablist" aria-multiselectable="true">
    <div class="card" v-for="item in checklist.checklistItems">
      <div class="card-header" role="tab" v-bind:id="item.key">
        <h5 class="mb-0">
          <div class="form-check">
          <label class="form-check-label">
            <input class="form-check-input" type="checkbox" v-model="item.isCompleted">                                        
            <span v-if="!item.isCompleted">{{ item.displayText }}</span>
            <del class="text-muted" v-if="item.isCompleted">{{ item.displayText }}</del>                    
          </label>
          <p class="pointer float-right d-inline-block mb-0 text-right" @click="expand(item)">{{ item.isExpanded ? 'Collapse' : 'Expand' }} {{ item.isExpanded ? '&#9660;' : '&#9658;' }}</p>                  
        </div>
        </h5>
      </div>
      <div v-bind:id="item.key" v-bind:class="{'show': item.isExpanded}" role="tabpanel" v-bind:aria-labelledby="item.key" class="collapse">
        <document-upload v-if="item.template === 'DocumentUpload'" :checklistItem="item" />
        <comments-date-of-action v-if="item.template === 'CommentsAndDateOfAction'" :checklistItem="item" />
        <comments-only v-if="item.template === 'CommentsOnly'" :checklistItem="item" />
        <date-of-action v-if="item.template === 'DateOfAction'" :checklistItem="item" />
      </div>
    </div>
  </div>  
</template>

<script>
import DocumentUpload from './DocumentUpload'
import CommentsDateOfAction from './CommentsAndDateOfAction'
import CommentsOnly from './CommentsOnly'
import DateOfAction from './DateOfAction'

export default {
  name: 'accordion',
  components: { DocumentUpload, CommentsDateOfAction, CommentsOnly, DateOfAction },
  props: {
    'checklist': {
      type: Object,
      default: null
    }
  },
  data () {
    return {
    }
  },
  methods: {
    expand: function (item) {
      item.isExpanded = !item.isExpanded
    }
  }
}
</script>
  
<style lang="scss" scoped>
  
</style>
