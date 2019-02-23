<template>
  <div id="app">
    <app-header/>
    <app-sidebar/>
    <div class="main-container">
      <router-view/>
    </div>
  </div>
</template>

<script>
import Header from "./components/header/header.vue";
import Sidebar from "./components/sidebar/sidebar.vue";

export default {
  name: "App",
  components: {
    "app-header": Header,
    "app-sidebar": Sidebar
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
body {
  background-color: #e9ecef;
}
.main-container{
  margin-left: 50px;
  min-height: calc(97vh - 43px);
  overflow-y: auto;
}
</style>
