<template>

  <div class="container pricing">
    <div class="row">
       <div class="col-xs-12 col-md-4">
          <v-card>
            <v-card-title class="primary title green">
              I am a tenant
            </v-card-title>
            <v-card-text>
              <div class="pricing-value green">
                &pound;0.00
                <span class="undertext">Per Month.</span>
              </div>
              <ul>
                <li><v-icon class="green--text">done</v-icon> Always free, 100% guaranteed</li>
                <li><v-icon class="green--text">done</v-icon> Contact your landlord directly</li>
                <li><v-icon class="green--text">done</v-icon> Manage your account online</li>
                <li><v-icon class="green--text">done</v-icon> Request maintenance/repairs</li>
                <li><v-icon class="green--text">done</v-icon> See when your rent is due</li>
                <li><v-icon class="green--text">done</v-icon> Get reminders for important dates, like Gas safety check</li>
              </ul>
              <div class="sign-up">
                <v-btn outline large class="green green--text">Sign up</v-btn>
              </div>
            </v-card-text>
          </v-card>
       </div>
       <div class="col-xs-12 col-md-4">
          <v-card>
            <v-card-title class="primary title deep-orange">
              I am a landlord
            </v-card-title>
            <v-card-text>
              <div class="pricing-value deep-orange">
                &pound;0.00
                <span class="undertext">Per Month.</span>
              </div>
              <ul>
                <li><v-icon class="deep-orange--text">done</v-icon> Property portfolio manager</li>
                <li><v-icon class="deep-orange--text">done</v-icon> New tenancy wizards</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Checklists</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Bank/credit card manager</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Financial calculators</li>
                <li><v-icon class="deep-orange--text">done</v-icon> Support and contribute to the development of this platform</li>
              </ul>
              <div class="sign-up">
                <v-btn outline large class="deep-orange deep-orange--text">Sign up</v-btn>
              </div>
            </v-card-text>
          </v-card>
       </div>
      <div class="col-xs-12 col-md-4">
          <v-card>
            <v-card-title class="primary title blue-grey">
              I represent an agency
            </v-card-title>
            <v-card-text>
              <div class="pricing-value blue-grey">
                &pound;0.00
                <span class="undertext">Per Month.</span>
              </div>
              <ul>
                <li><v-icon class="blue-grey--text">done</v-icon> Manage your landlords online</li>
                <li><v-icon class="blue-grey--text">done</v-icon> New tenancy wizards</li>
                <li><v-icon class="blue-grey--text">done</v-icon> Checklists</li>
                <li><v-icon class="blue-grey--text">done</v-icon> Bank/credit card manager</li>
                <li><v-icon class="blue-grey--text">done</v-icon> Calculators</li>
                <li><v-icon class="blue-grey--text">done</v-icon> Support and contribute to the development of this platform</li>
              </ul>
              <div class="sign-up">
                <v-btn outline large class="blue-grey blue-grey--text">Sign up</v-btn>
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
  @import 'src/assets/styles/mixins';

  .pricing {
    .card__title {
      display: block;
      width: 100%;
      text-align: center;
    }
  }

  .col-xs-12 {
    overflow: hidden;
  }

  .card {
    @include desktop {
      margin: 20px;
      transition: transform 200ms linear;

      &:hover {
        transform: scale(1.01);
      }
    }
  }

  ul {
    list-style-type: none;
    padding: 0;

    li {
      padding: 20px 0;
      text-align: center;
      border-bottom: 1px solid rgba(0, 0, 0, 0.2);

      @include tablet {
        padding: 20px;
      }
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
