<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <landing-list @action="onAssign"/>
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0" v-if="action !== 'read'">
        <LandingNew
          :title="title"
          :landingItem="landingItem"
          @onCancel="onCancel"
          @onItemSaved="onItemSaved"
        />
      </div>
    </div>
  </div>
</template>
<script>
import LandingList from "@/components/LandingList.vue";
import CreateUpdateLanding from "@/components/CreateUpdateLanding.vue";
export default {
  components: { "landing-list": LandingList, CreateUpdateLanding },
  data() {
    return {
      title: "ALTA LANDING PAGE",
      action: "read",
      landingItem: {}
    };
  },
  methods: {
    onAssign(item) {
      console.log("item?", item);
      this.action = (item && item.id) ? "update" : "create";
      this.landingItem = item;
    },
    onItemSaved(item) {
      if (this.action == "create") {
        this.$store.commit("addLandingPage", item);
      }
      else{
          console.log("create?", this.action);
      }
      this.onCancel();
    },
    onCancel() {
      this.action = "read";
    }
  }
};
</script>
 
