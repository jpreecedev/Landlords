<template>
  <div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="subheading">Please tell us which property you are renting out</div>
        <portfolio-properties ref="port" name="portfolio" @selected="portfolioPropertySelected" :properties="portfolioProperties"></portfolio-properties>
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="subheading">What kind of tenancy agreement is/will be in place?</div>
        <v-select v-bind:items="tenancyTypes" v-model="selectedTenancyType" item-value="id" label="Select a tenancy type" dark single-line auto></v-select>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :close-on-content-click="false" transition="v-scale-transition" offset-y :nudge-left="40">
          <v-text-field slot="activator" label="Tenancy start date" v-model="startDate" prepend-icon="date_range" readonly></v-text-field>
          <v-date-picker v-model="startDate" scrollable>
          </v-date-picker>
        </v-menu>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <v-menu lazy :close-on-content-click="false" transition="v-scale-transition" offset-y :nudge-left="40">
          <v-text-field slot="activator" label="Tenancy end date" v-model="endDate" prepend-icon="date_range" readonly></v-text-field>
          <v-date-picker v-model="endDate" scrollable>
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
        selectedProperty: null,
        portfolioProperties: [],
        selectedTenancyType: null,
        startDate: null,
        endDate: null
      }
    },
    created () {
      if (this.permissions.PD_GetBasic) {
        this.$http.get(`/api/propertydetails/basicdetails`).then(response => {
          if (response.data) {
            this.portfolioProperties = response.data
            this.$refs['port'].propertyModel = this.portfolioProperties[0].id
          }
        })
      }
    },
    methods: {
      portfolioPropertySelected: function (property) {
        this.selectedProperty = property
      },
      selectPeriod: function (period) {
        this.endDate = moment(this.startDate).add(period, 'M').subtract(1, 'day').format('YYYY-MM-DD')
      }
    }
  }

</script>
