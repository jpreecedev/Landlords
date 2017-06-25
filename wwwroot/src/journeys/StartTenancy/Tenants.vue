<template>
  <div>
    <h2 class="title">Who are the tenants?</h2>

    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="tenant">
        <div slot="header">
          <tenant-type :tenant="tenant"></tenant-type>
        </div>
        <v-card>
          <div class="subheading">What is the {{ tenant.isAdult ? 'tenants' : 'childs' }} name?</div>
          <div class="row">
            <div class="col-xs-12 col-md-4">
              <v-text-field v-model="tenant.firstName"
                            :value="tenant.firstName"
                            @input="updateField(index, tenant, 'firstName')"
                            label="First name"
                            required>
              </v-text-field>
              <!--<span v-if="errorBag.has('firstName')" class="md-error">Enter a valid first name</span>-->
            </div>
            <div class="col-xs-12 col-md-4">
              <v-text-field v-model="tenant.middleName"
                            :value="tenant.middleName"
                            @input="updateField(index, tenant, 'middleName')"
                            label="Middle name(s)">
              </v-text-field>
            </div>
            <div class="col-xs-12 col-md-4">
              <v-text-field v-model="tenant.lastName"
                            :value="tenant.lastName"
                            @input="updateField(index, tenant, 'lastName')"
                            label="Last name"
                            required>
              </v-text-field>
              <!--<span v-if="errorBag.has('lastName')" class="md-error">Enter a valid last name</span>-->
            </div>
            <div class="col-xs-12 col-md-4">
              <v-menu lazy :nudge-left="100">
                <v-text-field slot="activator"
                              label="Date of birth"
                              v-model="tenant.dateOfBirth"
                              prepend-icon="date_range"
                              required readonly>
                </v-text-field>
                <v-date-picker v-model="tenant.dateOfBirth"
                               :value="tenant.dateOfBirth"
                               @input="updateField(index, tenant, 'dateOfBirth')"
                               scrollable>
                </v-date-picker>
              </v-menu>
            </div>
          </div>
          <template v-if="tenant.isAdult">
            <div class="subheading mt-3">Now, tell us the tenants current address</div>
            <div class="row">
              <div class="col-xs-6">
                <v-text-field v-model="tenant.address.street"
                              :value="tenant.address.street"
                              :multi-line="true"
                              :rows="1"
                              :auto-grow="true"
                              @input="updateAddress(index, tenant, 'street')"
                              label="Street address"
                              required>
                </v-text-field>
                <!--<span v-if="errorBag.has('street')" class="md-error">Enter a valid street address</span>-->
              </div>
              <div class="col-xs-6">
                <v-text-field v-model="tenant.address.townOrCity"
                              :value="tenant.address.townOrCity"
                              @input="updateAddress(index, tenant, 'townOrCity')"
                              label="Town or city"
                              required>
                </v-text-field>
                <!--<span v-if="errorBag.has('townOrCity')" class="md-error">Enter a valid town or city</span>-->
              </div>
            </div>
            <div class="row">
              <div class="col-xs-12 col-md-4">
                <v-select :items="counties"
                          v-model="tenant.address.countyOrRegion"
                          @input="updateAddress(index, tenant, 'countyOrRegion')"
                          label="Select a county or region"
                          dark single-line auto>
                </v-select>
                <!--<span v-if="errorBag.has('countyOrRegion')" class="md-error">Enter a valid county or region</span>-->
              </div>
              <div class="col-xs-12 col-md-4">
                <v-text-field v-model="tenant.address.postcode"
                              :value="tenant.address.postcode"
                              @input="updateAddress(index, tenant, 'postcode')"
                              label="Postcode"
                              required>
                </v-text-field>
                <!--<span v-if="errorBag.has('postcode')" class="md-error">Enter a valid postal code</span>-->
              </div>
              <div class="col-xs-12 col-md-4">
                <v-select :items="countries"
                          v-model="tenant.address.country"
                          @input="updateAddress(index, tenant, 'country')"
                          label="Select a country"
                          dark single-line auto required>
                </v-select>
              </div>
            </div>
          </template>
          <template v-if="tenant.isAdult">
            <div class="subheading mt-3">And finally, their contact details</div>
            <div class="row">
              <div class="col-xs-12 col-md-4">
                <v-text-field v-model="tenant.mainContactNumber"
                              :value="tenant.mainContactNumber"
                              @input="updateField(index, tenant, 'mainContactNumber')"
                              label="Main contact number"
                              required>
                </v-text-field>
                <!--<span v-if="errorBag.has('mainContactNumber')" class="md-error">Enter a valid contact number</span>-->
              </div>
              <div class="col-xs-12 col-md-4">
                <v-text-field v-model="tenant.secondaryContactNumber"
                              :value="tenant.secondaryContactNumber"
                              @input="updateField(index, tenant, 'secondaryContactNumber')"
                              label="Another contact number">
                </v-text-field>
                <!--<span v-if="errorBag.has('secondaryContactNumber')" class="md-error">Enter a valid contact number</span>-->
              </div>
              <div class="col-xs-12 col-md-4">
                <v-text-field v-model="tenant.emailAddress"
                              :value="tenant.emailAddress"
                              @input="updateField(index, tenant, 'emailAddress')"
                              label="Email Address"
                              type="email"
                              required>
                </v-text-field>
                <!--<span v-if="errorBag.has('emailAddress')" class="md-error">Enter a valid email address</span>-->
              </div>
            </div>
          </template>
          <div class="row" v-if="index !== 0">
            <div class="col-xs-12">
              <v-btn primary error light @click.native="deleteOccupier(index)">
                Delete
                &nbsp;<tenant-type :tenant="tenant"></tenant-type>
              </v-btn>
            </div>
          </div>
        </v-card>
      </v-expansion-panel-content>
    </v-expansion-panel>

    <div class="expansion-panel__buttons">
      <v-btn primary light @click.native="addOccupier(true)">Add adult tenant</v-btn>
      <v-btn dark @click.native="addOccupier(false)">Add child</v-btn>
    </div>

  </div>
</template>

<script>
  import { mapState } from 'vuex'

  export default {
    name: 'tenants',
    props: {
      counties: {
        type: Array,
        default: []
      },
      countries: {
        type: Array,
        default: []
      }
    },
    data () {
      return {
        permissions: this.$store.state.permissions
      }
    },
    computed: mapState({
      tenants: state => state.newTenancy.tenants
    }),
    methods: {
      addOccupier (isAdult) {
        this.$store.commit('TENANT_ADD_TENANT', { isLeadTenant: false, dateOfBirth: '2005-01-01', isAdult: isAdult, address: {} })
      },
      deleteOccupier (index) {
        this.$store.commit('TENANT_REMOVE_TENANT', index)
      },
      updateField (index, tenant, field) {
        this.$store.commit('TENANT_UPDATE_FIELD', Object.assign(tenant, {index, field: field}))
      },
      updateAddress (index, tenant) {
        this.$store.commit('TENANT_UPDATE_ADDRESS', tenant)
      }
    }
  }

</script>
