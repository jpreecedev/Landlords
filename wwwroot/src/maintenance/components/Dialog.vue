<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog"
              :max-width="750">
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <v-card>
          <v-card-title>
            <h1 class="display-1">{{ title }}</h1>
            <h2 class="headline">{{ subTitle }}</h2>
          </v-card-title>
          <v-card-text>
            <div v-if="properties && properties.length"
                 class="row">
              <div class="col-xs-12">
                <select-list :items="properties"
                             :rules="[$validation.rules.required]"
                             label="Property"
                             v-model="propertyListValue"
                             required>
                </select-list>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="titleFieldValue"
                            label="Title"
                            ref="titleField"
                            @keyesc="close()"
                            :rules="[$validation.rules.required, $validation.rules.min_length(titleFieldValue, 2)]"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="descriptionFieldValue"
                            :multiline="true"
                            :rules="[$validation.rules.required, $validation.rules.min_length(descriptionFieldValue, 2)]"
                            label="Describe the problem and include as much detail as possible"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <select-list :items="severities"
                             :rules="[$validation.rules.required]"
                             label="Severity"
                             v-model="severityListValue"
                             required>
                </select-list>
              </div>
            </div>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn flat @click="close()">Cancel</v-btn>
            <v-btn type="submit"
                   class="blue--text darken-1"
                   flat>
                   Save
            </v-btn>
          </v-card-actions>
        </v-card>
      </form>

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
    'subTitle': {
      type: String,
      required: true
    },
    'severities': {
      type: Array,
      required: true
    },
    'properties': {
      type: Array,
      required: false,
      default: () => []
    }
  },
  data () {
    return {
      titleFieldValue: null,
      descriptionFieldValue: null,
      severityListValue: null,
      propertyListValue: null,
      dialog: false,
      errors: []
    }
  },
  methods: {
    close () {
      this.$emit('closed')
    },
    reset () {
      this.$validation.reset(this.$children)
    },
    validateBeforeSubmit () {
      this.$validation.validate(this.$children)
        .then(() => {
          this.errors = []
          this.$emit('closed', {
            title: this.titleFieldValue,
            description: this.descriptionFieldValue,
            severity: this.severityListValue,
            propertyDetails: this.propertyListValue
          })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
        })
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
        this.reset()
        this.dialog = true
        this.$refs.titleField.focus()
      } else {
        this.dialog = false
      }
    }
  }
}
</script>
