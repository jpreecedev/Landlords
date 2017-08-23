<template>
  <div>
    <h2 class="title">Confirm the identity of each adult tenant</h2>
    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="index">
        <div slot="header">
          <tenant-type :tenant="tenant"></tenant-type>
        </div>
        <div class="subheading">Work details</div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <select-list :items="employmentTypes"
                         :rules="[$validation.rules.required]"
                         v-model="tenant.employmentType"
                         @input="updateField(index, tenant, 'employmentType')"
                         label="Select an employment type"
                         required>
            </select-list>
          </div>
          <div class="col-xs-12 col-md-4">
            <text-field v-model="tenant.occupation"
                        :value="tenant.occupation"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.occupation, 2)]"
                        @input="updateField(index, tenant, 'occupation')"
                        label="Tenant occupation"
                        required>
            </text-field>
          </div>
          <div class="col-xs-12 col-md-4">
            <text-field v-model="tenant.companyName"
                        :value="tenant.companyName"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.companyName, 2)]"
                        @input="updateField(index, tenant, 'companyName')"
                        label="Employer name"
                        required>
            </text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <text-field v-model="tenant.workAddress"
                        :value="tenant.workAddress"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.workAddress, 2)]"
                        :multiline="true"
                        :rows="1"
                        @input="updateField(index, tenant, 'workAddress')"
                        label="Work address"
                        required>
            </text-field>
          </div>
          <div class="col-xs-12 col-md-4">
            <text-field v-model="tenant.workContactNumber"
                        :value="tenant.workContactNumber"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.workContactNumber, 2)]"
                        @input="updateField(index, tenant, 'workContactNumber')"
                        label="Employer contact number"
                        required>
            </text-field>
          </div>
        </div>
        <div class="subheading">Identity</div>
        <div class="row">
          <div class="col-xs-6">
            <text-field v-model="tenant.drivingLicenseReference"
                        :value="tenant.drivingLicenseReference"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.drivingLicenseReference, 2)]"
                        @input="updateField(index, tenant, 'drivingLicenseReference')"
                        label="Driving license number"
                        required>
            </text-field>
          </div>
          <div class="col-xs-6">
            <text-field v-model="tenant.passportReference"
                        :value="tenant.passportReference"
                        :rules="[$validation.rules.required, $validation.rules.min_length(tenant.passportReference, 2)]"
                        @input="updateField(index, tenant, 'passportReference')"
                        label="Passport reference"
                        required>
            </text-field>
          </div>
        </div>
        <div class="subheading">Referencing contacts</div>
        <div class="row">
          <div class="col-xs-12">
            <v-expansion-panel class="mt-3 mb-3 white">
              <v-expansion-panel-content v-for="(contact, contactIndex) in tenant.contacts" :key="contactIndex">
              <div slot="header" class="accordion-header">
                <div class="accordion col">
                  <span v-if="!contact.isDeleted" class="display-text default">Contact {{ contactIndex + 1 }} <template v-if="contact.name">({{ contact.name }})</template></span>
                  <del v-else class="display-text default text-muted">Contact {{ contactIndex + 1 }} <template v-if="contact.name">({{ contact.name }})</template></del>
                </div>
                <div class="accordion grow"></div>
                <div class="accordion col">
                  <v-btn @click.stop="deleteTenantContact(index, contactIndex)"
                         v-if="contactIndex > 0 && !contact.isDeleted"
                         icon>
                    <v-icon>delete_forever</v-icon>
                  </v-btn>
                </div>
              </div>
                <div class="row">
                  <div class="col-xs-6">
                    <text-field v-model="contact.name"
                                :value="contact.name"
                                :rules="[$validation.rules.required, $validation.rules.min_length(contact.name, 2)]"
                                @input="updateContact(index, contactIndex, contact, 'name')"
                                label="Contact name"
                                required>
                    </text-field>
                  </div>
                  <div class="col-xs-6">
                    <select-list :items="contactTypes"
                                 :rules="[$validation.rules.required, $validation.rules.min_length(contact.relationship, 2)]"
                                 v-model="contact.relationship"
                                 @input="updateContact(index, contactIndex, contact, 'relationship')"
                                 label="Select a relationship"
                                 required>
                    </select-list>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-6">
                    <text-field v-model="contact.mainContactNumber"
                                :value="contact.mainContactNumber"
                                :rules="[$validation.rules.required, $validation.rules.min_length(contact.mainContactNumber, 2)]"
                                @input="updateContact(index, contactIndex, contact, 'mainContactNumber')"
                                label="Main contact number"
                                type="text"
                                required>
                    </text-field>
                  </div>
                  <div class="col-xs-6">
                    <text-field v-model="contact.secondaryContactNumber"
                                :value="contact.secondaryContactNumber"
                                @input="updateContact(index, contactIndex, contact, 'secondaryContactNumber')"
                                label="Alternative contact number"
                                type="number">
                    </text-field>
                  </div>
                </div>
              </v-expansion-panel-content>
            </v-expansion-panel>
            <v-btn @click="addTenantContact(index)">Add another contact</v-btn>
          </div>
        </div>
        <div class="subheading mt-3">Other</div>
        <div class="row">
          <div class="col-xs-12">
            <text-field v-model="tenant.additionalInformation"
                        :value="tenant.additionalInformation"
                        :multiline="true"
                        :rows="1"
                        @input="updateField(index, tenant, 'additionalInformation')"
                        label="Any additional information/notes of interest">
            </text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <v-checkbox v-model="tenant.isSmoker"
                        color="primary"
                        @change="updateField(index, tenant, 'isSmoker')"
                        label="Does this tenant smoke?">
            </v-checkbox>
          </div>
          <div class="col-xs-12 col-md-4">
            <v-checkbox v-model="tenant.hasPets"
                        color="primary"
                        @change="updateField(index, tenant, 'hasPets')"
                        label="Does this tenant have any pets?">
            </v-checkbox>
          </div>
        </div>
      </v-expansion-panel-content>
    </v-expansion-panel>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'tenants',
  props: {
    employmentTypes: {
      type: Array,
      default: () => []
    },
    contactTypes: {
      type: Array,
      default: () => []
    }
  },
  data () {
    return {
    }
  },
  methods: {
    updateField (index, tenant, field) {
      this.$store.commit('TENANT_UPDATE_FIELD', Object.assign(tenant, {index, field: field}))
    },
    updateContact (tenantIndex, contactIndex, contact, field) {
      this.$store.commit('TENANT_UPDATE_CONTACT', {
        tenantIndex,
        contactIndex,
        contact,
        field
      })
    },
    addTenantContact (tenantIndex) {
      this.$store.commit('TENANT_ADD_CONTACT', tenantIndex)
    },
    deleteTenantContact (tenantIndex, contactIndex) {
      this.$store.commit('TENANT_DELETE_CONTACT', { tenantIndex, contactIndex })
    }
  },
  computed: {
    ...mapState({
      tenants: state => state.newTenancy.tenants.filter(tenant => tenant.isAdult),
      permissions: state => state.permissions
    })
  }
}
</script>
