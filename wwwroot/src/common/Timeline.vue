<template>
  <div class="main-container">
    <section id="timeline" class="timeline-outer">
      <div class="content">
        <div class="row">
          <div class="col-xs-12">
            <ul class="timeline">
              <li v-for="event in events"
                  :key="event.id"
                  :class="{ event}">
                <h3>Status: {{ event.status }}</h3>
                <p>
                  {{ event.description }}
                </p>
                <small class="italic">
                  By {{ event.user.name }} on {{ event.created | formatDate }}
                </small>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </section>
  </div>
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

  /* Timeline */

  .timeline {
    border-left: 8px solid #42A5F5;
    border-bottom-right-radius: 2px;
    border-top-right-radius: 2px;
    box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
    color: #333;
    margin: 50px auto;
    letter-spacing: 0.5px;
    position: relative;
    line-height: 24px;
    padding: 20px;
    list-style: none;
    text-align: left;
    h3 {
      font-size: 1.5rem;
      padding-top: .25rem;
    }
    .event {
      border-bottom: 1px solid rgba(160, 160, 160, 0.2);
      padding-bottom: 15px;
      margin-bottom: 20px;
      position: relative;
      padding-left: 10px;
      &:last-of-type {
        padding-bottom: 0;
        margin-bottom: 0;
        border: none;
      }
      &:after {
        position: absolute;
        display: block;
        box-shadow: 0 0 0 8px #42A5F5;
        left: -30px;
        background: #212121;
        border-radius: 50%;
        height: 11px;
        width: 11px;
        content: "";
        top: 5px;
      }
    }
  }

  /**/
  /*——————————————
  Responsive Stuff
  ———————————————*/

  @media (max-width: 945px) {
    .timeline {
      .event::before {
        left: 0.5px;
        top: 20px;
        min-width: 0;
        font-size: 13px;
      }
      h3 {
        font-size: 16px;
      }
      p {
        padding-top: 20px;
      }
    }
    section.lab h3.card-title {
      padding: 5px;
      font-size: 16px;
    }
  }

  @media (max-width: 768px) {
    .timeline {
      .event {
        &::before {
          left: 0.5px;
          top: 20px;
          min-width: 0;
          font-size: 13px;
        }
        &:nth-child(1)::before, &:nth-child(3)::before, &:nth-child(5)::before {
          top: 38px;
        }
      }
      h3 {
        font-size: 16px;
      }
      p {
        padding-top: 20px;
      }
    }
  }
</style>

