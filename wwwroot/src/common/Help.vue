<template>
  <div class="help mt-5">
    <div class="row top-xs">
      <div class="col-xs-12 col-md-2 center-xs">
        <v-icon>live_help</v-icon>
      </div>
      <div class="col-xs-12 col-md-10">
        <h3 class="title">{{ title }}</h3>
        <p>{{ text }}</p>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'help',
    props: {
      'title': {
        type: String,
        default: null
      },
      'text': {
        type: String,
        default: null
      }
    }
  }
</script>

<style lang="scss" scoped>
  @import 'src/assets/styles/mixins';

  .help {
    text-align: center;

    @include tablet {
      text-align: left;
      padding-left: 2rem;
      max-width: 50%;
    }
  }

  i {
    font-size: 5rem;
    margin-bottom: 1rem;

    @include tablet {
      margin-bottom: 0;
    }
  }

  p {
    margin-bottom: 0;
  }
</style>
