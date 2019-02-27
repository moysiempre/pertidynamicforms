<template>
  <div class="wrapper">
    <form id="search" class="row">
      <label for class="col-sm-8 col-form-label text-right">Search</label>
      <div class="col-sm-4">
        <input name="query" v-model="searchQuery" class="form-control">
      </div>
    </form>

    <div id="grid-template ">
      <div class="table-header-wrapper">
        <table class="table-header bg-white">
          <thead>
            <tr >
              <th
                v-for="col in columns"
                @click="sortBy(col)"
                :class="{ active: sortKey == col }"
                :key="col"
              >
                {{ col | capitalize }}
                <span
                  class="arrow"
                  :class="sortOrders[col] > 0 ? 'asc' : 'dsc'"
                ></span>
              </th>
            </tr>
          </thead>
        </table>
      </div>
      <div class="table-body-wrapper bg-white">
        <table class="table-body bg-white">
          <tbody>
            <tr v-for="(entry, index) in filteredData" :key="index">
              <td v-for="col in columns" :key="col">{{entry[col]}}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "grid",
  props: {
    data: Array,
    columns: Array
  },
  data() {
    return {
      searchQuery: "",
      sortKey: "",
      sortOrders: {}
    };
  },
  computed: {
    filteredData: function() {
      let sortKey = this.sortKey;
      let filterKey = this.searchQuery && this.searchQuery.toLowerCase();
      let order = this.sortOrders[sortKey] || 1;
      let data = this.data;

      if (filterKey) {
        data = data.filter(function(row) {
          return Object.keys(row).some(function(key) {
            return (
              String(row[key])
                .toLowerCase()
                .indexOf(filterKey) > -1
            );
          });
        });
      }
      if (sortKey) {
        data = data.slice().sort(function(a, b) {
          a = a[sortKey];
          b = b[sortKey];
          return (a === b ? 0 : a > b ? 1 : -1) * order;
        });
      }
      return data;
    }
  },
  filters: {
    capitalize: function(str) {
      return str.charAt(0).toUpperCase() + str.slice(1);
    }
  },
  methods: {
    sortBy: function(key) {
      this.sortKey = key;
      this.sortOrders[key] = this.sortOrders[key] * -1;
    }
  },
  created() {
    let sortOrders = {};
    this.columns.forEach(function(key) {
      sortOrders[key] = 1;
    });
    this.sortOrders = sortOrders;
  }
};
</script>
<style lang="scss">
table {
  border-spacing: 0;
  width: 100%;
}

th {
  background-color: #fff;
  color: #000;
  cursor: pointer;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

td, th {
  border-bottom: 1px #ddd solid;
}

th,
td {
  min-width: 150px;
  padding: 10px 20px;
}

th.active {
  color: #000;
}

th.active .arrow {
  opacity: 1;
}

.arrow {
  display: inline-block;
  vertical-align: middle;
  width: 0;
  height: 0;
  margin-left: 5px;
  opacity: 0.66;
}

.arrow.asc {
  border-left: 4px solid transparent;
  border-right: 4px solid transparent;
  border-bottom: 4px solid #fae042;
}

.arrow.dsc {
  border-left: 4px solid transparent;
  border-right: 4px solid transparent;
  border-top: 4px solid #fae042;
}

#grid-template {
  display: flex;
  display: -webkit-flex;
  flex-direction: column;
  -webkit-flex-direction: column;
  width: 100%;
}
</style>
