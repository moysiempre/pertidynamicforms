<template>
  <div id="app">
    <app-header/>
    <app-sidebar/>
    <div class="main-container">
      <router-view/>
    </div>
     <vue-snotify></vue-snotify>
  </div>
</template>
<script>
import Header from "@/components/header/header.vue";
import Sidebar from "@/components/sidebar/sidebar.vue";
// import Axios from "axios"
export default {
  name: "App",
  components: {
    "app-header": Header,
    "app-sidebar": Sidebar
  },
  created: function () {
    
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
  a {
    font-weight: bold;
    color: #2c3e50;
    &.router-link-exact-active {
      color: #42b983;
    }
  }
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
