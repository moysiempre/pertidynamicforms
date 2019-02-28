<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link>|
      <router-link to="/login">Login</router-link>|
      <router-link to="/landing">Landing</router-link>|
      <router-link to="/formularios">Formularios</router-link>|
      <router-link to="/solicitudes">Solicitudes</router-link>|
      <router-link to="/about">About</router-link>|
      <router-link to="/changepassword">ChangePassword</router-link>
    </div>
    <app-sidebar/>
    <div class="main-container">
      <router-view/>
    </div>
  </div>
</template>
<script>
// import Header from "./components/header/header.vue";
import Sidebar from "./components/sidebar/sidebar.vue";

export default {
  name: "App",
  components: {
    "app-sidebar": Sidebar
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
  min-height: calc(87vh - 43px);
  overflow: hidden;
  display: block;
  height: calc(87vh - 43px);
  overflow-y: auto
}
</style>
