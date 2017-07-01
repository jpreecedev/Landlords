<template>
  <div>
    <h1 class="display-2">Account details</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-alert error :value="errorBag.any()">
      <span v-show="!errorBag.has('GenericError')">Please fix validation errors and try and submit the form again</span>
      <span v-show="errorBag.has('GenericError')">{{ errorBag.first('GenericError') }}</span>
    </v-alert>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.AC_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-text>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('opened') }">
                      <label for="opened">Date Opened</label>
                      <md-input v-model="account.opened" id="opened" name="opened" type="date" data-vv-name="opened" v-validate="'required'" data-vv-validate-on="change" required />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('name') }">
                      <label for="name">Account Name</label>
                      <md-input v-model="account.name" id="name" name="name" type="text" data-vv-name="name" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errorBag.has('name')" class="md-error">Enter a valid account name</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('number') }">
                      <label for="number">Account Number</label>
                      <md-input v-model="account.number" id="number" name="number" type="text" data-vv-name="number" v-validate="'required'" data-vv-validate-on="change" required />
                      <span v-if="errorBag.has('number')" class="md-error">Enter a valid account number</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('sortCode') }">
                      <label for="sortCode">Sort Code</label>
                      <md-input v-model="account.sortCode" id="sortCode" name="sortCode" type="text" data-vv-name="sortCode" v-validate="'required'" data-vv-validate-on="change"  required />
                      <span v-if="errorBag.has('sortCode')" class="md-error">Enter a valid sort code</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('type') }">
                      <label for="type">Account Type</label>
                      <md-select v-model="account.type" v-validate="'required'" data-vv-validate-on="change" id="type" name="type" required>
                        <md-option disabled value="">Select an account type</md-option>
                        <md-option v-for="accountType in accountTypes" :key="accountType" :value="accountType">{{ accountType }}</md-option>
                      </md-select>
                      <span v-if="errorBag.has('type')" :title="errorBag.first('type')" class="md-error">Select a valid account type</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('providerName') }">
                      <label for="providerName">Provider Name</label>
                      <md-select v-model="account.providerName" v-validate="'required'" data-vv-validate-on="change" id="providerName" name="providerName" required>
                        <md-option disabled value="">Select a provider</md-option>
                        <md-option v-for="accountProvider in accountProviders" :key="accountProvider" :value="accountProvider">{{ accountProvider }}</md-option>
                      </md-select>
                      <span v-if="errorBag.has('type')" :title="errorBag.first('type')" class="md-error">Select a valid provider</span>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container :class="{ 'md-input-invalid': errorBag.has('openingBalance') }">
                      <label class="col-form-label" for="openingBalance">Opening Balance</label>
                      <md-input type="number" v-model="account.openingBalance" data-vv-name="openingBalance" v-validate="'required'" id="openingBalance" name="openingBalance" data-vv-validate-on="change" />
                    </md-input-container>
                  </div>
                </div>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary light v-if="permissions.AC_Update" type="submit">Save</v-btn>
            <v-btn flat dark v-if="permissions.AC_Update" type="reset">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </div>
</template>

<script>
import { ErrorBag } from 'vee-validate'
import utils from 'utils'

export default {
  name: 'accountDetails',
  data () {
    return {
      permissions: this.$store.state.permissions,
      accountTypes: [],
      accountProviders: [],
      account: {
        id: null,
        name: '',
        number: '',
        sortCode: '',
        providerName: '',
        type: '',
        porfolioId: null,
        opened: new Date().toJSON().slice(0, 10),
        openingBalance: 0
      }
    }
  },
  created () {
    this.$http.get(`/api/accounts/${this.$route.params.accountId}`).then(response => {
      Object.assign(this, utils.mapEntity(response.data, 'account', false))
    })
  },
  methods: {
    validateBeforeSubmit: function () {
      this.$validator.validateAll().then(() => {
        var bag = new ErrorBag()
        this.$http.post(`/api/accounts/`, { ...this.account })
          .then(() => {
            this.$router.push({ name: 'accounts-overview' })
          })
          .catch(response => {
            var validationResult = utils.getFormValidationErrors(response)
            validationResult.errors.forEach(validationError => {
              bag.add(validationError.key, validationError.messages[0], 'required')
            })
            if (validationResult.status) {
              bag.add('GenericError', validationResult.status, 'generic')
            }
          })
        this.$validator.errorBag = bag
      }).catch(() => {
        window.scrollTo(0, 0)
      })
    }
  }
}
</script>
