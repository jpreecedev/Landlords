<template>
  <main class="container">
    <div> 
      <h1>Account details</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>    
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset v-bind:disabled="!permissions.AC_Update">
        <div class="row">
          <div class="col-6">
            <div class="card">
              <div class="card-block">
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="name">Account Name</label>
                  <div class="col-12">
                    <input v-model="account.name" class="form-control" id="name" name="name" type="text" required>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="number">Account Number</label>
                  <div class="col-12">
                    <input v-model="account.number" class="form-control" id="number" name="name" type="text" required>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="sortCode">Sort Code</label>
                  <div class="col-12">
                    <input v-model="account.sortCode" class="form-control" id="sortCode" name="name" type="text" required>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('type') }">
                  <div class="col-12">
                    <md-input-container>
                      <label for="type">Account Type</label>
                      <md-select v-model="account.type" v-validate="'required'" data-vv-validate-on="blur" id="type" name="type" required>
                        <md-option disabled value="">Select an account type</md-option>
                        <md-option v-for="accountType in accountTypes" :key="accountType" v-bind:value="accountType">{{ accountType }}</md-option>
                      </md-select>
                    </md-input-container>
                    <span v-show="errors.has('type')" v-bind:title="errors.first('type')" class="form-control-feedback">Select a valid account type</span>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('providerName') }">
                  <div class="col-12">
                    <md-input-container>
                      <label for="providerName">Provider Name</label>
                      <md-select v-model="account.providerName" v-validate="'required'" data-vv-validate-on="blur" id="providerName" name="providerName" required>
                        <md-option disabled value="">Select a provider</md-option>
                        <md-option v-for="providerName in accountProviders" :key="providerName" v-bind:value="providerName">{{ providerName }}</md-option>
                      </md-select>
                    </md-input-container>
                    <span v-show="errors.has('providerName')" v-bind:title="errors.first('providerName')" class="form-control-feedback">Select a valid provider</span>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-12 col-form-label" for="opened">Date Opened</label>          
                  <div class="col-12">
                    <datepicker v-model="account.opened" id="opened" name="opened" placeholder="Select date..." input-class="form-control"></datepicker>
                  </div>
                </div>
                <div class="form-group row">
                  <div class="col" :class="{ 'has-danger': errors.has('openingBalance') }">
                    <label class="col-form-label" for="openingBalance">Opening Balance</label>
                    <div class="input-group">
                      <span class="input-group-addon">£</span>
                      <input type="number" class="form-control" v-model="account.openingBalance" v-validate="'required'" id="openingBalance" name="openingBalance" data-vv-validate-on="blur" placeholder="0.00">
                      <span class="input-group-addon">.00</span>
                    </div>
                  </div>
                </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col">
            <md-button v-if="permissions.AC_Update" type="submit" class="md-raised md-primary">Save</md-button>
            <md-button v-if="permissions.AC_Update" type="reset" class="md-raised md-default">Reset</md-button>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
import Datepicker from 'vuejs-datepicker'
import { ErrorBag } from 'vee-validate'
import utils from 'utils'

export default {
  name: 'accountDetails',
  components: { Datepicker },
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
  
<style lang="scss" scoped>
  
</style>
