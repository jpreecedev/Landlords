<template>
  <md-tabs class="md-transparent" :md-fixed="true" :md-centered="true">

    <md-tab md-icon="announcement" :md-active="true">
      <p>Let's get started creating a new tenancy.  You will need the following information;</p>
      <ul>
        <li>The address of the property you're renting out</li>
        <li>The type of tenancy agreement</li>
        <li>The start/end date</li>
        <li>The agreed amount of rent, and how often it will be paid</li>
        <li>The name of all the occupiers (tenants)</li>
        <li>And a few other bits and bobs</li>
      </ul>
      <p>It will probably take 20 minutes to complete this bit, and we will help you best we can along the way</p>
    </md-tab>

    <md-tab md-icon="home">
      <div class="md-subheading">Please tell us which property you are renting out</div>
      <portfolio-properties ref="port" name="portfolio" @selected="portfolioPropertySelected" :properties="portfolioProperties" />
    </md-tab>

    <md-tab md-icon="phone">
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt dolorum quas amet cum vitae, omnis! Illum quas voluptatem, expedita iste, dicta ipsum ea veniam dolore in, quod saepe reiciendis nihil.</p>
    </md-tab>

    <md-tab md-icon="favorite">
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt dolorum quas amet cum vitae, omnis! Illum quas voluptatem, expedita iste, dicta ipsum ea veniam dolore in, quod saepe reiciendis nihil.</p>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt dolorum quas amet cum vitae, omnis! Illum quas voluptatem, expedita iste, dicta ipsum ea veniam dolore in, quod saepe reiciendis nihil.</p>
    </md-tab>

    <md-tab md-icon="near_me">
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt dolorum quas.</p>
    </md-tab>
  </md-tabs>
</template>

<script>
  export default {
    name: 'startTenancy',
    data () {
      return {
        permissions: this.$store.state.permissions,
        selectedProperty: null,
        portfolioProperties: []
      }
    },
    created () {
      if (this.permissions.PD_GetBasic) {
        return this.$http.get(`/api/propertydetails/basicdetails`).then(response => {
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
      }
    }
  }
</script>
  
<style lang="scss" scoped>

  .md-tab {
    padding: 32px 16px;
  }

</style>
