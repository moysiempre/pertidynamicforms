<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <landing-list />
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0" v-if="lpAction !== 'read'">
        <LandingNew :title="title" :landing="landing" @onSaved="onItemSaved" @onCancel="onCancel"/>
      </div>
    </div>
  </div>
</template>
<script>
import LandingList from "@/components/LandingList.vue";
import LandingNew from "@/components/LandingNew.vue";
import { mapState } from "vuex";
export default {
  components: { 'landing-list': LandingList, LandingNew },
  data() {
    return {
      title: "ALTA LANDING PAGE",
      landings: [],
      landing: {},
      mode: "read"
    };
  },
   computed: {
    ...mapState(["lpAction"])
  },
  methods: {
    onItemSaved() {
      this.mode = "read";
      console.log("onItemSaved");
    },
    onChangedMode(data) {
      if (data && data.id) {
        this.mode = "update";
        console.log("changedMode", data.id);
      } else {
        this.mode = "create";
      }

      console.log("new mode is: " + data);
    },
    onCancel() {
      this.mode = "read";
    },
    validateMode() {
      return this.mode == "create" || this.mode == "update" ? true : false;
    }
  }, 
  
};
</script>
<style lang="scss">
</style>


