<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <FormList @changedMode="onChangedMode($event)"/>
      </div>

      <div
        class="col-12 col-sm-6 col-md-7 col-lg-5"
        v-if="fAction == 'create' || fAction == 'update'"
      >
        <FormNew/>
      </div>

      <div class="col-12 col-sm-6 col-md-7 col-lg-3">
        <!-- <FormPreview :fields = "fields"/> -->
        <!-- <pre class="language-json"><code>{{ formHd  }}</code></pre> -->

      </div>
    </div>
  </div>
</template>

<script>
import FormList from "@/components/FormList.vue";
import FormNew from "@/components/FormNew.vue";

import { mapGetters } from "vuex";

export default {
  components: {
    FormList,
    FormNew
    
  },
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      landingPages: []
    };
  },
  computed: {
    ...mapGetters(["fAction", "formHd"])
  },
  methods: {},
  beforeMount() {},
  mounted() {}
};
</script>

 
 