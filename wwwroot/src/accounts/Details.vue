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
                  <label class="col-12 col-form-label" for="type">Account Type</label>
                  <div class="col-12">
                    <select v-model="account.type" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="type" name="type" required>
                      <option disabled value="">Select an account type</option>
                      <option v-for="accountType in accountTypes" v-bind:value="accountType">{{ accountType }}</option>
                    </select>
                    <span v-show="errors.has('type')" v-bind:title="errors.first('type')" class="form-control-feedback">Select a valid account type</span>
                  </div>
                </div>
                <div class="form-group row" :class="{ 'has-danger': errors.has('providerName') }">
                  <label class="col-12 col-form-label" for="providerName">Provider Name</label>
                  <div class="col-12">
                    <select v-model="account.providerName" v-validate="'required'" data-vv-validate-on="blur" class="form-control" id="providerName" name="providerName" required>
                      <option disabled value="">Select a provider</option>
                      <option v-for="providerName in accountProviders" v-bind:value="providerName">{{ providerName }}</option>
                    </select>
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
                    <input type="number" class="form-control" v-model="account.openingBalance" v-validate="'required'" id="openingBalance" name="openingBalance" data-vv-validate-on="blur" placeholder="0.00">
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col">
            <button v-if="permissions.AC_Update" type="submit" class="btn btn-primary">Save</button>
            <input v-if="permissions.AC_Update" type="reset" class="btn btn-secondary" value="Reset" />
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
