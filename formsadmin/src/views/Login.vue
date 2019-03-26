<template>
  <div class="container-fluid">
    <div class="card login">
      <h5 class="text-center font-weight-normal mb-3">LOGIN</h5>
      <div class="card-body">
        <form @submit.prevent="onSubmit">
          <div class="form-group">
            <input
              type="email"
              id="userName"
              class="form-control"
              placeholder="Digite su correo electrónico"
              v-model="userName"
            >
          </div>
          <div class="form-group">
            <input
              type="password"
              id="password"
              class="form-control"
              placeholder="digite su contraseña"
              v-model="password"
            >
          </div>

          <div class="form-group">
            <button type="submit" class="btn btn-primary w-100" :disabled="isloading">
              <span>INICIA SESIÓN</span>
              <btn-loader :isloading="isloading"/>
            </button>
          </div>
          <div class="form-group">
            <p class="mb-0">
              <a href="#">¿Has olvidado tu contraseña?</a>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import BtnLoader from "@/components/BtnLoader.vue";
export default {
  name: "login",
  components: { BtnLoader },
  data() {
    return {
      userName: "",
      password: "",
      isloading: false
    };
  },
  created() {
    this.$store.commit("SET_FULL_LAYOUT", false);
  },
  mounted() {},
  computed: {},
  methods: {
    onSubmit: function() {
      this.isloading = true;
      let userName = this.userName;
      let password = this.password;
      this.$store
        .dispatch("login", { userName, password })
        .then(response => {
          this.isloading = false;
          if (response.data.success === true) {
            this.$store.commit("SET_FULL_LAYOUT", true);
            this.$router.push({ name: "formularios" });
          } else {            
            this.$swal(response.data.message, {
              icon: "warning"
            });
          }
        })
        .catch(err => {
          this.isloading = false;
          this.$swal("No se pudo logear, favor valide su conexión a internet, o comuniquese con el admistrador", {
            icon: "error"
          });
          console.log("err-login", err);
        });
    }
  }
};
</script>