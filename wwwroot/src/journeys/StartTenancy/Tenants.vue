<template>
  <div>
    <h2 class="title">Who are the tenants?</h2>

    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="tenant">
        <div slot="header">
          <tenant-type :tenant="tenant"></tenant-type>
        </div>
        <div class="subheading">What is the {{ tenant.isAdult ? 'tenants' : 'childs' }} name?</div>
        <div class="row">
          <div class="col-xs-12 col-md-3">
            <v-select :items="titles"
                      :rules="[$validation.rules.required, $validation.rules.min_length(tenant.title, 2)]"
                      v-model="tenant.title"
                      @input="updateField(index, tenant, 'title')"
                      label="Select a title"
                      dark single-line auto>
            </v-select>
            <!--<span v-if="errorBag.has('title')" class="md-error">Select a valid title</span>-->
          </div>
          <div class="col-xs-12 col-md-3">
            <v-text-field v-model="tenant.firstName"
                          :rules="[$validation.rules.required, $validation.rules.min_length(tenant.firstName, 2)]"
                          :value="tenant.firstName"
                          @input="updateField(index, tenant, 'firstName')"
                          label="First name"
                          required>
            </v-text-field>
            <!--<span v-if="errorBag.has('firstName')" class="md-error">Enter a valid first name</span>-->
          </div>
          <div class="col-xs-12 col-md-3">
            <v-text-field v-model="tenant.middleName"
                          :value="tenant.middleName"
                          @input="updateField(index, tenant, 'middleName')"
                          label="Middle name(s)">
            </v-text-field>
          </div>
          <div class="col-xs-12 col-md-3">
            <v-text-field v-model="tenant.lastName"
                          :value="tenant.lastName"
                          :rules="[$validation.rules.required, $validation.rules.min_length(tenant.lastName, 2)]"
                          @input="updateField(index, tenant, 'lastName')"
                          label="Last name"
                          required>
            </v-text-field>
            <!--<span v-if="errorBag.has('lastName')" class="md-error">Enter a valid last name</span>-->
          </div>
        </div>
        <div class="row">
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
          <div class="subheading">Now, tell us the tenants address history - we need 3 years total.</div>
          <v-expansion-panel class="mt-3 mb-3 white">
            <v-expansion-panel-content v-for="(address, addressIndex) in tenant.addresses" :key="address">
              <div slot="header">
                Address {{ addressIndex + 1 }}
              </div>
              <div class="row">
                <div class="col-xs-6">
                  <v-text-field v-model="address.street"
                                :value="address.street"
                                :multi-line="true"
                                :rows="1"
                                :auto-grow="true"
                                :rules="[$validation.rules.required, $validation.rules.min_length(address.street, 2)]"
                                @input="updateAddress(index, addressIndex, address, 'street')"
                                label="Street address"
                                required>
                  </v-text-field>
                  <!--<span v-if="errorBag.has('street')" class="md-error">Enter a valid street address</span>-->
                </div>
                <div class="col-xs-6">
                  <v-text-field v-model="address.townOrCity"
                                :value="address.townOrCity"
                                :rules="[$validation.rules.required, $validation.rules.min_length(address.townOrCity, 2)]"
                                @input="updateAddress(index, addressIndex, address, 'townOrCity')"
                                label="Town or city"
                                required>
                  </v-text-field>
                  <!--<span v-if="errorBag.has('townOrCity')" class="md-error">Enter a valid town or city</span>-->
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12 col-md-4">
                  <v-select :items="counties"
                            v-model="address.countyOrRegion"
                            @input="updateAddress(index, addressIndex, address, 'countyOrRegion')"
                            label="Select a county or region"
                            dark single-line auto>
                  </v-select>
                  <!--<span v-if="errorBag.has('countyOrRegion')" class="md-error">Enter a valid county or region</span>-->
                </div>
                <div class="col-xs-12 col-md-4">
                  <v-text-field v-model="address.postcode"
                                :value="address.postcode"
                                :rules="[$validation.rules.required, $validation.rules.min_length(address.postcode, 5), $validation.rules.max_length(address.postcode, 7)]"
                                @input="updateAddress(index, addressIndex, address, 'postcode')"
                                label="Postcode"
                                required>
                  </v-text-field>
                  <!--<span v-if="errorBag.has('postcode')" class="md-error">Enter a valid postal code</span>-->
                </div>
                <div class="col-xs-12 col-md-4">
                  <v-select :items="countries"
                            :rules="[$validation.rules.required]"
                            v-model="address.country"
                            @input="updateAddress(index, addressIndex, address, 'country')"
                            label="Select a country"
                            dark single-line auto required>
                  </v-select>
                </div>
              </div>
              <div class="subheading">How long has the tenant lived at this address?</div>
              <div class="row">
                <div class="col-xs-6 col-md-2">
                  <v-text-field v-model="address.yearsAtAddress"
                                :value="address.yearsAtAddress"
                                :rules="[$validation.rules.required, $validation.rules.min_value(address.yearsAtAddress, 1), $validation.rules.max_value(address.yearsAtAddress, 100)]"
                                @input="updateAddress(index, addressIndex, address, 'yearsAtAddress')"
                                label="Years"
                                type="number"
                                min="0"
                                max="100"
                                step="1"
                                required>
                  </v-text-field>
                </div>
                <div class="col-xs-6 col-md-2">
                  <v-text-field v-model="address.monthsAtAddress"
                                :value="address.monthsAtAddress"
                                :rules="[$validation.rules.required, $validation.rules.min_value(address.monthsAtAddress, 0), $validation.rules.max_value(address.monthsAtAddress, 12)]"
                                @input="updateAddress(index, addressIndex, address, 'monthsAtAddress')"
                                label="Months"
                                type="number"
                                min="0"
                                max="12"
                                step="1"
                                required>
                  </v-text-field>
                </div>
              </div>
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-btn dark @click.native="addTenantAddress(index)">Add another address</v-btn>
        </template>
        <template v-if="tenant.isAdult">
          <div class="subheading mt-4">And finally, their contact details</div>
          <div class="row">
            <div class="col-xs-12 col-md-4">
              <v-text-field v-model="tenant.mainContactNumber"
                            :value="tenant.mainContactNumber"
                            :rules="[$validation.rules.required, $validation.rules.min_length(tenant.mainContactNumber, 2)]"
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
                            :rules="[$validation.rules.required, $validation.rules.email]"
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
      titles: {
        type: Array,
        default: () => []
      },
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
    computed: {
      ...mapState({
        tenants: state => state.newTenancy.tenants
      })
    },
    methods: {
      hasEnoughAddressInformation (tenantIndex) {
        let years = 0
        let months = 0
        this.tenants[tenantIndex].addresses.forEach(address => {
          years += Number.parseInt(address.yearsAtAddress || 0)
          months += Number.parseInt(address.monthsAtAddress || 0)
        })
        return years + (months / 12) >= 3
      },
      addTenantAddress (tenantIndex) {
        this.$store.commit('TENANT_ADD_ADDRESS', tenantIndex)
      },
      addOccupier (isAdult) {
        this.$store.commit('TENANT_ADD_TENANT', isAdult)
      },
      deleteOccupier (index) {
        this.$store.commit('TENANT_REMOVE_TENANT', index)
      },
      updateField (index, tenant, field) {
        this.$store.commit('TENANT_UPDATE_FIELD', Object.assign(tenant, {index, field: field}))
      },
      updateAddress (tenantIndex, addressIndex, address, field) {
        this.$store.commit('TENANT_UPDATE_ADDRESS', {
          tenantIndex,
          addressIndex,
          address,
          field
        })
      }
    }
  }

</script>
