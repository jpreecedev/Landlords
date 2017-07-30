<template>
  <v-expansion-panel v-if="checklist && checklist.checklistItems && checklist.checklistItems.length">
    <v-expansion-panel-content v-for="(item, index) in checklist.checklistItems" :key="index">
      <div slot="header">
        <v-checkbox v-if="permissions.CI_ToggleCompleted"
                    v-model="item.isCompleted"
                    class="accordion-checkbox"
                    color="primary"
                    @change="newValue => { toggleCompleted(newValue, item); }">
        </v-checkbox>
        <span v-if="!item.isCompleted" class="display-text">{{ item.displayText }}</span>
        <del v-else class="display-text text-muted">{{ item.displayText }}</del>
      </div>
      <v-card>
        <v-card-text>
          <document-upload v-if="item.template === 'DocumentUpload'"
                           :checklistId="checklist.id"
                           :checklistItem="item">
          </document-upload>
          <comments-date-of-action v-else-if="item.template === 'CommentsAndDateOfAction'"
                                   :checklistId="checklist.id"
                                   :checklistItem="item">
          </comments-date-of-action>
          <comments-only v-else-if="item.template === 'CommentsOnly'"
                         :checklistId="checklist.id"
                         :checklistItem="item">
          </comments-only>
          <date-of-action v-else-if="item.template === 'DateOfAction'"
                          :checklistId="checklist.id"
                          :checklistItem="item">
          </date-of-action>
        </v-card-text>
      </v-card>
    </v-expansion-panel-content>
  </v-expansion-panel>
  <div v-else>
    Checklist is unavailable
  </div>
</template>

<script>
import { mapState } from 'vuex'
import DocumentUpload from './DocumentUpload'
import CommentsDateOfAction from './CommentsAndDateOfAction'
import CommentsOnly from './CommentsOnly'
import DateOfAction from './DateOfAction'

export default {
  name: 'accordion',
  components: {
    DocumentUpload,
    CommentsDateOfAction,
    CommentsOnly,
    DateOfAction
  },
  props: {
    'checklist': {
      type: Object,
      default: null
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  methods: {
    toggleCompleted (newValue, item) {
      item.isCompleted = newValue === true
      this.$emit('toggleCompleted', item)
    },
    move (direction, item) {
      this.$emit('move', { direction, item })
    }
  }
}
</script>

<style lang="scss">
  .accordion-checkbox {
    height: auto;
    width: auto;
    margin: 0;
    float: left;

    &.input-group.input-group--selection-controls .input-group__input {
      width: 2rem;
      display: inline-block;
    }

    .input-group__details {
      display: none;
    }
  }

  .display-text {
    display: inline-block;
    margin-top: 1.625rem;
  }

</style>
