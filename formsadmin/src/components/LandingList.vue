<template>
  <div class="card border-light helix">
    <div class="card-header">
      <h5 class="mt-2">LANDING PAGES</h5>
      <button class="btn btn-primary" @click="createLandingPage">+</button>
    </div>
    <div class="card-body p-0">
      <ul class="list-group" v-if="landings">
        <li class="list-group-item" v-for="item in landings" :key="item.id" @click="updateLandingPage(item)">
          <h6 class="mb-0">{{item.name}}</h6>
          <p class="mb-0" style="color:#75818b">{{item.description}}</p>
          <p class="mb-0 small text-muted" hidden>
            <small style="color:#a3abb2">very good always</small>
          </p>
        </li>
      </ul>
      <div class="alert alert-warning" v-if="!landings">no hay registro</div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["landings"],
  data() {
    return {
      mode: "read"
    };
  },
  methods: {
    createLandingPage() {
      this.$emit("changedMode", {});
    },
    updateLandingPage(item) {
      this.$emit("changedMode", item);
    }
  }
};
</script>

