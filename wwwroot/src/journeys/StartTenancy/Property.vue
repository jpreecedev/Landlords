<template>
  <div>
    <h2 class="title">Your agreement with the tenant</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <label class="select-label">Which property are you renting out?</label>
        <select-list :items="portfolioProperties"
                     :rules="[$validation.rules.required]"
                     v-model="tenancy.propertyDetailsId"
                     @input="updateField(tenancy, 'propertyDetailsId')"
                     item-value="id"
                     label="Select a property"
                     required>
        </select-list>
      </div>
      <div class="col-xs-12 col-md-6">
        <label class="select-label">What kind of tenancy agreement is/will be in place?</label>
        <select-list :items="tenancyTypes"
                     :rules="[$validation.rules.required]"
                     v-model="tenancy.tenancyType"
                     @input="updateField(tenancy, 'tenancyType')"
                     label="Select a tenancy type"
                     required>
        </select-list>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <date-picker v-model="tenancy.startDate"
                     label="Tenancy start date"
                     @input="updateField(tenancy, 'startDate')"
                     :rules="[$validation.rules.required]">
        </date-picker>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <date-picker v-model="tenancy.endDate"
                     label="Tenancy end date"
                     @input="updateField(tenancy, 'endDate')"
                     :rules="[$validation.rules.required]">
        </date-picker>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12">
        <v-btn :disabled="!tenancy.startDate" @click="selectPeriod(3)">3 months</v-btn>
        <v-btn :disabled="!tenancy.startDate" @click="selectPeriod(6)">6 months</v-btn>
        <v-btn :disabled="!tenancy.startDate" @click="selectPeriod(12)">12 months</v-btn>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'
import { mapState } from 'vuex'

export default {
  name: 'property',
  props: {
    tenancyTypes: {
      type: Array,
      default: () => []
    }
  },
  data () {
    return {
      portfolioProperties: []
    }
  },
  created () {
    if (this.permissions.PD_GetBasic) {
      this.$http.get(`/api/propertydetails/basicdetails`)
        .then(response => {
          if (response.data) {
            this.portfolioProperties = response.data
          }
        })
    }
  },
  computed: {
    ...mapState({
      tenancy: state => state.newTenancy.tenancy,
      permissions: state => state.permissions
    })
  },
  methods: {
    selectPeriod (period) {
      this.$store.commit('TENANCY_UPDATE_END_DATE', moment(this.tenancy.startDate).add(period, 'M').subtract(1, 'day').format('YYYY-MM-DD'))
    },
    updateField (tenancy, field) {
      this.$store.commit('TENANCY_UPDATE', Object.assign(tenancy, { field: field }))
    }
  }
}
</script>
