<template>
  <div v-if="checklist && checklist.checklistItems && checklist.checklistItems.length">
    <v-expansion-panel>
      <v-expansion-panel-content v-for="(item, index) in checklist.checklistItems" :key="index">
        <div slot="header" class="accordion-header">
          <div class="accordion col">
            <v-checkbox v-if="permissions.CI_ToggleCompleted"
                        v-model="item.isCompleted"
                        class="accordion-checkbox"
                        color="primary"
                        @click.stop="newValue => { toggleCompleted(newValue, item) }">
            </v-checkbox>
          </div>
          <div class="accordion col">
            <span v-if="!item.isCompleted" class="display-text">{{ item.displayText }}</span>
            <del v-else class="display-text text-muted">{{ item.displayText }}</del>
          </div>
          <div class="accordion grow"></div>
          <div class="accordion col">
            <v-btn @click.stop="editChecklistItem(item)"
                   v-if="permissions.CI_Update"
                   icon>
              <v-icon>edit</v-icon>
            </v-btn>
            <v-btn @click="deleteChecklistItem(item)"
                   v-if="permissions.CI_Delete"
                   icon>
              <v-icon>delete_forever</v-icon>
            </v-btn>
          </div>
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
    <checklist-dialog title="Edit checklist item description"
                      textLabel="Update the description for this checklist item"
                      v-on:closed="closeEditDialog"
                      :isOpen="dialog"
                      :textValue="newDescription">
    </checklist-dialog>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'accordion',
  data () {
    return {
      dialog: false,
      selectedChecklistItem: null,
      newDescription: null
    }
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
    toggleCompleted (newValue, checklistItem) {
      checklistItem.isCompleted = !checklistItem.isCompleted === true
      this.$emit('toggleCompleted', checklistItem)
    },
    closeEditDialog (newDescription) {
      if (newDescription) {
        this.selectedChecklistItem.displayText = newDescription
        this.$emit('editChecklistItem', this.selectedChecklistItem)
      }
      this.dialog = false
    },
    editChecklistItem (checklistItem) {
      this.selectedChecklistItem = checklistItem
      this.newDescription = checklistItem.displayText
      this.dialog = true
    },
    deleteChecklistItem (checklistItem) {
      if (confirm('Are you sure you want to delete this checklist item?')) {
        this.$emit('deleteChecklistItem', checklistItem)
      }
    }
  }
}
</script>
