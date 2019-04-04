<template>
  <div class="container-fluid">
    <div class="card login">
      <h5 class="text-center font-weight-normal mb-3">RESETEA CONTRASEÑA</h5>
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
          <div class="form-group">
            <button
              type="submit"
              class="btn btn-primary w-100"
              :disabled="isloading"
            >
              <span>RESETEA CONTRASEÑA</span>
              <btn-loader :isloading="isloading" />
            </button>
          </div>
          <div class="form-group">
            <p class="mb-0">
              <small class="text-muted pr-2"
                >¿Te acuerdas de tu contraseña?</small
              >
              <router-link to="/login">
                <small>INICIA SESIÓN</small>
              </router-link>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import BtnLoader from '@/components/BtnLoader.vue'
export default {
  components: { BtnLoader },
  data() {
    return {
      isloading: false,
      userName: ''
    }
  },
  methods: {
    onSubmit() {
      this.$validator.validateAll().then(isValid => {
        if (isValid) {
          this.isloading = true

          axios({
            method: 'GET',
            url: `api-security/resetepassword/${this.userName}`
          })
            .then(response => {
              this.isloading = false
              if (response && response.data && response.data.success) {
                this.$swal('Tu contraseña fue enviado a ' + this.userName).then(
                  () => {
                    this.$router.push({ name: 'login' })
                  }
                )
              } else {
                this.$swal(response.data.message, {
                  icon: 'warning',
                  closeOnClickOutside: false
                })
              }
            })
            .catch(err => {
              console.log(err)
              this.isloading = false
              this.$swal('No se pudo resetear su contraseña', {
                icon: 'warning',
                closeOnClickOutside: false
              })
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

<style scoped></style>
