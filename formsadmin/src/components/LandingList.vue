<template>
  <div class="card border-light helix">
    <div class="card-header">
      <h5 class="mt-2">LANDING PAGES {{lpAction}}</h5>
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
        style="height: 200px;overflow: hidden;overflow-y: auto;"
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
          <div class="d-flex align-items-center c-pointer">
            <div class="btn-group">
              <button
                type="button"
                class="btn btn-link btn-sm"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <i class="pe-7s-more"></i>
              </button>
              <div class="dropdown-menu dropdown-menu-right">
                <button class="dropdown-item" type="button">Editar</button>
                <button class="dropdown-item" type="button">Seleccionar</button>
                <button class="dropdown-item" type="button">ver detalle</button>
              </div>
            </div>
          </div>
        </li>
      </ul>
      <div class="alert alert-warning" v-if="!landingPages">no hay registro</div>
    </div>
  </div>
</template>
<script>
import { mapState } from "vuex";
export default {
  mounted() {
    this.$store.dispatch("getLandingPages");
  },
  computed: {
    ...mapState(["landingPages", "lpAction"])
  },
  methods: {
    setAction(action, item) {
      this.$store.state.landingPage = item;
      this.$store.state.lpAction = action;
    }
  }
};
</script>

