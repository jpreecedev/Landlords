<template>
  <div>
    <header>
      <h1 class="headline primary--text">Account details</h1>
      <p class="display-2 grey--text text--darken-1">Your bank account or other financial account details</p>
    </header>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.AC_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-text>
                <div class="row">
                  <div class="col-xs-12">
                    <date-picker v-model="account.opened"
                                 label="Date opened"
                                 :rules="[$validation.rules.required]">
                    </date-picker>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <text-field v-model="account.name"
                                :rules="[$validation.rules.required]"
                                label="Account name"
                                required>
                    </text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <text-field v-model="account.number"
                                :rules="[$validation.rules.required]"
                                label="Account number"
                                required>
                    </text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <text-field v-model="account.sortCode"
                                :rules="[$validation.rules.required]"
                                label="Sort code"
                                required>
                    </text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <select-list :items="accountTypes"
                                 :rules="[$validation.rules.required]"
                                 v-model="account.type"
                                 label="Account type"
                                 required>
                    </select-list>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <select-list :items="accountProviders"
                                 :rules="[$validation.rules.required]"
                                 v-model="account.providerName"
                                 label="Provider name"
                                 required>
                    </select-list>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <text-field v-model="account.openingBalance"
                                :rules="[$validation.rules.required, $validation.rules.min_value(account.openingBalance, 0)]"
                                label="Opening balance"
                                type="number"
                                min="0"
                                step="100"
                                prefix="£"
                                required>
                    </text-field>
                  </div>
                </div>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary v-if="permissions.AC_Update" type="submit" :loading="isSaving">Save</v-btn>
            <v-btn flat v-if="permissions.AC_Update" @click="reset()">Reset</v-btn>
          </div>
        </div>
      </fieldset>
    </form>
  </div>
</template>

<script>
import utils from 'utils'

export default {
  name: 'accountDetails',
  data () {
    return {
      isSaving: false,
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
    this.$http.get(`/api/accounts/${this.$route.params.accountId}`)
      .then(response => {
        Object.assign(this, utils.mapEntity(response.data, 'account', false))
        this.$validation.commit(this.$children)
      })
  },
  methods: {
    validateBeforeSubmit () {
      this.$validation.validate(this.$children)
        .then(() => {
          this.isSaving = true
          this.$http.post(`/api/accounts/`, { ...this.account })
            .then(() => {
              this.$router.push({ name: 'accounts-overview' })
            })
            .catch(response => {
              let validationResult = utils.getFormValidationErrors(response)
              validationResult.errors.forEach(validationError => {
                console.log('ERROR', validationError.key, validationError.messages[0], 'required')
              })
            })
            .finally(() => {
              this.isSaving = false
            })
        })
        .catch(() => {
          this.$bus.$emit('SHOW_NOTIFICATION', {
            message: 'Please fix the form validation issues before continuing.  Errors are highlighted in red.',
            context: 'error'
          })
        })
    },
    reset () {
      debugger
      this.$validation.reset(this.$children)
    }
  }
}
</script>
