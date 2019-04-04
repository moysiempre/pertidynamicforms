<template>
  <div class="container-fluid">
    <div class="card login">
      <h5 class="text-center font-weight-normal mb-3">INICIA SESIÓN</h5>
      <div class="card-body">
        <form @submit.prevent="onSubmit">
          <div class="form-group text-left">
            <input
              v-validate="'required|email'"
              type="text"
              id="userName"
              name="userName"
              class="form-control"
              placeholder="Digite su correo electrónico"
              v-model="userName"
            />
            <small class="text-danger">
              {{ getErrMsg(errors, 'userName') }}
            </small>
          </div>
          <div class="form-group text-left">
            <input
              v-validate="'required'"
              type="password"
              id="password"
              name="password"
              class="form-control"
              placeholder="Digite su contraseña"
              v-model="password"
            />
            <small class="text-danger">
              {{ getErrMsg(errors, 'password') }}
            </small>
          </div>

          <div class="form-group">
            <button
              type="submit"
              class="btn btn-primary w-100"
              :disabled="isloading"
            >
              <span>INICIAR SESIÓN</span>
              <btn-loader :isloading="isloading" />
            </button>
          </div>
          <div class="form-group">
            <p class="mb-0">
              <small class="text-muted pr-2"
                >¿Has olvidado tu contraseña?</small
              >
              <router-link to="/resetepassword">
                <small>RESETÉALO</small>
              </router-link>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import BtnLoader from '@/components/BtnLoader.vue'
export default {
  name: 'login',
  components: { BtnLoader },
  data() {
    return {
      userName: '',
      password: '',
      isloading: false
    }
  },
  created() {
    this.$store.commit('SET_FULL_LAYOUT', false)
  },
  mounted() {},
  computed: {},
  methods: {
    onSubmit: function() {
      this.$validator.validateAll().then(isValid => {
        if (isValid) {
          this.isloading = true
          let userName = this.userName
          let password = this.password
          this.$store
            .dispatch('login', { userName, password })
            .then(response => {
              this.isloading = false
              if (response.data.success === true) {
                this.$store.commit('SET_FULL_LAYOUT', true)
                this.$router.push({ name: 'formularios' })
              } else {
                this.$swal(response.data.message, {
                  icon: 'warning',
                  closeOnClickOutside: false
                })
              }
            })
            .catch(err => {
              this.isloading = false
              let message =
                'No se pudo logear, favor valide su conexión a internet, o comuniquese con el admistrador'
              this.$swal(message, {
                icon: 'error',
                closeOnClickOutside: false
              })
              console.log('err-login', err)
            })
        }
      })
    },
    getErrMsg(errors, field) {
      var message = ''
      var error = errors.items.find(x => x.field == field)
      if (error) {
        switch (error.rule) {
          case 'required':
            message = `El campo es obligatorio.`
            break
          case 'email':
            message = `El campo debe ser un correo electrónico válido.`
            break
          default:
            break
        }
      } else {
        message = ''
      }

      return message
    }
  }
}
</script>
