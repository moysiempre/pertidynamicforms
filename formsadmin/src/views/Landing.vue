<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <landing-list :landings="landings" @changedMode="onChangedMode($event)"/>
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0" v-if="validateMode() == true">
        <LandingNew :title="title" :landing="landing" @onSaved="onItemSaved" @onCancel="onCancel"/>
      </div>
    </div>
  </div>
</template>
<script>
import LandingList from "@/components/LandingList.vue";
import LandingNew from "@/components/LandingNew.vue";

export default {
  components: { LandingList, LandingNew },
  data() {
    return {
      title: "ALTA LANDING PAGE",
      landings: [],
      landing: {},
      mode: "read"
    };
  },
  beforeMount() {
    this.fetchData();
  },
  mounted() {
    console.log("mounted");
  },
  methods: {
    fetchData() {
      this.$http.get("http://localhost:64423/landing/api-landingpage").then(
        response => {
          this.landings = response.body;
        },
        error => {
          console.log(error);
        }
      );
    },
    onItemSaved() {
      this.mode = "read";
      this.fetchData();
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
  created() {
    console.log("created");
  }
};
</script>
<style lang="scss">
</style>


