<template>
  <nav class="navbar navbar-expand navbar-light bg-lights">
    <router-link to="/login" class="brand">
      <img src="../../assets/logo-pertis.png" alt>
      <span>FORMULAD GP</span>
    </router-link>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <div class="navbar-nav ml-auto btn-group">
        <button
          type="button"
          class="btn"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="dropdown-menu dropdown-menu-right">
          <div class="menu-box">
            <button class="btn btn-link" type="button">
              <router-link to="/landing">
                <img src="../../assets/landings.png" alt style="width:35px">
                <small class="text-muted">Landing Pages</small>
              </router-link>
            </button>
            <button class="btn btn-link" type="button">
              <router-link to="/formularios">
                <img src="../../assets/forms.png" alt style="width:35px">
                <small class="text-muted">Formularios</small>
              </router-link>
            </button>
            <button class="btn btn-link" type="button">
              <router-link to="/solicitudes">
                <img src="../../assets/requests.png" alt style="width:35px">
                <small class="text-muted">Solicitudes</small>
              </router-link>
            </button>
          </div>
        </div>
      </div>
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
  .brand {
    color: inherit;
    &:hover {
      color: #405668;
      text-decoration: none;
    }
    img {
      width: 35px;
    }
    span {
      margin-left: 15px;
    }
  }
  .bg-xlight {
    background-color: #f5f5f5;
  }
  .menu-box {
    display: flex;
    flex-direction: row;
    padding: 10px;
    a {
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 10px;
      border: 1px solid #ddd;
      border-radius: 10px;
      height: 100px;
      width: 110px;
      justify-content: center;
      background-color: #fff;
      small {
        margin-top: 5px;
      }
      &:focus,
      &:hover,
      &:link {
        background-color: #f5f5f5;
        text-decoration: none;
        color: transparent;
      }
    }
  }
}
</style>


