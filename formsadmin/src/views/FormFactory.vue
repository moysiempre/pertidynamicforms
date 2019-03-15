<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <FormList @changedMode="onChangedMode($event)"/>
      </div>

      <div 
        class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0"
        v-if="fhAction == 'create' || fhAction == 'update'" 
      >
        <FormNew/>
      </div>

      <div class="col-12 col-sm-6 col-md-7 col-lg-8 mt-3 mt-sm-0" v-if="fhAction == 'detail'">
        <div class="row">
          <div class="col-md-8">
            <FormItemNew/>           
          </div>

          <div class="col-md-4">
            <form-generator/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import FormGenerator from "@/components/forms/FormGenerator";
import FormNew from "@/components/FormNew.vue";
import FormItemNew from "@/components/FormItemNew.vue";
import FormList from "@/components/FormList.vue";
import { mapState } from "vuex";

export default {
  components: {
    FormList,
    FormNew,
    FormItemNew,
    FormGenerator
  },
  data() {
    return {
      msg: "Welcome to Your Vue.js App"
    };
  },
  computed: {
    ...mapState(["fhAction", "formHd"])
  },
  methods: {},
  beforeMount() {},
  mounted() {}
};
</script>

 
 