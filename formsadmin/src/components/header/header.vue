<template>
   <nav class="navbar navbar-expand-md navbar-light">
  <a class="navbar-brand" href="#">Navbar w/ text</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarText">
    <ul class="navbar-nav ml-auto">
      <li class="nav-item active">
        <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Features</a>
      </li>
      <li class="nav-item d-block d-md-none ">
        <a class="nav-link" href="#">Pricing MX </a>
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
  .bg-xlight{
    background-color: #F5F5F5
  }
}
</style>


