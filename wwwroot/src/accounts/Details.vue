<template>
  <main>
    <div> 
      <h1 class="md-display-2">Account details</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>    
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset v-bind:disabled="!permissions.AC_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <md-card>
              <md-card-content>       
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="opened">Date Opened</label>          
                      <datepicker v-model="account.opened" id="opened" name="opened" placeholder="Select date..." class="md-input"></datepicker>
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="name">Account Name</label>
                      <md-input v-model="account.name" id="name" name="name" type="text" required />
                    </md-input-container>
                  </div>
                </div>         
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="number">Account Number</label>
                      <md-input v-model="account.number" id="number" name="name" type="text" required />
                    </md-input-container>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <md-input-container>
                      <label for="sortCode">Sort Code</label>
                      <md-input v-model="account.sortCode" id="sortCode" name="name" type="text" required />
                    </md-input-container>
                  </div>
                </div>
                <div class="row" :class="{ 'md-input-invalid': errors.has('type') }">
                  <div class="col-xs-12">
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
                <div class="row" :class="{ 'md-input-invalid': errors.has('providerName') }">
                  <div class="col-xs-12">
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
                <div class="row">
                  <div class="col-xs-12" :class="{ 'md-input-invalid': errors.has('openingBalance') }">
                    <md-input-container>
                      <label class="col-form-label" for="openingBalance">Opening Balance</label>
                      <md-input type="number" v-model="account.openingBalance" data-vv-name="openingBalance" v-validate="'required'" id="openingBalance" name="openingBalance" data-vv-validate-on="blur" />
                    </md-input-container>
                  </div>
                </div>
              </md-card-content>
            </md-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
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
