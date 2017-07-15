<template>
  <div>
    <h1 class="display-2">Property List</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

    <v-card>
      <v-data-table :headers="headers" :items="data" :loading="loading">
        <template slot="items" scope="props">
          <td>
            <div class="thumbnail">
              <img class="mt-2 mb-2" v-if="props.item.leadImage" :src="'/static/uploads/' + props.item.portfolioId + '/' + props.item.leadImage.fileName" :alt="props.item.leadImage.fileName">
            </div>
          </td>
          <td>
            <router-link v-if="permissions.PD_View" :to="'/manager/property-details/' + props.item.id">{{ props.item.reference }}</router-link>
            <span v-else>{{ props.item.reference }}</span>
          </td>
          <td>
              {{ props.item.propertyStreetAddress }}
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>

    <v-btn primary v-if="permissions.PD_New" type="button" class="mt-4" @click.native="addProperty()">Add a property</v-btn>

  </div>
</template>

<script>
export default {
  name: 'propertylist',
  data () {
    return {
      loading: false,
      pagination: {},
      headers: [
        {
          text: '',
          left: true,
          sortable: false
        }, {
          text: 'Reference',
          left: true,
          sortable: false
        }, {
          text: 'Street Address',
          left: true,
          sortable: false
        }
      ],
      permissions: this.$store.state.permissions,
      data: []
    }
  },
  created () {
    this.loading = true
    this.$http.get('/api/propertydetails').then(response => {
      this.loading = false
      this.data = response.data
    })
  },
  methods: {
    addProperty () {
      this.$http.post('/api/propertydetails/new').then(response => {
        this.$router.push(`/manager/property-details/${response.data.id}`)
      })
    }
  }
}
</script>

<style lang="scss" scoped>

  .thumbnail img {
    max-height: 150px;
    max-width: 150px;

    &:hover {
      opacity: 1;
    }
  }

  table.datatable.table tbody tr {
    &:hover {
      .thumbnail img {
        border: 1px solid #ccc;
      }
    }
    td:first-child {
      width: 200px;
    }
  }

</style>
