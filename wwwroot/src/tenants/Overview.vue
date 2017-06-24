<template>
  <div>
    <h1 class="display-2">Your Tenants</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    <v-card>
      <v-data-table :headers="headers" :items="data">
        <template slot="items" scope="props">
          <td>
            <router-link v-if="permissions.TE_GetById" :to="'/tenants/details/' + props.item.id">
              <span>{{ props.item.firstName + ' ' + props.item.lastName }}</span>
            </router-link>
            <span v-else>{{ props.item.firstName + ' ' + props.item.lastName }}</span>
          </td>
          <td>
            {{ props.item.mainContactNumber }} <span v-if="props.item.secondaryContactNumber">or {{ props.item.secondaryContactNumber }}</span>
          </td>
          <td>
            {{ props.item.emailAddress }}
          </td>
        </template>
        <template slot="pageText" scope="{ pageStart, pageStop }">
          From {{ pageStart }} to {{ pageStop }}
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
export default {
  name: 'tenants-overview',
  data () {
    return {
      pagination: {},
      headers: [
        {
          text: 'Name',
          left: true,
          sortable: false
        },
        {
          text: 'Contact Number',
          left: true,
          sortable: false
        },
        {
          text: 'Email Address',
          left: true,
          sortable: false
        }
      ],
      permissions: this.$store.state.permissions,
      data: []
    }
  },
  created () {
    this.$http.get('/api/tenants').then(response => {
      this.data = response.data
    })
  }
}
</script>

<style lang="scss" scoped>

</style>
