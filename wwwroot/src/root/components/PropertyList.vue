<template>
  <div>
    <header>
      <h1 class="headline primary--text">Your properties</h1>
      <p class="display-2 grey--text text--darken-1">There are no notifications or upcoming events</p>
      <p class="subheading">Click on the property reference for details.</p>
    </header>

    <v-card>
      <v-data-table :headers="headers" :items="data" :loading="loading">
        <template slot="headers" scope="props">
          <tr>
            <th v-for="(header, index) in props.headers" :key="index" class="text-xs-left">
              {{ header.text }}
            </th>
          </tr>
        </template>
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
          <td>
            <span v-if="!props.item.notifications || props.item.notifications.length === 0">No notifications</span>
            <ul class="notifications" v-else>
              <li v-for="notification in props.item.notifications" :key="notification.id">
                <notification-icon :type="notification.type"></notification-icon>
                {{ notification.message }}
              </li>
            </ul>
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>

    <v-btn primary v-if="permissions.PD_New" type="button" class="mt-4 no-left-margin" @click="addProperty()" :loading="isAddingProperty">Add a property</v-btn>

  </div>
</template>

<script>
export default {
  name: 'propertylist',
  data () {
    return {
      loading: false,
      isAddingProperty: false,
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
        }, {
          text: 'Notifications',
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
    this.$http.get('/api/propertydetails')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.loading = false
      })
  },
  methods: {
    addProperty () {
      this.isAddingProperty = true
      this.$http.post('/api/propertydetails/new')
        .then(response => {
          this.$router.push(`/manager/property-details/${response.data.id}`)
        })
        .finally(() => {
          this.isAddingProperty = false
        })
    }
  }
}
</script>

<style lang="scss" scoped>

  .notifications {
    list-style: none;
    padding-left: 0;
    margin: 1rem 0;

    li {
      margin-top: 0.75rem;
      margin-bottom: 0.75rem;
    }
  }

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
