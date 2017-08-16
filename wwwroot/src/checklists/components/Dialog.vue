<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog"
              :width="500">
      <v-card>
        <v-card-title>
          <span class="headline">{{ title }}</span>
        </v-card-title>
        <v-card-text>
          <text-field v-model="textValue"
                      :label="label"
                      ref="input"
                      @keyenter="closeAndSave()"
                      @keyesc="close()">
          </text-field>
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
    'label': {
      type: String,
      required: true
    },
    'title': {
      type: String,
      required: true
    },
    'value': {
      type: String,
      default: null
    }
  },
  data () {
    return {
      textValue: null,
      dialog: false
    }
  },
  methods: {
    close () {
      this.$emit('closed')
    },
    closeAndSave () {
      this.$emit('closed', this.textValue)
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
        this.textValue = this.value
        this.dialog = true
        this.$refs.input.focus()
      } else {
        this.dialog = false
      }
    }
  }
}
</script>
