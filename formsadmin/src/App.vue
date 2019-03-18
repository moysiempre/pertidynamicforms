<template>
  <div id="app" v-cloak>
    <app-header v-if="withSidebar"/>
    <app-sidebar v-if="withSidebar"/>
    <div class="main-container">
      <router-view/>
    </div>
    <vue-snotify></vue-snotify>
  </div>
</template>
<script>
import Header from "@/components/header/header.vue";
import Sidebar from "@/components/sidebar/sidebar.vue";
import axios from "axios";
export default {
  name: "App",
  data() {
    return {
      withSidebar: true
    };
  },
  components: {
    "app-header": Header,
    "app-sidebar": Sidebar
  },
  created: function() {},
  mounted() {
    console.log("ASIDE mounted", this.$route.query);
    this.load();
  },
  methods: {
    load() {
      axios.get("api-forms/basedetail").then(response => {
        this.$store.state.baseDetails = response.data;
        console.log('baseDetails: ', response.data)
      });
    }
  },
  watch: {
    $route(to, from) {
      this.withSidebar = to.path === "/login" ? false : true;
    }
  }
};
</script>
<style lang="scss">
@import "./assets/scss/global.scss";
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  border: 0px solid red;
  height: 100%;
}
#nav {
  padding: 30px;
}
.main-container {
  margin-left: 50px;
  min-height: calc(87vh - 50px);
  overflow: hidden;
  display: block;
  height: calc(98.2vh - 50px);
  overflow-y: auto;
  position: relative;
}
</style>
