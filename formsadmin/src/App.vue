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
import Header from "@/components/header/header.vue";
import Sidebar from "@/components/sidebar/sidebar.vue";
import Axios from "axios"
export default {
  name: "App",
  components: {
    "app-header": Header,
    "app-sidebar": Sidebar
  },
  created: function () {
    Axios.interceptors.response.use(undefined, function (err) {
      return new Promise(function (resolve, reject) {
        if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
          this.$store.dispatch(logout)
          console.log("from interceptors IF FOR [logout]");
        } else{
          console.log("from interceptors ELSE");
        }
        throw err;
      });
    });
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
  overflow-y: auto;
  position: relative;
}
</style>
