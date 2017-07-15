<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-text-field v-model="checklistItem.payload.comments"
                      :multi-line="true"
                      :rows="1"
                      :auto-grow="true"
                      @blur="save()"
                      label="Comments">
        </v-text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :nudge-left="100">
          <v-text-field slot="activator"
                        label="Actioned"
                        v-model="checklistItem.payload.actioned"
                        prepend-icon="date_range"
                        required readonly>
          </v-text-field>
          <v-date-picker v-model="checklistItem.payload.actioned"
                         @input="save()"
                         scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12">
        <file-upload v-model="file"
                     @formData="fileChosen"
                     label="Browse for a file on your computer">
        </file-upload>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'document-upload',
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
      file: null,
      permissions: this.$store.state.permissions
    }
  },
  created () {
    if (!this.checklistItem.payload) {
      this.checklistItem.payload = {
        comments: null,
        actioned: null
      }
    } else {
      this.checklistItem.payload = JSON.parse(this.checklistItem.payload)
    }
  },
  methods: {
    fileChosen () {
      // TODO
      debugger
    },
    save () {
      if (this.permissions.CI_ApplyTemplate) {
        this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, this.checklistItem.payload)
      }
    }
  }
}
</script>
