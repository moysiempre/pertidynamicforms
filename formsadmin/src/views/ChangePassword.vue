<template>
  <div class="container-fluid">
    <div class="card change-password">
      <h5 class="text-center font-weight-normal mb-3">LOGIN</h5>
      <div class="card-body">
        <form @submit.prevent="onSubmit">
          <div class="form-group" :class="{invalid: $v.formData.email.$error}">
            <input
              type="email"
              id="email"
              class="form-control"
              placeholder="Digite su correo electrónico"
              @blur="$v.formData.email.$touch()"
              v-model="formData.email"
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
          <div class="form-group" :class="{invalid: $v.formData.repassword.$error}">
            <input
              type="password"
              id="password"
              class="form-control"
              placeholder="digite su contraseña"
              @blur="$v.formData.repassword.$touch()"
              v-model="formData.repassword"
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

 

export default {
  name: "login",
  data() {
    return {
      formData: {
        email: "",
        password: ""
      }
    };
  },
 
  methods: {
    onSubmit() {
      console.log("onSubmit " + new Date().toDateString());
    }
  }
};
</script>
 

