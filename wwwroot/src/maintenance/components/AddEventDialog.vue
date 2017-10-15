<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog"
              :max-width="500">
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <v-card>
          <v-card-title>
            <h1 class="display-1">Tell us what has changed</h1>
          </v-card-title>
          <v-card-text>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="descriptionFieldValue"
                            :multiline="true"
                            :rules="[$validation.rules.required, $validation.rules.min_length(descriptionFieldValue, 2)]"
                            label="Describe the change in as much detail as possible"
                            ref="descriptionField"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <select-list :items="statuses"
                             :rules="[$validation.rules.required]"
                             label="Current status"
                             v-model="statusListValue"
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
    'statuses': {
      type: Array,
      required: true
    }
  },
  data () {
    return {
      descriptionFieldValue: null,
      statusListValue: null,
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
            description: this.descriptionFieldValue,
            status: this.statusListValue
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
        this.$refs.descriptionField.focus()
      } else {
        this.dialog = false
      }
    }
  }
}
</script>
