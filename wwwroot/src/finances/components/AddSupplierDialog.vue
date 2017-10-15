<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog"
              :max-width="500">
      <form v-if="dialog"
            @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <v-card>
          <v-card-title>
            <h1 class="display-1">Add supplier</h1>
          </v-card-title>
          <v-card-text>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="nameFieldValue"
                            :rules="[$validation.rules.required, $validation.rules.min_length(nameFieldValue, 2)]"
                            label="Name of the supplier"
                            ref="nameField"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="addressLine1FieldValue"
                            :rules="[$validation.rules.required, $validation.rules.min_length(addressLine1FieldValue, 2)]"
                            label="Address Line 1"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="addressLine2FieldValue"
                            label="Address Line 2"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="addressLine3FieldValue"
                            label="Address Line 3"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="townOrCityFieldValue"
                            :rules="[$validation.rules.required, $validation.rules.min_length(townOrCityFieldValue, 2)]"
                            label="Town or City"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="postCodeFieldValue"
                            :rules="[$validation.rules.required, $validation.rules.min_length(postCodeFieldValue, 2)]"
                            label="Post code"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="mainContactNumberFieldValue"
                            :rules="[$validation.rules.required, $validation.rules.min_length(mainContactNumberFieldValue, 2)]"
                            label="Main contact number"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="secondaryContactNumberFieldValue"
                            label="Secondary contact number"
                            @keyesc="close()"
                            required>
                </text-field>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12">
                <text-field v-model="emailAddressFieldValue"
                            :rules="[$validation.rules.required, $validation.rules.email]"
                            label="Email address"
                            @keyesc="close()"
                            type="email"
                            required>
                </text-field>
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
    }
  },
  data () {
    return {
      nameFieldValue: null,
      addressLine1FieldValue: null,
      addressLine2FieldValue: null,
      addressLine3FieldValue: null,
      townOrCityFieldValue: null,
      postCodeFieldValue: null,
      mainContactNumberFieldValue: null,
      secondaryContactNumberFieldValue: null,
      emailAddressFieldValue: null,
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
            name: this.nameFieldValue,
            addressLine1: this.addressLine1FieldValue,
            addressLine2: this.addressLine2FieldValue,
            addressLine3: this.addressLine3FieldValue,
            townOrCity: this.townOrCityFieldValue,
            postCode: this.postCodeFieldValue,
            mainContactNumber: this.mainContactNumberFieldValue,
            secondaryContactNumber: this.secondaryContactNumberFieldValue,
            emailAddress: this.emailAddressFieldValue
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
        this.$refs.nameField.focus()
      } else {
        this.dialog = false
      }
    }
  }
}
</script>
