<template>
  <nav class="navbar navbar-expand navbar-light bg-lights">
    <router-link to="/login" class="brand">
      <img src="../../assets/logo-pertis.png" alt />
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
        <div id="bigmenu" class="dropdown-menu dropdown-menu-right">
          <div class="menu-box">
            <div class="row no-gutters">
              <div class="col-12 col-sm-3">
                <button class="btn btn-link" type="button">
                  <router-link to="/landings">
                    <img
                      src="../../assets/landings.png"
                      alt
                      style="width:35px"
                    />
                    <small class="text-muted">Landing Pages</small>
                  </router-link>
                </button>
              </div>
              <div class="col-12 col-sm-3">
                <button class="btn btn-link" type="button">
                  <router-link to="/formularios">
                    <img src="../../assets/forms.png" alt style="width:35px" />
                    <small class="text-muted">Formularios</small>
                  </router-link>
                </button>
              </div>
              <div class="col-12 col-sm-3">
                <button class="btn btn-link" type="button">
                  <router-link to="/solicitudes">
                    <img
                      src="../../assets/requests.png"
                      alt
                      style="width:35px"
                    />
                    <small class="text-muted">Solicitudes</small>
                  </router-link>
                </button>
              </div>
              <div class="col-12 col-sm-3">
                <button class="btn btn-link" type="button">
                  <router-link to="/mailTemplate">
                    <img
                      src="../../assets/email_setting.png"
                      alt
                      style="width:47px"
                    />
                    <small class="text-muted">E-Mail Plantillas</small>
                  </router-link>
                </button>
              </div>
            </div>
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
      title: 'Navbar text with an inline element'
    }
  }
}
</script>
<style lang="scss" scooped>
#bigmenu {
  &.dropdown-menu {
    width: auto !important;
  }
}
@media (min-width: 576px) {
  #bigmenu {
    &.dropdown-menu {
      width: 500px !important;
    }
  }
}
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

  .dropdown-menu {
    background-color: #fff;
    box-shadow: 0 0 15px 1px rgba(69, 65, 78, 0.2);
    border: 1px solid rgba(25, 118, 210, 0.15) !important;
    -webkit-box-shadow: 0 1px 15px 1px rgba(76, 15, 218, 0.06) !important;
    -moz-box-shadow: 0 1px 15px 1px rgba(76, 15, 218, 0.06) !important;
    box-shadow: 0 1px 15px 1px rgba(76, 15, 218, 0.06) !important;

    &:before {
      position: absolute;
      top: -7px;
      right: 9px;
      display: inline-block !important;
      border-right: 7px solid transparent;
      border-bottom: 7px solid #ccc;
      border-left: 7px solid transparent;
      border-bottom-color: rgba(0, 0, 0, 0.2);
      content: '';
    }
    &:after {
      position: absolute;
      top: -6px;
      right: 10px;
      display: inline-block !important;
      border-right: 6px solid transparent;
      border-bottom: 6px solid #fff;
      border-left: 6px solid transparent;
      content: '';
    }
  }
}
</style>
