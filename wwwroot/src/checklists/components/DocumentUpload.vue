<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <text-field v-model="checklistItem.payload.comments"
                    :multiline="true"
                    :rows="1"
                    @blur="save()"
                    label="Comments">
        </text-field>
      </div>
      <div class="col-xs-12 col-md-6">
        <date-picker v-model="checklistItem.payload.actioned"
                     @input="save()"
                     label="Actioned">
        </date-picker>
      </div>
    </div>
    <div class="row" v-if="permissions.CI_UploadDocument">
      <div class="col-xs-12">
        <file-upload v-model="checklistItem.payload.file.fileName"
                     @formData="fileChosen"
                     label="Browse for a file on your computer">
        </file-upload>
        <v-btn v-if="checklistItem.payload.file.relativeFilePath"
               @click="openFile(checklistItem.payload.file.relativeFilePath)"
               class="blue--text darken-2"
               outline>
          Open
        </v-btn>
      </div>
    </div>
    <div class="row" v-if="progress">
      <div class="col-xs-12">
        <v-progress-linear v-model="progress"></v-progress-linear>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import FileUploadService from 'services/file-upload.service'

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
      progress: 0,
      file: null
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  created () {
    if (!this.checklistItem.payload) {
      this.checklistItem.payload = {
        comments: null,
        actioned: null,
        file: {
          fileName: null,
          filePath: null,
          relativeFilePath: null
        }
      }
    } else {
      this.checklistItem.payload = JSON.parse(this.checklistItem.payload)
    }
  },
  methods: {
    fileChosen (formData) {
      FileUploadService.upload(formData[0], `/api/checklistitem/upload?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, uploadProgress => { this.progress = Number.parseInt(uploadProgress) })
        .then(resources => {
          if (resources) {
            this.checklistItem.payload.file = resources[0]
            this.save()
          }
        })
        .finally(() => {
          this.progress = 0
        })
    },
    save () {
      if (this.permissions.CI_ApplyTemplate) {
        this.$http.post(`/api/checklistitem/template?checklistId=${this.checklistId}&checklistItemId=${this.checklistItem.id}`, this.checklistItem.payload)
      }
    },
    openFile (filePath) {
      window.open(filePath)
    }
  }
}
</script>
