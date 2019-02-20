<template>
  <div class="container">
    <div class="login">
      <div class="inner-wrapper">
        <form id="loginForm" class="needs-validation" novalidate>
          <h1 class="text-center mb-4">LOGIN</h1>
          <div class="form-group">
            <input
              type="email"
              class="form-control"
              placeholder="dirección de correo electrónico"
              maxlength="256"
              required
            >
            <div
              class="invalid-feedback mt-0 text-left"
            >Introduce una dirección de correo electrónico válida.</div>
          </div>
          <div class="form-group">
            <input
              type="password"
              class="form-control"
              placeholder="contraseña"
              minlength="6"
              maxlength="20"
              required
            >
            <div class="invalid-feedback mt-0 text-left">
              Introduce una contraseña valida.
              <i>(ej: Amor&1850)</i>
            </div>
          </div>
          <div class="form-group">
            <button class="btn btn-info w-100" @click="navigateToHome">Inicia Sesión</button>
          </div>
          <div class="form-group">
            <p class="mb-0">
              <a href="#">¿Has olvidado tu contraseña?</a>
            </p>
            <p class="mb-1">
              <span>¿Todavía no eres miembro?</span>
              <a href="#">Únete ahora</a>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome to Your Vue.js App"
    };
  },
  methods: {
    navigateToHome() {
      this.$router.push({ name: "formsFactory" });
    }
  }
};
</script>
<style lang="css">
.login {
  border: none;
  text-align: center;
  background: transparent;
  position: fixed;
  top: 50%;
  left: 50%;
  margin-top: -180px;
  margin-left: -227.5px;
  padding: 0;
  width: 455px;
}
.login .inner-wrapper {
  background: rgba(255, 255, 255, 0.7);
  padding: 30px 40px;
}
</style>
