<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-5 col-md-4 col-lg-3">
        <landing-list/>
      </div>
      <div class="col-12 col-sm-7 col-md-6 col-lg-5 mt-3 mt-sm-0">
        <LandingNew/>
      </div>
    </div>
  </div>
</template>
<script>
 
import LandingList from "@/components/LandingList.vue";
import LandingNew from "@/components/LandingNew.vue";

export default {
  components: { LandingList,LandingNew },
  data() {
    return {
      title: "ALTA LANDING PAGE"
    };
  }
};
</script>
<style lang="scss">
</style>


