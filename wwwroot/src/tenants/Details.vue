<template>
  <div>
    <h1 class="display-2">Tenant details</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.TE_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-title class="title primary">
                Personal Details
              </v-card-title>
              <v-card-text>
                <v-text-field v-model="tenant.firstName"
                              :rules="[$validation.rules.required, $validation.rules.min_length(tenant.firstName, 2)]"
                              label="First name"
                              required>
                </v-text-field>
                <v-text-field v-model="tenant.middle"
                              label="Middle name(s)">
                </v-text-field>
                <v-text-field v-model="tenant.lastName"
                              :rules="[$validation.rules.required, $validation.rules.min_length(tenant.lastName, 2)]"
                              label="Last name"
                              required>
                </v-text-field>
                <v-menu lazy :nudge-left="100">
                  <v-text-field slot="activator"
                                label="Date of birth"
                                v-model="tenant.dateOfBirth"
                                prepend-icon="date_range"
                                required readonly>
                  </v-text-field>
                  <v-date-picker v-model="tenant.dateOfBirth"
                                scrollable>
                  </v-date-picker>
                </v-menu>
              </v-card-text>
              <v-card-title class="title">
                Contact Details
              </v-card-title>
              <v-card-text>
                  <v-text-field v-model="tenant.mainContactNumber"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.mainContactNumber, 2)]"
                                label="Main contact number"
                                required>
                  </v-text-field>
                  <v-text-field v-model="tenant.secondaryContactNumber"
                                label="Another contact number">
                  </v-text-field>
                  <v-text-field v-model="tenant.emailAddress"
                                label="Email Address"
                                type="email"
                                required>
                  </v-text-field>
              </v-card-text>
            </v-card>
          </div>
          <div class="col-xs-12 col-md-6" v-if="tenant.addresses && tenant.addresses.length === 1">
            <v-card>
              <v-card-title class="title primary">
                Addresses
              </v-card-title>
              <v-card-text>
                  <v-text-field v-model="tenant.addresses[0].street"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[0].street, 2)]"
                                label="Street address"
                                required>
                  </v-text-field>
                  <v-text-field v-model="tenant.addresses[0].townOrCity"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[0].townOrCity, 2)]"
                                label="Town or city"
                                required>
                  </v-text-field>
                  <v-text-field v-model="tenant.addresses[0].countyOrRegion"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[0].countyOrRegion, 2)]"
                                label="County or region"
                                required>
                  </v-text-field>
                  <v-text-field v-model="tenant.addresses[0].postcode"
                                :rules="[$validation.rules.required, $validation.rules.min_length(tenant.addresses[0].postcode, 5), $validation.rules.max_length(tenant.addresses[0].postcode, 7)]"
                                label="Postcode"
                                required>
                  </v-text-field>
                  <p>
                    Country
                    <br>
                    {{ tenant.addresses[0].country }}
                  </p>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary v-if="permissions.TE_Update" type="submit">Save</v-btn>
            <v-btn flat v-if="permissions.TE_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </div>
</template>

<script>
import utils from 'utils'

export default {
  name: 'tenantsDetails',
  data () {
    return {
      permissions: this.$store.state.permissions,
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
  created () {
    this.$http.get(`/api/tenants/${this.$route.params.tenantId}`).then(response => {
      Object.assign(this, utils.mapEntity(response.data, 'tenant', false))
    })
  },
  methods: {
    validateBeforeSubmit: function () {
      this.$http.post(`/api/tenants/`, { ...this.tenant })
        .then(() => {
          this.$router.push({ name: 'tenants-overview' })
        })
    }
  }
}
</script>
