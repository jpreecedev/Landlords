<template>
  <div>
    <header>
      <h1 class="headline primary--text">Landlord list</h1>
      <p class="display-2 grey--text text--darken-1">All landlords managed by your agency</p>
    </header>
    <v-card>
      <v-data-table :headers="headers" :items="data" :loading="isLoading">
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
      isLoading: false,
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
  mounted () {
    this.isLoading = true
    this.$http.get('/api/landlord')
      .then(response => {
        this.data = response.data
      })
      .finally(() => {
        this.isLoading = false
      })
  }
}
</script>

<style lang="scss" scoped>

</style>
