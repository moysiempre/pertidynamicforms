<template>
  <div id="app" v-cloak>
    <app-header v-if="showFullLayout"></app-header>
    <app-sidebar v-if="showFullLayout"></app-sidebar>
    <div class="main-container">
      <router-view></router-view>
    </div>
  </div>
</template>
<script>
import Header from '@/components/header/header.vue'
import Sidebar from '@/components/sidebar/sidebar.vue'
import { mapState } from 'vuex'

export default {
  name: 'App',
  components: {
    'app-header': Header,
    'app-sidebar': Sidebar
  },
  computed: {
    ...mapState(['showFullLayout'])
  }
}
</script>
<style lang="scss">
@import './assets/scss/global.scss';
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
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
