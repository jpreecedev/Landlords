<template>
  <section id="timeline" class="timeline-outer">
    <div class="content">
      <div class="row">
        <div class="col-xs-12">
          <ul class="timeline">
            <li v-for="event in events"
                :key="event.id"
                :class="['event', event.status.toLowerCase().replace(' ', '-')]">
              <h3 class="headline">{{ event.status }}</h3>
              <p v-if="event.description"
                 class="subheading">
                {{ event.description }}
              </p>
              <small class="italic">
                By {{ event.user.name }} on <time :datetime="event.created">{{ event.created | formatDate }} at {{ event.created | formatTime }}</time>
              </small>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
  export default {
    name: 'timeline',
    props: {
      'events': {
        type: Array,
        required: true,
        default: () => []
      }
    }
  }
</script>

<style lang="scss" scoped>

  .content {
    text-align: center;
  }

  section.timeline-outer {
    margin: 0 auto;
  }

  .timeline {
    box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
    color: #333;
    margin: 2rem auto;
    letter-spacing: 0.5px;
    position: relative;
    line-height: 24px;
    list-style: none;
    text-align: left;
    padding: 0;
    .event {
      border-left: 8px solid #2196f3;
      border-bottom: 1px solid rgba(160, 160, 160, 0.2);
      padding-bottom: 15px;
      margin-bottom: 0;
      position: relative;
      padding: 16px 30px;
      &:last-of-type {
        border-bottom: none;
      }
      &:after {
        position: absolute;
        display: block;
        box-shadow: 0 0 0 8px #2196f3;
        left: -9px;
        background: #212121;
        border-radius: 50%;
        height: 11px;
        width: 11px;
        content: "";
        top: 42px;
      }
      &:not(:last-of-type) {
        opacity: .5;
      }
      &:hover {
        opacity: 1;
      }
      &:last-of-type {
        background-color: #bbdefb;
        &.in-progress {
          border-left: 8px solid #ffeb3b;
          background-color: #fff9c4;
          &:after {
            box-shadow: 0 0 0 8px #ffeb3b;
          }
        }
        &.action-required {
          border-left: 8px solid #ff5722;
          background-color: #ffccbc;
          &:after {
            box-shadow: 0 0 0 8px #ff5722;
          }
        }
        &.closed {
          border-left: 8px solid #4caf50;
          background-color: #c8e6c9;
          &:after {
            box-shadow: 0 0 0 8px #4caf50;
          }
        }
      }
      .headline {
        margin-bottom: 0;
      }
    }
  }
</style>

