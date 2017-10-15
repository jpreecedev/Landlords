<template>
  <div>
    <h2 class="title">Who are the tenants?</h2>

    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="index">
        <div slot="header" class="accordion-header">
          <div class="accordion col">
            <span v-if="!tenant.isDeleted" class="display-text default"><tenant-type :tenant="tenant"></tenant-type></span>
            <del v-else class="display-text default text-muted"><tenant-type :tenant="tenant"></tenant-type></del>
          </div>
          <div class="accordion grow"></div>
          <div class="accordion col">
            <v-btn @click.stop="deleteOccupier(index)"
                   v-if="!tenant.isLeadTenant && !tenant.isDeleted"
                   icon>
              <v-icon>delete_forever</v-icon>
            </v-btn>
          </div>
        </div>
        <div class="subheading">What is the {{ tenant.isAdult ? 'tenants' : 'childs' }} name?</div>
        <div class="row">
          <div class="col-xs-12 col-md-3">
            <select-list :items="titles"
                         :rules="[$validation.rules.required, $validation.rules.min_length(tenant.title, 2)]"
                         v-model="tenant.title"
                         @input="updateField(index, tenant, 'title')"
                         label="Select a title">
            </select-list>
          </div>
          <div class="col-xs-12 col-md-3">
            <text-field v-model="tenant.firstName"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.firstName, 2)]"
                        :value="tenant.firstName"
                        @input="updateField(index, tenant, 'firstName')"
                        label="First name"
                        required>
            </text-field>
          </div>
          <div class="col-xs-12 col-md-3">
            <text-field v-model="tenant.middleName"
                        :value="tenant.middleName"
                        @input="updateField(index, tenant, 'middleName')"
                        label="Middle name(s)">
            </text-field>
          </div>
          <div class="col-xs-12 col-md-3">
            <text-field v-model="tenant.lastName"
                        :value="tenant.lastName"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.lastName, 2)]"
                        @input="updateField(index, tenant, 'lastName')"
                        label="Last name"
                        required>
            </text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <date-picker v-model="tenant.dateOfBirth"
                         label="Date of birth">
            </date-picker>
          </div>
        </div>
        <template v-if="tenant.isAdult">
          <div class="subheading">Now, tell us the tenants address history - we need 3 years total.</div>
          <v-expansion-panel class="mt-3 mb-3 white">
            <v-expansion-panel-content v-for="(address, addressIndex) in tenant.addresses" :key="addressIndex">
              <div slot="header" class="accordion-header">
                <div class="accordion col">
                  <span v-if="!address.isDeleted" class="display-text default">Address {{ addressIndex + 1 }}</span>
                  <del v-else class="display-text default text-muted">Address {{ addressIndex + 1 }}</del>
                </div>
                <div class="accordion grow"></div>
                <div class="accordion col">
                  <v-btn @click.stop="deleteTenantAddress(index, addressIndex)"
                         v-if="addressIndex > 0 && !address.isDeleted"
                         icon>
                    <v-icon>delete_forever</v-icon>
                  </v-btn>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-6">
                  <text-field v-model="address.street"
                              :value="address.street"
                              :multiline="true"
                              :rows="1"
                              :rules="[$validation.rules.required, $validation.rules.min_length(address.street, 2)]"
                              @input="updateAddress(index, addressIndex, address, 'street')"
                              label="Street address"
                              required>
                  </text-field>
                </div>
                <div class="col-xs-6">
                  <text-field v-model="address.townOrCity"
                              :value="address.townOrCity"
                              :rules="[$validation.rules.required, $validation.rules.min_length(address.townOrCity, 2)]"
                              @input="updateAddress(index, addressIndex, address, 'townOrCity')"
                              label="Town or city"
                              required>
                  </text-field>
                </div>
              </div>
              <div class="row">
                <div class="col-xs-12 col-md-4">
                  <select-list :items="counties"
                               :rules="[$validation.rules.required]"
                               v-model="address.countyOrRegion"
                               @input="updateAddress(index, addressIndex, address, 'countyOrRegion')"
                               label="Select a county or region">
                  </select-list>
                </div>
                <div class="col-xs-12 col-md-4">
                  <text-field v-model="address.postcode"
                              :value="address.postcode"
                              :rules="[$validation.rules.required, $validation.rules.min_length(address.postcode, 5), $validation.rules.max_length(address.postcode, 7)]"
                              @input="updateAddress(index, addressIndex, address, 'postcode')"
                              label="Postcode"
                              required>
                  </text-field>
                </div>
                <div class="col-xs-12 col-md-4">
                  <select-list :items="countries"
                               :rules="[$validation.rules.required]"
                               v-model="address.country"
                               @input="updateAddress(index, addressIndex, address, 'country')"
                               label="Select a country"
                               required>
                  </select-list>
                </div>
              </div>
              <div class="subheading">How long has the tenant lived at this address?</div>
              <div class="row">
                <div class="col-xs-6 col-md-2">
                  <text-field v-model="address.yearsAtAddress"
                              :value="address.yearsAtAddress"
                              :rules="[$validation.rules.required, $validation.rules.min_value(address.yearsAtAddress, 0), $validation.rules.max_value(address.yearsAtAddress, 100)]"
                              @input="updateAddress(index, addressIndex, address, 'yearsAtAddress')"
                              label="Years"
                              type="number"
                              min="0"
                              max="100"
                              step="1"
                              required>
                  </text-field>
                </div>
                <div class="col-xs-6 col-md-2">
                  <text-field v-model="address.monthsAtAddress"
                              :value="address.monthsAtAddress"
                              :rules="[$validation.rules.required, $validation.rules.min_value(address.monthsAtAddress, 0), $validation.rules.max_value(address.monthsAtAddress, 11)]"
                              @input="updateAddress(index, addressIndex, address, 'monthsAtAddress')"
                              label="Months"
                              type="number"
                              min="0"
                              max="11"
                              step="1"
                              required>
                  </text-field>
                </div>
              </div>
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-btn @click="addTenantAddress(index)">Add another address</v-btn>
        </template>
        <template v-if="tenant.isAdult">
          <div class="subheading mt-4">And finally, their contact details</div>
          <div class="row">
            <div class="col-xs-12 col-md-4">
              <text-field v-model="tenant.mainContactNumber"
                          :value="tenant.mainContactNumber"
                          :rules="[$validation.rules.required, $validation.rules.min_length(tenant.mainContactNumber, 2)]"
                          @input="updateField(index, tenant, 'mainContactNumber')"
                          label="Main contact number"
                          required>
              </text-field>
            </div>
            <div class="col-xs-12 col-md-4">
              <text-field v-model="tenant.secondaryContactNumber"
                          :value="tenant.secondaryContactNumber"
                          @input="updateField(index, tenant, 'secondaryContactNumber')"
                          label="Another contact number">
              </text-field>
            </div>
            <div class="col-xs-12 col-md-4">
              <text-field v-model="tenant.emailAddress"
                          :value="tenant.emailAddress"
                          :rules="[$validation.rules.required, $validation.rules.email]"
                          @input="updateField(index, tenant, 'emailAddress')"
                          label="Email Address"
                          type="email"
                          required>
              </text-field>
            </div>
          </div>
        </template>
      </v-expansion-panel-content>
    </v-expansion-panel>

    <div class="expansion-panel__buttons">
      <v-btn color="primary" @click="addOccupier(true)">Add adult tenant</v-btn>
      <v-btn @click="addOccupier(false)">Add child</v-btn>
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
      default: () => []
    },
    countries: {
      type: Array,
      default: () => []
    }
  },
  computed: {
    ...mapState({
      tenants: state => state.newTenancy.tenants,
      permissions: state => state.permissions
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
    deleteTenantAddress (tenantIndex, addressIndex) {
      this.$store.commit('TENANT_DELETE_ADDRESS', {
        tenantIndex,
        addressIndex
      })
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
