<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog"
              :max-width="500">
      <v-card>
        <v-card-title>
          <span class="headline">{{ title }}</span>
        </v-card-title>
        <v-card-text>
          <div v-if="showTextField"
               class="row">
            <div class="col-xs-12">
              <text-field v-model="textFieldValue"
                          :label="textLabel"
                          ref="textField"
                          @keyenter="!showList && closeAndSave()"
                          @keyesc="close()">
              </text-field>
            </div>
          </div>
          <div v-if="showList"
               class="row">
            <div class="col-xs-12">
              <select-list :items="listItems"
                           :label="listLabel"
                           v-model="selectListValue">
              </select-list>
            </div>
          </div>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat @click="close()">Cancel</v-btn>
          <v-btn class="blue--text darken-1" flat @click="closeAndSave()">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
export default {
  name: 'name',
  props: {
    'isOpen': {
      type: Boolean,
      default: false
    },
    'title': {
      type: String,
      required: true
    },
    'textValue': {
      type: String,
      default: null
    },
    'textLabel': {
      type: String,
      required: true
    },
    'showTextField': {
      type: Boolean,
      required: false,
      default: true
    },
    'showList': {
      type: Boolean,
      required: false,
      default: false
    },
    'listItems': {
      type: Array,
      required: false,
      default: () => []
    },
    'listLabel': {
      type: String,
      required: false,
      default: null
    }
  },
  data () {
    return {
      textFieldValue: null,
      selectListValue: null,
      dialog: false
    }
  },
  methods: {
    close () {
      this.$emit('closed')
    },
    closeAndSave () {
      this.$emit('closed', this.textFieldValue, this.selectListValue)
    }
  },
  watch: {
    dialog () {
      if (!this.dialog) {
        this.$emit('closed')
      }
    },
    isOpen () {
      if (this.isOpen) {
        this.textFieldValue = this.textValue
        this.selectListValue = this.listValue
        this.dialog = true
        this.$refs.textField.focus()
      } else {
        this.dialog = false
      }
    }
  }
}
</script>
