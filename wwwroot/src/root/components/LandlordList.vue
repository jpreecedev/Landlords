<template>
  <div>
    <h1 class="display-2">Landlord List</h1>
    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>

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
            {{ props.item.landlordName }}
          </td>
          <td>
            {{ props.item.mainPhoneNumber }} <span v-if="props.item.secondaryPhoneNumber">or {{ props.item.secondaryPhoneNumber }}</span>
          </td>
          <td>
            {{ props.item.properties }}
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
  name: 'landlordlist',
  data () {
    return {
      loading: false,
      pagination: {},
      headers: [
        {
          text: 'Landlord Name',
          left: true,
          sortable: false
        },
        {
          text: 'Phone Number',
          left: true,
          sortable: false
        },
        {
          text: '# of Properties',
          left: true,
          sortable: false
        }
      ],
      data: []
    }
  },
  created () {
    this.loading = true
    this.$http.get('/api/landlord').then(response => {
      this.loading = false
      this.data = response.data
    })
  }
}
</script>

<style lang="scss" scoped>

</style>
