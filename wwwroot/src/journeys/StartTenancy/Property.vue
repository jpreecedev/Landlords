<template>
  <div>
    <h2 class="title">Your agreement with the tenant</h2>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">Which property are you renting out?</div>
        <v-select :items="portfolioProperties"
                  :value="selectedProperty"
                  @input="updateSelectedProperty"
                  item-value="id"
                  label="Select a property"
                  dark single-line auto required>
        </v-select>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">What kind of tenancy agreement is/will be in place?</div>
        <v-select :items="tenancyTypes"
                  :value="selectedTenancyType"
                  @input="updateSelectedTenancyType"
                  item-value="id"
                  label="Select a tenancy type"
                  dark single-line auto>
        </v-select>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :nudge-left="100">
          <v-text-field slot="activator" label="Tenancy start date" v-model="startDate" prepend-icon="date_range" readonly></v-text-field>
          <v-date-picker v-model="startDate"
                         :value="startDate"
                         @input="updateSelectedStartDate"
                         scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :nudge-left="100">
          <v-text-field slot="activator" label="Tenancy end date" v-model="endDate" prepend-icon="date_range" readonly></v-text-field>
          <v-date-picker v-model="endDate"
                         :value="endDate"
                         @input="updateSelectedEndDate"
                         scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12">
        <v-btn flat :disabled="!startDate" @click.native="selectPeriod(3)">3 months</v-btn>
        <v-btn flat :disabled="!startDate" @click.native="selectPeriod(6)">6 months</v-btn>
        <v-btn flat :disabled="!startDate" @click.native="selectPeriod(12)">12 months</v-btn>
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
        selectedProperty: state => state.newTenancy.tenancy.propertyDetailsId,
        selectedTenancyType: state => state.newTenancy.tenancy.tenancyType,
        startDate: state => state.newTenancy.tenancy.startDate,
        endDate: state => state.newTenancy.tenancy.endDate
      })
    },
    methods: {
      selectPeriod (period) {
        this.$store.commit('TENANCY_SELECTED_END_DATE', moment(this.startDate).add(period, 'M').subtract(1, 'day').format('YYYY-MM-DD'))
      },
      updateSelectedProperty (propertyId) {
        this.$store.commit('TENANCY_SELECTED_PROPERTY', propertyId)
      },
      updateSelectedTenancyType (tenancyType) {
        this.$store.commit('TENANCY_SELECTED_TENANCY_TYPE', tenancyType)
      },
      updateSelectedStartDate (startDate) {
        this.$store.commit('TENANCY_SELECTED_START_DATE', startDate)
      },
      updateSelectedEndDate (endDate) {
        this.$store.commit('TENANCY_SELECTED_END_DATE', endDate)
      }
    }
  }

</script>
