<template>
  <div class="container-fluid">
    <div class="card login">
      <h5 class="text-center font-weight-normal mb-3">LOGIN</h5>
      <div class="card-body">
        <form @submit.prevent="onSubmit">
          <div class="form-group" :class="{invalid: $v.formData.userName.$error}">
            <input
              type="email"
              id="userName"
              class="form-control"
              placeholder="Digite su correo electrónico"
              @blur="$v.formData.userName.$touch()"
              v-model="formData.userName"
            >
          </div>
          <div class="form-group" :class="{invalid: $v.formData.password.$error}">
            <input
              type="password"
              id="password"
              class="form-control"
              placeholder="digite su contraseña"
              @blur="$v.formData.password.$touch()"
              v-model="formData.password"
            >
          </div>

          <div class="form-group">
            <button
              type="submit"
              :disabled="$v.formData.$invalid"
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
      formData: {
        userName: "",
        password: ""
      }
    };
  },
  validations: {
    formData: {
      userName: {
        required,
        email
      },
      password: {
        required
      }
    }
  },
  methods: {
    onSubmit() {

       this.$http.post("api-security/login", this.formData).then(
        response => {
          var data = response.body;
          localStorage.setItem('bearerToken', data.bearerToken);
          this.$router.push({ name: "landing" });
        },
        error => {
          console.log("error", error);
        }
      );
      
    }
  }
};
</script>
 