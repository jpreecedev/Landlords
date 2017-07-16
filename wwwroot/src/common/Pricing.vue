<template>

  <div class="container">
    <div class="row">
       <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary title green">
              Free
            </v-card-title>
            <v-card-text>
              <div class="pricing-value green">
                &pound;0.00
                <span class="undertext">Per Month</span>
              </div>
              <ul>
                <li><v-icon class="green--text">done</v-icon> Always free, 100% guaranteed</li>
                <li><v-icon class="green--text">done</v-icon> Property portfolio manager</li>
                <li><v-icon class="green--text">done</v-icon> New tenancy wizards</li>
                <li><v-icon class="green--text">done</v-icon> Checklists</li>
                <li><v-icon class="green--text">done</v-icon> Bank/credit card manager</li>
                <li><v-icon class="green--text">done</v-icon> Calculators</li>
              </ul>
              <div class="sign-up">
                <v-btn outline large class="green green--text">Sign up</v-btn>
              </div>
            </v-card-text>
          </v-card>
       </div>
       <div class="col-xs-12 col-md-6">
          <v-card>
            <v-card-title class="primary title deep-orange">
              Pro
            </v-card-title>
            <v-card-text>
              <div class="pricing-value deep-orange">
                &pound;4.99
                <span class="undertext">Per Month</span>
              </div>
              <ul>
                <li><v-icon class="deep-orange--text">done</v-icon> Support and contribute to the development of this platform</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Property portfolio manager</li>
                <li><v-icon class="deep-orange--text">done</v-icon> New tenancy wizards</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Checklists</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Bank/credit card manager</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Calculators</li>
              </ul>
              <div class="sign-up">
                <v-btn outline large class="deep-orange deep-orange--text">Sign up</v-btn>
              </div>
            </v-card-text>
          </v-card>
       </div>
    </div>
  </div>

</template>

<script>
export default {
  name: 'pricing'
}
</script>

<style lang="scss" scoped>

  .container {
    margin-top: 1rem;
  }

  .col-xs-12 {
    overflow: hidden;
  }

  .card {
    margin: 20px;
  }

  ul {
    list-style-type: none;
    padding: 0;

    li {
      padding: 20px;
      text-align: center;
      border-bottom: 1px solid rgba(0, 0, 0, 0.2);
    }
  }

  .pricing-value {
    width: 180px;
    height: 180px;
    padding-top: 46px;
    border-radius: 50%;
    color: #fff;
    font-size: 46px;
    font-weight: 300;
    margin: 10px auto;
    text-align: center;
    .smallText {
      font-size: 14px;
    }
    .undertext {
      display: block;
      font-size: 16px;
    }
  }

  .sign-up {
    margin: 40px auto 10px auto;
    text-align: center;

    button {
      text-transform: none;
    }
  }

</style>
