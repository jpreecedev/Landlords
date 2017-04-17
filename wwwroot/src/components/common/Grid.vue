<template>
<div>
  <table class="table table-striped table-hover">
    <thead class="thead-inverse">
      <tr>
        <th v-if="key.display" v-for="key in columns" @click="sortBy(key)" :class="{ active: sortKey == key }">
          {{ key.display }}
          <span class="arrow" :class="sortOrders[key] > 0 ? 'asc' : 'dsc'">
          </span>
        </th>
      </tr>
    </thead>
    <tbody>
      <tr class="pointer" @click="rowClicked(entry)" v-for="entry in filteredData">
        <td v-if="key.key !== 'id'" v-for="(key, index) in columns">
          <a class="link" v-if="index === 1">{{entry[key.key]}}</a>
          <span v-if="index > 1">{{entry[key.key]}}</span>          
        </td>
      </tr>
    </tbody>
  </table>
</div>
</template>

<script>

export default {
  props: {
    data: Array,
    columns: Array
  },
  data: function () {
    var sortOrders = {}
    this.columns.forEach(function (key) {
      sortOrders[key] = 1
    })
    return {
      sortKey: '',
      sortOrders: sortOrders
    }
  },
  computed: {
    filteredData: function () {
      var sortKey = this.sortKey
      var filterKey = this.filterKey && this.filterKey.toLowerCase()
      var order = this.sortOrders[sortKey] || 1
      var data = this.data
      if (filterKey) {
        data = data.filter(function (row) {
          return Object.keys(row).some(function (key) {
            return String(row[key]).toLowerCase().indexOf(filterKey) > -1
          })
        })
      }
      if (sortKey) {
        data = data.slice().sort(function (a, b) {
          a = a[sortKey]
          b = b[sortKey]
          return (a === b ? 0 : a > b ? 1 : -1) * order
        })
      }
      return data
    }
  },
  filters: {
    capitalize: function (str) {
      return str.charAt(0).toUpperCase() + str.slice(1)
    }
  },
  methods: {
    sortBy: function (key) {
      this.sortKey = key
      this.sortOrders[key] = this.sortOrders[key] * -1
    },
    rowClicked: function (entry) {
      this.$emit('rowclick', entry)
    }
  }
}

</script>

<style lang="scss" scoped>

.arrow {
	display: inline-block;
	vertical-align: middle;
	width: 0;
	height: 0;
	margin-left: 5px;
	opacity: 0.66;
	&.asc {
		border-left: 4px solid transparent;
		border-right: 4px solid transparent;
		border-bottom: 4px solid #fff;
	}
	&.dsc {
		border-left: 4px solid transparent;
		border-right: 4px solid transparent;
		border-top: 4px solid #fff;
	}
}
</style>
