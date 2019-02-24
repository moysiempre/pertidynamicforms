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
<style lang="scss">

 @import "../static/scss/global.scss";
 
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
body {
  background-color: #f5f5f5;
}
.main-container {
  margin-left: 50px;
  min-height: calc(87vh - 43px);
  overflow: hidden;
  display: block;
  height: calc(87vh - 43px);
}
.btn {
  padding-top: 0.5rem;
  &.btn-dark {
    color: #fff;
    background-color: #405668;
    border-color: #405668;
  }
}

.btn-dark:not(:disabled):not(.disabled).active,
.btn-dark:not(:disabled):not(.disabled):active,
.btn-dark:not(:disabled):not(.disabled).hover,
.btn-dark:not(:disabled):not(.disabled):hover,
.show > .btn-dark.dropdown-toggle {
  color: #fff;
  background-color: #667786;
  border-color: #667786;
}


</style>
