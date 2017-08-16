<template>
  <div v-if="checklist && checklist.checklistItems && checklist.checklistItems.length">
    <v-expansion-panel>
      <v-expansion-panel-content v-for="(item, index) in checklist.checklistItems" :key="index">
        <div slot="header" class="header">
          <div class="accordion col">
            <v-checkbox v-if="permissions.CI_ToggleCompleted"
                        v-model="item.isCompleted"
                        class="accordion-checkbox"
                        color="primary"
                        @click.stop="newValue => { toggleCompleted(newValue, item); }">
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
    <v-layout row justify-center>
      <v-dialog v-model="dialog"
                @open="$refs.displayText.focus()"
                :width="500">
        <v-card>
          <v-card-title>
            <span class="headline">Edit description</span>
          </v-card-title>
          <v-card-text>
            <text-field v-model="newDescription"
                        label="Update the new description for this checklist item"
                        ref="displayText"
                        @keyenter="closeEditDialog()">
            </text-field>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn class="blue--text darken-1" flat @click="closeEditDialog()">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-layout>
  </div>
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
    closeEditDialog () {
      this.selectedChecklistItem.displayText = this.newDescription
      this.dialog = false
      this.$emit('editChecklistItem', this.selectedChecklistItem)
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

<style lang="scss" scoped>
  .accordion-checkbox {
    padding: 0;
    .input-group__details {
      display: none;
    }
  }

  .header {
    display: flex;
    width: 100%;
    padding: 1.25rem 3rem 0 0;

    .accordion {
      &.col {
        .display-text {
          transform: translateY(6px);
          padding: 0 0 0 1rem;
          display: inline-block;
        }
      }
      &.grow {
        flex-grow: 1;
      }
      .btn {
        margin: 0;
      }
      .icon {
        margin-right: 0;
      }
    }
  }

</style>
