<template>
  <div>
    <h1 class="display-2">Account details</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <form @submit.prevent="validateBeforeSubmit" role="form" novalidate>
      <fieldset :disabled="!permissions.AC_Update">
        <div class="row">
          <div class="col-xs-12 col-md-6">
            <v-card>
              <v-card-text>
                <div class="row">
                  <div class="col-xs-12">
                    <v-menu lazy :nudge-left="100">
                      <v-text-field slot="activator"
                                    label="Date opened"
                                    v-model="account.opened"
                                    prepend-icon="date_range"
                                    required readonly>
                      </v-text-field>
                      <v-date-picker v-model="account.opened"
                                     scrollable>
                      </v-date-picker>
                    </v-menu>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-text-field v-model="account.name"
                                  :rules="[$validation.rules.required]"
                                  label="Account name"
                                  required>
                    </v-text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-text-field v-model="account.number"
                                  :rules="[$validation.rules.required]"
                                  label="Account number"
                                  required>
                    </v-text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-text-field v-model="account.sortCode"
                                  :rules="[$validation.rules.required]"
                                  label="Sort code"
                                  required>
                    </v-text-field>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-select :items="accountTypes"
                              :rules="[$validation.rules.required]"
                              v-model="account.type"
                              label="Account type"
                              required>
                    </v-select>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-select :items="accountProviders"
                              :rules="[$validation.rules.required]"
                              v-model="account.providerName"
                              label="Provider name"
                              required>
                    </v-select>
                  </div>
                </div>
                <div class="row">
                  <div class="col-xs-12">
                    <v-text-field v-model="account.openingBalance"
                                  :rules="[$validation.rules.required, $validation.rules.min_value(account.openingBalance, 0)]"
                                  label="Opening balance"
                                  type="number"
                                  min="0"
                                  step="100"
                                  prefix="£"
                                  required>
                    </v-text-field>
                  </div>
                </div>
              </v-card-text>
            </v-card>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col-xs-12">
            <v-btn primary v-if="permissions.AC_Update" type="submit">Save</v-btn>
            <v-btn flat v-if="permissions.AC_Update" type="reset">Reset</v-btn>
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
      permissions: this.$store.state.permissions,
      accountTypes: [],
      accountProviders: [],
      errors: [],
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
      this.errors = []

      this.$http.post(`/api/accounts/`, { ...this.account })
        .then(() => {
          this.$router.push({ name: 'accounts-overview' })
        })
        .catch(response => {
          this.loggingIn = false
          if (!response.ok) {
            var validationResult = utils.getFormValidationErrors(response)
            validationResult.errors.forEach(validationError => {
              this.errors.push({
                key: validationError.key,
                message: validationError.messages[0]
              })
            })
            if (validationResult.status) {
              this.errors.push({
                key: 'GenericError',
                message: validationResult.status
              })
            }
          }
        })
    }
  }
}
</script>
