<template>
  <main class="container">
    <div>
      <h1>New tenant</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div>
      <form>
        <fieldset>
          <div id="accordion" v-if="checklist && checklist.length" role="tablist" aria-multiselectable="true">
            <div class="card" v-for="item in checklist">
              <div class="card-header" role="tab" v-bind:id="item.key">
                <h5 class="mb-0">
                  <div class="form-check">
                  <label class="form-check-label w-50">
                    <input class="form-check-input" type="checkbox" v-model="item.completed">                                        
                    {{ item.display }}
                  </label>
                  <p class="pointer float-right d-inline-block w-50 mb-0 text-right" @click="expand(item)">Expand {{ item.expanded ? '&#9660;' : '&#9658;' }}</p>                  
                </div>
                </h5>
              </div>
              <div v-bind:id="item.key" v-bind:class="{'show': item.expanded}" role="tabpanel" v-bind:aria-labelledby="item.key" class="collapse">
                <div class="card-block">
                  Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio veniam quis maxime nemo, totam omnis odio! Laudantium aspernatur eum officiis, incidunt fugit maiores quasi illo. Ab facere dolores possimus, ipsum.
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
  export default {
    name: 'newTenantMoveIn',
    data () {
      return {
        checklist: [
          {
            key: 'NTMI1',
            display: 'Ensure property is clean and tidy',
            completed: false,
            expanded: false
          },
          {
            key: 'NTMI2',
            display: 'Get tenant to sign tenancy agreement',
            completed: false,
            expanded: false
          },
          {
            key: 'NTMI3',
            display: 'Get tenant to pay monies',
            completed: false,
            expanded: false
          }
        ]
      }
    },
    methods: {
      expand: function (item) {
        this.checklist.forEach(checklistItem => {
          if (checklistItem !== item) {
            checklistItem.expanded = false
          }
        })
        item.expanded = !item.expanded
      }
    }
  }
</script>
