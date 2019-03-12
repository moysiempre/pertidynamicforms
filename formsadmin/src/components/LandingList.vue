<template>
  <div class="card border-light helix">
    <div class="card-header">
      <h5 class="mt-2">LANDING PAGES</h5>
      <button class="btn btn-primary" hidden @click="setAction('create', {})">+</button>
    </div>
    <div class="card-body p-0">
      <div class="input-group mb-0">
        <input type="text" class="form-control" placeholder="filtrar por landing page">
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
          v-for="item in landingPages"
          :key="item.id"
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
</template>
<script>
import axios from "axios";
import { mapState } from "vuex";
export default {
  data() {
    return {
      landingPages: []
    };
  },
  mounted() {
    this.load();
  },
  computed: {
    ...mapState(["lpAction"])
  },
  methods: {
    setAction(action, item) {
      this.$store.state.landingPage = item;
      this.$store.state.lpAction = action;
    },
    load() {
      axios.get("api-landingpage").then(response => {
        console.log(response.data);
        this.landingPages = response.data
      });
    }
  }
};
</script>

