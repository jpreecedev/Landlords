<template>
  <v-expansion-panel v-if="checklist && checklist.checklistItems && checklist.checklistItems.length">
    <v-expansion-panel-content v-for="(item, index) in checklist.checklistItems" :key="index">
      <div slot="header">
        <label :for="'tab-' + index">
          <md-checkbox v-if="permissions.CI_ToggleCompleted" v-model="item.isCompleted" @change="toggleCompleted(item, $event)"></md-checkbox>
          <span v-if="!item.isCompleted">{{ item.displayText }}</span>
          <del class="text-muted" v-if="item.isCompleted">{{ item.displayText }}</del>
        </label>
      </div>
      <v-card>
        <v-card-text>
          <document-upload v-if="item.template === 'DocumentUpload'" :checklistId="checklist.id" :checklistItem="item" />
          <comments-date-of-action v-else-if="item.template === 'CommentsAndDateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
          <comments-only v-else-if="item.template === 'CommentsOnly'" :checklistId="checklist.id" :checklistItem="item" />
          <date-of-action v-else-if="item.template === 'DateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
        </v-card-text>
      </v-card>
    </v-expansion-panel-content>
  </v-expansion-panel>
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
    toggleCompleted: function (item, newState) {
      item.isCompleted = newState
      this.$emit('toggleCompleted', item)
    },
    move: function (direction, item) {
      this.$emit('move', { direction, item })
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
