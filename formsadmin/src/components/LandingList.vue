<template>
  <div>
    <div class="card border-light helix">
      <div class="card-header">
        <h6 class="mt-2">LANDING PAGES</h6>
        <a class="btn btn-link" @click="setAction('create', {})">
          <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
          <br>
        </a>
      </div>
      <div class="card-body p-0">
        <div class="input-group search-by mb-0">
          <input
            type="text"
            class="form-control"
            v-model="searchby"
            placeholder="filtrar por landing page"
          >
          <div class="input-group-append">
            <span class="input-group-text" id="basic-addon2">
              <i class="pe-7s-search"></i>
            </span>
          </div>
        </div>
        <ul
          class="list-group"
          v-if="landingPages"
          style="height: 100%;overflow: hidden;overflow-y: auto;"
        >
          <li
            class="list-group-item d-flex justify-content-between"
            v-for="item in filterSearch "
            :key="item.id"  @click="setAction('update', item)"
          >
            <div>
              <h6 class="mb-0">{{item.name}}</h6>
              <p class="mb-0" style="color:#75818b">
                <small>{{item.description}}</small>
              </p>
            </div>
          </li>
        </ul>
        <div class="alert alert-warning" v-if="!landingPages">no hay registro</div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import { mapState } from "vuex";
export default {
  data() {
    return {
      searchby: ""
    };
  },
  mounted() {
    this.load();
  },
  computed: {
    ...mapState(["landingPages"]),
    filterSearch() {
      return this.$store.state.landingPages.filter(item => {
        return (
          !this.searchby ||
          item.name.toLowerCase().indexOf(this.searchby.toLowerCase()) > -1
        );
      });
    }
  },
  methods: {
     load() {
      axios.get("api-landingpage").then(response => {
        this.$store.state.landingPages = response.data
      });
    },
    setAction(action, item) {
      if(action == "create"){
        item.isActive = true;
      }
      this.$store.state.landingPage = item;
      this.$store.state.lpAction = action;
    }   
  }
};
</script>
<style lang="scss">

</style>

