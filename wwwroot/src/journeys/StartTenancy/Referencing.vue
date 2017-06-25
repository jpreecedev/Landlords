<template>
  <div>
    <h2 class="title">Confirm the identity of each adult tenant</h2>
    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="tenant">
        <div slot="header">
          <tenant-type :tenant="tenant"></tenant-type>
        </div>
        <div class="subheading">Work details</div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <v-select :items="employmentTypes"
                       v-model="tenant.employmentType"
                       @input="updateField(index, tenant, 'employmentType')"
                       label="Select an employment type"
                       dark single-line auto required>
            </v-select>
          </div>
          <div class="col-xs-12 col-md-4">
            <v-text-field v-model="tenant.occupation"
                          :value="tenant.occupation"
                          @input="updateField(index, tenant, 'occupation')"
                          label="Tenant occupation"
                          required>
            </v-text-field>
            <!--<span v-if="errorBag.has('title')" class="md-error">Select a valid title</span>-->
          </div>
          <div class="col-xs-12 col-md-4">
            <v-text-field v-model="tenant.companyName"
                          :value="tenant.companyName"
                          @input="updateField(index, tenant, 'companyName')"
                          label="Employer name">
            </v-text-field>
            <!--<span v-if="errorBag.has('title')" class="md-error">Select a valid title</span>-->
          </div>
        </div>
        <div class="row">
          <div class="col-xs-12 col-md-4">
            <v-text-field v-model="tenant.companyName"
                          :value="tenant.companyName"
                          @input="updateField(index, tenant, 'companyName')"
                          label="Employer name">
            </v-text-field>
          </div>
          <div class="col-xs-12 col-md-4">
            <v-text-field v-model="tenant.workAddress"
                          :value="tenant.workAddress"
                          :multi-line="true"
                          :rows="1"
                          :auto-grow="true"
                          @input="updateField(index, tenant, 'workAddress')"
                          label="Work address">
            </v-text-field>
          </div>
          <div class="col-xs-12 col-md-4">
            <v-text-field v-model="tenant.workContactNumber"
                          :value="tenant.workContactNumber"
                          @input="updateField(index, tenant, 'workContactNumber')"
                          label="Employer contact number"
                          type="number">
            </v-text-field>
          </div>
        </div>
        <div class="subheading">Identity</div>
        <div class="row">
          <div class="col-xs-6">
            <v-text-field v-model="tenant.drivingLicenseReference"
                          :value="tenant.drivingLicenseReference"
                          @input="updateField(index, tenant, 'drivingLicenseReference')"
                          label="Driving license number">
            </v-text-field>
          </div>
          <div class="col-xs-6">
            <v-text-field v-model="tenant.passportReference"
                          :value="tenant.passportReference"
                          @input="updateField(index, tenant, 'passportReference')"
                          label="Passport reference">
            </v-text-field>
          </div>
        </div>
        <div class="subheading">Referencing contacts</div>
        <div class="row">
          <div class="col-xs-12">
            <v-expansion-panel class="mt-3 mb-3 white">
              <v-expansion-panel-content v-for="(contact, contactIndex) in tenant.contacts" :key="contact">
                <div slot="header">
                  Contact {{ contactIndex + 1 }} <template v-if="contact.relationship">({{ contact.relationship }})</template>
                </div>
                <div class="row">
                  <div class="col-xs-6">
                    <v-text-field v-model="contact.name"
                                  :value="contact.name"
                                  @input="updateContact(index, contactIndex, contact, 'name')"
                                  label="Contact name">
                    </v-text-field>
                  </div>
                  <div class="col-xs-6">
                    <v-select :items="contactTypes"
                              v-model="contact.relationship"
                              @input="updateContact(index, contactIndex, contact, 'relationship')"
                              label="Select a relationship"
                              dark single-line auto>
                    </v-select>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-6">
                    <v-text-field v-model="contact.mainContactNumber"
                                  :value="contact.mainContactNumber"
                                  @input="updateContact(index, contactIndex, contact, 'mainContactNumber')"
                                  label="Main contact number"
                                  type="number">
                    </v-text-field>
                  </div>
                  <div class="col-xs-6">
                    <v-text-field v-model="contact.secondaryContactNumber"
                                  :value="contact.secondaryContactNumber"
                                  @input="updateContact(index, contactIndex, contact, 'secondaryContactNumber')"
                                  label="Alternative contact number"
                                  type="number">
                    </v-text-field>
                  </div>
                </div>
                <div class="row" v-if="contactIndex !== 0">
                  <div class="col-xs-12">
                    <v-btn primary error light @click.native="deleteTenantContact(index, contactIndex)">
                      Delete Contact
                    </v-btn>
                  </div>
                </div>
              </v-expansion-panel-content>
            </v-expansion-panel>
            <v-btn dark @click.native="addTenantContact(index)">Add another contact</v-btn>
          </div>
        </div>
        <div class="subheading mt-3">Other</div>
        <div class="row">
          <div class="col-xs-12">
            <v-text-field v-model="tenant.additionalInformation"
                          :value="tenant.additionalInformation"
                          :multi-line="true"
                          :rows="1"
                          :auto-grow="true"
                          @input="updateField(index, tenant, 'additionalInformation')"
                          label="Any additional information/notes of interest">
            </v-text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-xs-4">
            <v-checkbox v-model="tenant.isSmoker"
                        @change="updateField(index, tenant, 'isSmoker')"
                        label="Does this tenant smoke?">
            </v-checkbox>
          </div>
          <div class="col-xs-4">
            <v-checkbox v-model="tenant.hasPets"
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
        permissions: this.$store.state.permissions
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
        tenants: state => state.newTenancy.tenants.filter(tenant => tenant.isAdult)
      })
    }
  }

</script>
