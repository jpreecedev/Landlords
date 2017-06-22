<template>
  <div>
    <h2 class="title">Who are the tenants?</h2>

    <v-expansion-panel>
      <v-expansion-panel-content v-for="(tenant, index) in tenants" :key="tenant">
        <div slot="header">
          <tenant-type :tenant="tenant" />
        </div>
        <v-card>
          <div class="subheading">What is the {{ tenant.isAdult ? 'tenants' : 'childs' }} name?</div>
          <div class="row">
            <div class="col-xs-12 col-md-4">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('firstName') }">
                <label for="firstName">First name</label>
                <md-input v-model="tenant.firstName" id="firstName" name="firstName" type="text" data-vv-name="firstName" v-validate="'required'" data-vv-validate-on="change" required />
                <span v-if="errorBag.has('firstName')" class="md-error">Enter a valid first name</span>
              </md-input-container>
            </div>
            <div class="col-xs-12 col-md-4">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('middleName') }">
                <label for="middleName">Middle name(s)</label>
                <md-input v-model="tenant.middleName" id="middleName" name="middleName" type="text" />
              </md-input-container>
            </div>
            <div class="col-xs-12 col-md-4">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('lastName') }">
                <label for="lastName">Last name</label>
                <md-input v-model="tenant.lastName" id="lastName" name="lastName" type="text" data-vv-name="lastName" v-validate="'required'" data-vv-validate-on="change" required />
                <span v-if="errorBag.has('lastName')" class="md-error">Enter a valid last name</span>
              </md-input-container>
            </div>
            <div class="col-xs-12 col-md-4">
              <md-input-container :class="{ 'md-input-invalid': errorBag.has('dateOfBirth') }">
                <label for="dateOfBirth">Date of Birth</label>
                <md-input v-model="tenant.dateOfBirth" id="dateOfBirth" name="dateOfBirth" type="date" />
                <span v-if="errorBag.has('dateOfBirth')" class="md-error">Enter a date of birth</span>
              </md-input-container>
            </div>
          </div>
          <template v-if="tenant.isAdult">
            <div class="subheading mt-3">Now, tell us the tenants current address</div>
            <div class="row">
              <div class="col-xs-6">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('street') }">
                  <label for="street">Street address</label>
                  <md-textarea v-model="tenant.address.street" data-vv-name="street" v-validate="'required'" data-vv-validate-on="change"  id="street" name="street" required />
                  <span v-if="errorBag.has('street')" class="md-error">Enter a valid street address</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-4">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('townOrCity') }">
                  <label for="townOrCity">Town or City</label>
                  <md-input v-model="tenant.address.townOrCity" data-vv-name="townOrCity" v-validate="'required'" data-vv-validate-on="change"  id="townOrCity" name="townOrCity" required />
                  <span v-if="errorBag.has('townOrCity')" class="md-error">Enter a valid town or city</span>
                </md-input-container>
              </div>
              <div class="col-xs-4">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('countyOrRegion') }">
                  <label for="countyOrRegion">County or Region</label>
                  <md-input v-model="tenant.address.countyOrRegion" data-vv-name="countyOrRegion" v-validate="'required'" data-vv-validate-on="change"  id="countyOrRegion" name="countyOrRegion" required />
                  <span v-if="errorBag.has('countyOrRegion')" class="md-error">Enter a valid county or region</span>
                </md-input-container>
              </div>
              <div class="col-xs-4">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('postcode') }">
                  <label for="postcode">Postcode</label>
                  <md-input v-model="tenant.address.postcode" data-vv-name="postcode" v-validate="'required'" data-vv-validate-on="change"  id="postcode" name="postcode" required />
                  <span v-if="errorBag.has('postcode')" class="md-error">Enter a valid postal code</span>
                </md-input-container>
              </div>
            </div>
            <div class="row">
              <div class="col-xs-6">
                <v-select v-if="viewdata && viewdata.countries" v-bind:items="viewdata.countries" v-model="tenant.address.country" item-value="id" label="Select a country" dark single-line auto></v-select>
              </div>
            </div>
          </template>
          <template v-if="tenant.isAdult">
            <div class="subheading mt-3">And finally, their contact details</div>
            <div class="row">
              <div class="col-xs-6 col-md-4">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('mainContactNumber') }">
                  <label for="mainContactNumber">Main contact number</label>
                  <md-textarea v-model="tenant.mainContactNumber" data-vv-name="mainContactNumber" v-validate="'required'" data-vv-validate-on="change"  id="mainContactNumber" name="mainContactNumber" required />
                  <span v-if="errorBag.has('mainContactNumber')" class="md-error">Enter a valid contact number</span>
                </md-input-container>
              </div>
              <div class="col-xs-6 col-md-4">
                <md-input-container :class="{ 'md-input-invalid' : errorBag.has('secondaryContactNumber') }">
                  <label for="secondaryContactNumber">Another contact number</label>
                  <md-textarea v-model="tenant.secondaryContactNumber" data-vv-name="secondaryContactNumber" v-validate="'required'" data-vv-validate-on="change"  id="secondaryContactNumber" name="secondaryContactNumber" required />
                  <span v-if="errorBag.has('secondaryContactNumber')" class="md-error">Enter a valid contact number</span>
                </md-input-container>
              </div>
              <div class="col-xs-6 col-md-4">
                <md-input-container :class="{ 'md-input-invalid': errorBag.has('emailAddress') }">
                  <label for="emailAddress">Email Address</label>
                  <md-input type="email" id="emailAddress" name="emailAddress" data-vv-name="emailAddress" v-validate="'required|email'" v-model="tenant.emailAddress" data-vv-validate-on="change" required />
                  <span v-if="errorBag.has('emailAddress')" class="md-error">Enter a valid email address</span>
                </md-input-container>
              </div>
            </div>
          </template>
          <div class="row" v-if="index !== 0">
            <div class="col-xs-12">
              <v-btn primary error light @click.native="deleteOccupier(index)">
                Delete
                &nbsp;<tenant-type :tenant="tenant" />
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
  export default {
    name: 'tenants',
    props: {
      viewdata: Object
    },
    data () {
      return {
        tenants: [{
          firstName: null,
          middleName: null,
          lastName: null,
          isLeadTenant: true,
          isAdult: true,
          isChecked: true,
          mainContactNumber: null,
          secondaryContactNumber: null,
          emailAddress: null,
          address: {
            street: null,
            townOrCity: null,
            countyOrRegion: null,
            postcode: null,
            country: null
          }
        }],
        permissions: this.$store.state.permissions
      }
    },
    created () {
    },
    methods: {
      addOccupier: function (isAdult) {
        this.tenants.forEach(tenant => { tenant.isChecked = false })
        this.tenants.push({
          firstName: null,
          middleName: null,
          lastName: null,
          isLeadTenant: false,
          isAdult: isAdult,
          isChecked: true,
          mainContactNumber: null,
          secondaryContactNumber: null,
          emailAddress: null,
          address: {
            street: null,
            townOrCity: null,
            countyOrRegion: null,
            postcode: null,
            country: null
          }
        })
      },
      deleteOccupier: function (index) {
        this.tenants.splice(index, 1)
      }
    }
  }

</script>
