<template>
  <v-footer app class="grey lighten-3 black--text">
    <div class="container">
      <p class="social">
        <a href="#">
          <i class="icon facebook"></i>
        </a>
        <a href="#">
          <i class="icon linkedin"></i>
        </a>
        <a href="#">
          <i class="icon email"></i>
        </a>
      </p>
      <p class="copyright">© 2017 Landlords. All rights reserved.</p>
    </div>
  </v-footer>
</template>

<style lang="scss" scoped>
  .footer {
    padding: 1rem;
    height: auto;
    text-align: center;
  }

  .copyright {
    margin-bottom: 0;
  }

  .icon {
    width: 60px;
    height: 60px;
    display: inline-block;
    border-radius: 35px;
    margin: 0 4px;

    &:before {
      margin: 14px auto;
      content: " ";
      width: 32px;
      height: 32px;
      display: block;
    }

    &.facebook {
      background-color: #3C5B9B;
      &:before {
        background-image: url("../../assets/images/social/facebook-logo.png");
      }
    }

    &.linkedin {
      background-color: #0077b5;
      &:before {
        background-image: url("../../assets/images/social/linkedin-logo.png");
      }
    }

    &.email {
      background-color: #4a7f45;
      &:before {
        background-image: url("../../assets/images/social/mail.png");
      }
    }
  }
</style>
