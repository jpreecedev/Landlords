<template>
  <div id="accordion" v-if="checklist && checklist.checklistItems && checklist.checklistItems.length" role="tablist" aria-multiselectable="true">
    <div class="card" v-for="item in checklist.checklistItems">
      <div class="card-header" role="tab" v-bind:id="item.key">
        <h5 class="mb-0">
          <div class="form-check">            
            <div class="sort" v-if="permissions.CI_Move">
              <span v-if="item.order < checklist.checklistItems.length" class="pointer" @click="move('down', item)" title="Move down">&darr;</span>
              <span v-if="item.order > 1" class="pointer" @click="move('up', item)" title="Move up">&uarr;</span>
            </div>
            <label class="form-check-label">
              <input class="form-check-input" v-if="permissions.CI_ToggleCompleted" type="checkbox" v-model="item.isCompleted" @change="toggleCompleted(item)">                                        
              <span v-if="!item.isCompleted">{{ item.displayText }}</span>
              <del class="text-muted" v-if="item.isCompleted">{{ item.displayText }}</del>                    
            </label>
            <p class="pointer float-right d-inline-block mb-0 text-right" @click="expand(item)">{{ item.isExpanded ? 'Collapse' : 'Expand' }} {{ item.isExpanded ? '&#9660;' : '&#9658;' }}</p>                  
          </div>
        </h5>
      </div>
      <div v-bind:id="item.key" v-bind:class="{'show': item.isExpanded}" role="tabpanel" v-bind:aria-labelledby="item.key" class="collapse">
        <document-upload v-if="item.template === 'DocumentUpload'" :checklistId="checklist.id" :checklistItem="item" />
        <comments-date-of-action v-if="item.template === 'CommentsAndDateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
        <comments-only v-if="item.template === 'CommentsOnly'" :checklistId="checklist.id" :checklistItem="item" />
        <date-of-action v-if="item.template === 'DateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
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
      permissions: this.$store.state.permissions
    }
  },
  methods: {
    expand: function (item) {
      var expanded = item.isExpanded
      this.$emit('collapseAll')
      item.isExpanded = !expanded
      this.$emit('toggleExpanded', item)
    },
    toggleCompleted: function (item) {
      this.$emit('toggleCompleted', item)
    },
    move: function (direction, item) {
      this.$emit('move', { direction, item })
    }
  }
}
</script>
  
<style lang="scss" scoped>
  .sort {
    display: inline-block;
    width: 30px;
    vertical-align: top;
  }
  .form-check-label{
    max-width: 80%;
  }
</style>
