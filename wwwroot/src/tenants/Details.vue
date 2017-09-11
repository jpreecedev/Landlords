<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Tenant details</h1>
        <p class="display-2 grey--text text--darken-1">Your tenants name, address, and contact information</p>
      </header>
      <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
        <fieldset :disabled="!permissions.TE_Update">
          <div class="row">
            <div class="col-xs-12 col-md-6">
              <v-card>
                <v-card-title class="title primary">
                  Personal Details
                </v-card-title>
                <v-card-text>
                  <text-field v-model="tenant.firstName"
                              :rules="[$validation.rules.required, $validation.rules.min_length(tenant.firstName, 2)]"
                              label="First name"
                              required>
                  </text-field>
                  <text-field v-model="tenant.middle"
                              label="Middle name(s)">
                  </text-field>
                  <text-field v-model="tenant.lastName"
                              :rules="[$validation.rules.required, $validation.rules.min_length(tenant.lastName, 2)]"
                              label="Last name"
                              required>
                  </text-field>
                  <date-picker v-model="tenant.dateOfBirth"
                              label="Date of birth"
                              :rules="[$validation.rules.required]">
                  </date-picker>
                </v-card-text>
                <v-card-title class="title">
                  Contact Details
                </v-card-title>
                <v-card-text>
                    <text-field v-model="tenant.mainContactNumber"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.mainContactNumber, 2)]"
                                label="Main contact number"
                                required>
                    </text-field>
                    <text-field v-model="tenant.secondaryContactNumber"
                                label="Another contact number">
                    </text-field>
                    <text-field v-model="tenant.emailAddress"
                                label="Email Address"
                                type="email"
                                required>
                    </text-field>
                </v-card-text>
              </v-card>
            </div>
            <div class="col-xs-12 col-md-6" v-if="tenant.addresses">
              <v-card>
                <v-card-title class="title primary">
                  Addresses
                </v-card-title>
                <v-card-text>
                    <text-field v-model="tenant.addresses[addressIndex].street"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[addressIndex].street, 2)]"
                                :multiline="true"
                                label="Street address">
                    </text-field>
                    <text-field v-model="tenant.addresses[addressIndex].townOrCity"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[addressIndex].townOrCity, 2)]"
                                label="Town or city">
                    </text-field>
                    <text-field v-model="tenant.addresses[addressIndex].countyOrRegion"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[addressIndex].countyOrRegion, 2)]"
                                label="County or region">
                    </text-field>
                    <text-field v-model="tenant.addresses[addressIndex].postcode"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[addressIndex].postcode, 5), $validation.rules.max_length(tenant.addresses[addressIndex].postcode, 7)]"
                                label="Postcode">
                    </text-field>
                    <p>
                      Country: <br/>
                      {{ tenant.addresses[addressIndex].country }}
                    </p>
                    <div class="subheading mt-5">Time lived at property</div>
                    <div class="row">
                      <div class="col-xs-6 col-md-4">
                        <text-field v-model="tenant.addresses[addressIndex].yearsAtAddress"
                                    :rules="[$validation.rules.required, $validation.rules.min_value(tenant.addresses[addressIndex].yearsAtAddress, 1), $validation.rules.max_value(tenant.addresses[addressIndex].yearsAtAddress, 100)]"
                                    label="Years"
                                    type="number"
                                    min="0"
                                    max="100"
                                    step="1"
                                    required>
                        </text-field>
                      </div>
                      <div class="col-xs-6 col-md-4">
                        <text-field v-model="tenant.addresses[addressIndex].monthsAtAddress"
                                    :rules="[$validation.rules.required, $validation.rules.min_value(tenant.addresses[addressIndex].monthsAtAddress, 0), $validation.rules.max_value(tenant.addresses[addressIndex].monthsAtAddress, 11)]"
                                    label="Months"
                                    type="number"
                                    min="0"
                                    max="11"
                                    step="1"
                                    required>
                        </text-field>
                      </div>
                    </div>
                    <div class="row" v-if="tenant.addresses && tenant.addresses.length > 0">
                      <div class="col-xs-12">
                        <v-btn class="no-left-margin" :disabled="addressIndex === 0" @click="addressIndex -= 1">Previous</v-btn>
                        <v-btn :disabled="addressIndex === tenant.addresses.length - 1" @click="addressIndex += 1">Next</v-btn>
                      </div>
                    </div>
                </v-card-text>
              </v-card>
            </div>
          </div>
          <div class="row mt-3">
            <div class="col-xs-12">
              <v-btn primary v-if="permissions.TE_Update" type="submit" :loading="isSaving">Save</v-btn>
              <v-btn flat v-if="permissions.TE_Update" @click="reset()">Reset</v-btn>
            </div>
          </div>
        </fieldset>
      </form>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'tenantsDetails',
  data () {
    return {
      isSaving: false,
      isLoading: false,
      addressIndex: 0,
      tenant: {
        id: null,
        firstName: null,
        middleName: null,
        lastName: null,
        dateOfBirth: '1970-01-01',
        mainContactNumber: null,
        secondaryContactNumber: null,
        emailAddress: null,
        addresses: [{
          street: null,
          townOrCity: null,
          countyOrRegion: null,
          postcode: null,
          country: null
        }]
      }
    }
  },
  mounted () {
    this.isLoading = true
    this.$http.get(`/api/tenants/${this.$route.params.tenantId}`)
      .then(response => {
        Object.assign(this, utils.mapEntity(response.data, 'tenant', false))
      })
      .finally(() => {
        this.isLoading = false
        this.$validation.commit(this.$children)
      })
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  methods: {
    validateBeforeSubmit () {
      this.$validation.validate(this.$children)
        .then(() => {
          this.isSaving = true
          this.$http.post(`/api/tenants/`, { ...this.tenant })
            .then(() => {
              this.$router.push({ name: 'tenants-overview' })
            })
            .finally(() => {
              this.isSaving = false
            })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_VALIDATION_NOTIFICATION')
        })
    },
    reset () {
      this.$validation.reset(this.$children)
    }
  }
}
</script>
