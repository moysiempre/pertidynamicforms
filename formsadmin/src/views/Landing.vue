<template>
  <div class="container-fluid h-100 landing">
    
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <landing-list />
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0" v-if="lpAction !== 'read'">
        <LandingNew :title="title" @onCancel="onCancel"/>
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
      title: "ALTA LANDING PAGE"     
    };
  },
   computed: {
    ...mapState(["lpAction"])
  },
  methods: {
    onItemSaved() {
      
    },
    onChangedMode(data) {
      
    },
    onCancel() {
       
    },
    validateMode() {
      
    }
  }, 
  
};
</script>
<style lang="scss">
</style>


