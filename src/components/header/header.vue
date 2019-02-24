<template>
  <nav class="navbar navbar-expand-lg">
    <a class="navbar-brand" href="#">Navbar w/ text</a>
    <!-- <img src="../../assets/logo-perti.png" class="logo" alt="logo"> -->
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav ml-auto">
        <li class="nav-item active">
           <router-link to="/landingNew">INICIO</router-link>
        </li>
        <li> | </li>
        <li class="nav-item active">
          <router-link to="/">LOGIN</router-link>          
        </li>
         <li> | </li>
        <li class="nav-item active">
          <router-link to="/register">REGISTER</router-link>          
        </li>
        <li> | </li>
        <li class="nav-item active">
          <router-link to="/formsFactory">FORMULARIOS</router-link>          
        </li>
      </ul>
    </div>
  </nav>
</template>
<script>
export default {
  data() {
    return {
      title: "Navbar text with an inline element"
    };
  }
};
</script>
<style lang="scss">
nav {
  .logo {
    width: 120px;
    padding-left: 20px;
  }
}
</style>


