<template>
  <md-tab md-icon="home">
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <div class="md-subheading">Please tell us which property you are renting out</div>
        <portfolio-properties ref="port" name="portfolio" @selected="portfolioPropertySelected" :properties="portfolioProperties" />
      </div>
      <div class="col-xs-12 col-md-6">
        <div class="md-subheading">What kind of tenancy agreement is/will be in place?</div>
        <md-input-container v-if="viewdata && viewdata.tenancyTypes">
          <label for="tenancyType">Select a tenancy type</label>
          <md-select v-model="selectedTenancyType" id="tenancyType" name="tenancyType">
            <md-option disabled>Select a tenancy type</md-option>
            <md-option v-for="tenancyType in viewdata.tenancyTypes" :key="tenancyType" v-bind:value="tenancyType">{{ tenancyType }}</md-option>
          </md-select>
        </md-input-container>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <md-input-container>
          <md-icon>date_range</md-icon>
          <label for="startDate">Tenancy Start Date</label>
          <md-input v-model="startDate" id="startDate" name="startDate" type="date" />
        </md-input-container>
      </div>
    </div>
    <div class="row">
      <div class="col-xs-12 col-md-6">
        <md-input-container>
          <md-icon>date_range</md-icon>
          <label for="startDate">Tenancy End Date</label>
          <md-input v-model="endDate" id="endDate" name="endDate" type="date" />
        </md-input-container>
        <md-button :disabled="!startDate" @click.native="selectPeriod(3)" class="md-default">3 months</md-button>
        <md-button :disabled="!startDate" @click.native="selectPeriod(6)" class="md-default">6 months</md-button>
        <md-button :disabled="!startDate" @click.native="selectPeriod(12)" class="md-default">12 months</md-button>
      </div>
    </div>
  </md-tab>
</template>

<script>
  import moment from 'moment'

  export default {
    name: 'starttenancy-home',
    props: {
      viewdata: Object
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
