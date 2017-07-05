<template>
  <div class="header">
    <div class="header-inner flex-center">
      <ul>
        <li>
          <h1 class="display-2">Hello, landlords!</h1>
        </li>
        <li>
          <p>Let us help you manage your property portfolio</p>
        </li>
        <li>
          <v-btn class="green" @click.native="$router.push({name: 'registration'})">Go to registration &raquo;</v-btn>
        </li>
      </ul>
    </div>
  </div>
</template>

<style lang="scss" scoped>
  .header {
    background: url("../../assets/images/landing-page-bg.jpg") no-repeat center center fixed;
    background-size: cover;
    height: 100%;
    overflow: hidden;
    position: relative;
    cursor: default;

    .header-inner {
      position: absolute;
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

        h1, p {
          color: #fff;
        }
      }
    }
  }
</style>
