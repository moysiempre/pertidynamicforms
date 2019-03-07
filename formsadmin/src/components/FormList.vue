<template>
  <div class="card border-light helix">
    <div class="card-header">
      <h5 class="mt-2">FORMULARIOS</h5>
      <button class="btn btn-primary" @click="createFormHd">+</button>
    </div>
    <div class="card-body p-0">
      <ul class="list-group" v-if="formHds">
        <li class="list-group-item" v-for="item in formHds" :key="item.id" @click="updateFormHd(item)">
          <h6 class="mb-0">{{item.title}}</h6>
          <p class="mb-0" style="color:#75818b">{{item.description}}</p>
          <p class="mb-0 small text-muted" hidden>
            <small style="color:#a3abb2">very good always</small>
          </p>
        </li>
      </ul>
      <div class="alert alert-warning" v-if="!formHds">no hay registro</div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["formHds"],
  data() {
    return {
      mode: "read"
    };
  },
  methods: {
    createFormHd() {
      this.$emit("changedMode", {});
    },
    updateFormHd(item) {
      this.$emit("changedMode", item);
    }
  }
};
</script>

