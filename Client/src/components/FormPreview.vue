<template>
  <div>
    <form id="contactForm">
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-header bg-white p-0">
              <div
                class="text-center alert alert-success mb-0"
                hidden
                style="border-radius: 0"
              >
                {{ formHd.title }}
              </div>
              <h5 class="text-center my-3">{{ formHd.title }}</h5>

              <div
                class="alert alert-dismissible fade show iz-alert"
                :class="alert_class"
                role="alert"
                v-if="isSended"
              >
                {{ message_gp }}
                <button
                  type="button"
                  class="close"
                  data-dismiss="alert"
                  aria-label="Close"
                >
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
            </div>
            <div class="card-body bg-light p-2">
              <div
                class="container-fluid bg-white pt-3 pb-3"
                style="min-height: 200px;"
              ></div>
            </div>
          </div>
        </div>
        <hr />
      </div>
    </form>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  data() {
    return {
      baseUrl: 'http://localhost:60829/landing/',
      formData: {
        id: ''
      },
      isloading: false,
      isSended: false,
      message_gp: '',
      alert_class: 'alert-success'
    }
  },
  computed: {
    ...mapState({
      formHd: state => state.forms.formHd
    })
  },
  methods: {
    getErrMsg(errors, field) {
      var message = ''
      var error = errors.items.find(x => x.field == field)
      if (error) {
        switch (error.rule) {
          case 'required':
            message = `El campo ${field} es obligatorio.`
            break
          case 'email':
            message = `El campo ${field} debe ser un correo electrónico válido.`
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
