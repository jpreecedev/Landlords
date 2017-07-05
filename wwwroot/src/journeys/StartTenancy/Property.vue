<template>
  <div>
    <h2 class="title">Your agreement with the tenant</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">Which property are you renting out?</div>
        <v-select :items="portfolioProperties"
                  :rules="[$validation.rules.required]"
                  v-model="tenancy.propertyDetailsId"
                  @input="updateField(tenancy, 'propertyDetailsId')"
                  item-value="id"
                  label="Select a property"
                  required>
        </v-select>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">What kind of tenancy agreement is/will be in place?</div>
        <v-select :items="tenancyTypes"
                  :rules="[$validation.rules.required]"
                  v-model="tenancy.tenancyType"
                  @input="updateField(tenancy, 'tenancyType')"
                  item-value="id"
                  label="Select a tenancy type"
                  required>
        </v-select>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :nudge-left="100">
          <v-text-field slot="activator"
                        label="Tenancy start date"
                        v-model="tenancy.startDate"
                        prepend-icon="date_range"
                        required readonly>
          </v-text-field>
          <v-date-picker v-model="tenancy.startDate"
                         :value="tenancy.startDate"
                         @input="updateField(tenancy, 'startDate')"
                         scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :nudge-left="100">
          <v-text-field slot="activator"
                        label="Tenancy end date"
                        v-model="tenancy.endDate"
                        prepend-icon="date_range"
                        required readonly>
          </v-text-field>
          <v-date-picker v-model="tenancy.endDate"
                         :value="tenancy.endDate"
                         @input="updateField(tenancy, 'endDate')"
                         scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12">
        <v-btn flat :disabled="!tenancy.startDate" @click.native="selectPeriod(3)">3 months</v-btn>
        <v-btn flat :disabled="!tenancy.startDate" @click.native="selectPeriod(6)">6 months</v-btn>
        <v-btn flat :disabled="!tenancy.startDate" @click.native="selectPeriod(12)">12 months</v-btn>
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
        default: []
      }
    },
    data () {
      return {
        permissions: this.$store.state.permissions,
        portfolioProperties: []
      }
    },
    created () {
      if (this.permissions.PD_GetBasic) {
        this.$http.get(`/api/propertydetails/basicdetails`).then(response => {
          if (response.data) {
            this.portfolioProperties = response.data
          }
        })
      }
    },
    computed: {
      ...mapState({
        tenancy: state => state.newTenancy.tenancy
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
