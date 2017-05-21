<template>
  <main class="container">
    <div>
      <h1>New tenant</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div>
      <p class="text-muted">There are {{ outstandingActions }} outstanding actions.</p>
      <form>
        <fieldset>
          <div id="accordion" v-if="checklist && checklist.checklistItems && checklist.checklistItems.length" role="tablist" aria-multiselectable="true">
            <div class="card" v-for="item in checklist.checklistItems">
              <div class="card-header" role="tab" v-bind:id="item.key">
                <h5 class="mb-0">
                  <div class="form-check">
                  <label class="form-check-label">
                    <input class="form-check-input" type="checkbox" v-model="item.isCompleted">                                        
                    <span v-if="!item.isCompleted">{{ item.displayText }}</span>
                    <del class="text-muted" v-if="item.isCompleted">{{ item.displayText }}</del>                    
                  </label>
                  <p class="pointer float-right d-inline-block mb-0 text-right" @click="expand(item)">{{ item.isExpanded ? 'Collapse' : 'Expand' }} {{ item.isExpanded ? '&#9660;' : '&#9658;' }}</p>                  
                </div>
                </h5>
              </div>
              <div v-bind:id="item.key" v-bind:class="{'show': item.isExpanded}" role="tabpanel" v-bind:aria-labelledby="item.key" class="collapse">
                <div class="card-block">
                  <div class="form-group row">
                    <div class="col">
                      <label class="col-form-label" v-bind:for="item.key + 'comments'">Comments</label>
                      <textarea v-model="item.comments" class="form-control" v-bind:id="item.key + 'comments'" placeholder="Enter any comments here" v-bind:name="item.key + 'comments'" type="text" rows="4"></textarea>                    
                    </div>
                    <div class="col">
                      <label class="col-form-label" v-bind:for="item.key + 'actioned'">Date actioned</label>
                      <datepicker v-model="item.actioned" id="item.key + 'actioned'" name="item.key + 'actioned'" placeholder="Select date..." input-class="form-control"></datepicker>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col">
                      <p class="mb-2">Upload one or more supporting documents</p>
                      <label class="custom-file">
                        <input type="file" id="file" class="pointer custom-file-input">
                        <span class="custom-file-control"></span>
                      </label>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </fieldset>
      </form>
    </div>
  </main>
</template>

<script>
  import Datepicker from 'vuejs-datepicker'

  export default {
    name: 'newTenantMoveIn',
    components: { Datepicker },
    data () {
      return {
        checklistId: this.$route.params.checklistId,
        checklist: null
      }
    },
    created () {
      this.$http.get(`/api/checklists/${this.checklistId}`).then(response => {
        this.checklist = response.data
      })
    },
    computed: {
      outstandingActions: function () {
        if (!this.checklist) {
          return 0
        }

        var count = 0
        this.checklist.checklistItems.forEach(item => {
          if (!item.isCompleted) {
            count += 1
          }
        })
        return count
      }
    },
    methods: {
      expand: function (item) {
        item.isExpanded = !item.isExpanded
      }
    }
  }
</script>
