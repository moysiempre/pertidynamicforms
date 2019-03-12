<template>
  <div class="container-fluid">
    <div class="card login">
      <h5 class="text-center font-weight-normal mb-3">LOGIN</h5>
      <div class="card-body">
        <form @submit.prevent="onSubmit">
          <div class="form-group" :class="{invalid: $v.userName.$error}">
            <input
              type="email"
              id="userName"
              class="form-control"
              placeholder="Digite su correo electrónico"
              @blur="$v.userName.$touch()"
              v-model="userName"
            >
          </div>
          <div class="form-group" :class="{invalid: $v.password.$error}">
            <input
              type="password"
              id="password"
              class="form-control"
              placeholder="digite su contraseña"
              @blur="$v.password.$touch()"
              v-model="password"
            >
          </div>

          <div class="form-group">
            <button
              type="submit"
              :disabled="$v.$invalid"
              class="btn btn-primary w-100"
            >INICIA SESIÓN</button>
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

import { required, email } from "vuelidate/lib/validators";
export default {
  name: "login",
  data() {
    return {
      userName: "",
      password: ""
    };
  },
  validations: {
    userName: {
      required,
      email
    },
    password: {
      required
    }
  },
  computed: {},
  methods: {
    onSubmit: function() {
      let userName = this.userName;
      let password = this.password;
      this.$store
        .dispatch("login", { userName, password })
        .then(() => {
          console.log("to landing");
          this.$router.push({ name: "landing" });
        })
        .catch(err => console.log(err));
    }
  }
};
</script>
 