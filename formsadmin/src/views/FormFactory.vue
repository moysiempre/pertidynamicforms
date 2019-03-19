<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-3">
        <FormList @changedMode="onChangedMode($event)"/>
      </div>

      <div
        class="col-12 col-sm-6 col-md-7 col-lg-6"
        v-if="fhAction == 'create' || fhAction == 'update'"
      >
        <FormNew/>
      </div>

      <div class="col-12 col-sm-6 col-md-7 col-lg-3">
        <!-- <pre class="language-json"><code>{{ formHd  }}</code></pre> -->
      </div>
    </div>
  </div>
</template>

<script>
import FormList from "@/components/FormList.vue";
import FormNew from "@/components/FormNew.vue";
import { mapState } from "vuex";

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
    ...mapState(["fhAction", "formHd"])
  },
  methods: {},
  beforeMount() {},
  mounted() {}
};
</script>

 
 