<template>
  <header class="header">
    <div class="header-inner flex-center">
      <ul>
        <li>
          <v-icon class="white--text" style="font-size: 13rem;" x-large>home</v-icon>
        </li>
        <li>
          <h1 class="display-3">Hello, landlords!</h1>
        </li>
        <li>
          <p>We will make managing your property portfolio a breeze!</p>
        </li>
        <li>
          <v-btn color="primary" class="green" @click="$router.push({name: 'registration'})">Go to registration &raquo;</v-btn>
        </li>
      </ul>
    </div>
  </header>
</template>

<style lang="scss" scoped>
  .header {
    background: url("../../assets/images/landing-page-bg.jpg") no-repeat center center fixed;
    background-size: cover;
    height: 100%;
    position: relative;
    cursor: default;

    .header-inner {
      overflow: hidden;
      left: 0;
      height: 100%;
      width: 100%;
      top: 0;
      background: rgba(0, 0, 0, .7);

      ul {
        max-width: 720px;
        width: 100%;
        list-style: none;
        text-align: center;
        color: #fff;
        background-color: rgba(0, 0, 0, .5);
        padding: 20px;

        h1, p {
          color: #fff;
        }
      }
    }
  }
</style>
