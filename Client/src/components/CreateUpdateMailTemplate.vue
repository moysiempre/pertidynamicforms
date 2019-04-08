<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">{{ title }}</h6>
      <button class="btn btn-link close" @click="onCancel">
        <i class="pe-7s-close fa-2x"></i>
      </button>
    </div>
    <div class="card-body">
      <form @submit.prevent="onSubmit">
        <div class="row">
          <div class="col-12 col-md-6">
            <div class="form-group">
              <label class="mb-0" for="name">
                Nombre de la plantilla
                <span class="i-required">*</span>
              </label>
              <input
                v-validate="'required'"
                type="text"
                class="form-control"
                name="name"
                maxlength="150"
                placeholder="Digite el nombre"
                v-model="mailtemplate.name"
              />
            </div>
          </div>
          <div class="col-12 col-md-6">
            <div class="form-group">
              <label class="mb-0" for="name">
                Saludo
                <small class="text-muted">ej. Estimado...</small>
                <span class="i-required">*</span>
              </label>
              <input
                v-validate="'required'"
                type="text"
                class="form-control"
                name="name"
                maxlength="150"
                placeholder="Digite el saludo"
                v-model="mailtemplate.salut"
              />
            </div>
          </div>
        </div>

        <div class="form-group">
          <label class="mb-0" for="name">
            Asunto
            <span class="i-required">*</span>
          </label>
          <input
            v-validate="'required'"
            type="text"
            class="form-control"
            name="name"
            maxlength="150"
            placeholder="Digite el asunto"
            v-model="mailtemplate.subject"
          />
        </div>
        <div class="form-group">
          <label class="mb-0" for="description">
            Cuerpo del mensaje
            <span class="i-required">*</span>
          </label>
          <ckeditor
            :editor="editor"
            v-model="mailtemplate.body"
            :config="editorConfig"
          ></ckeditor>
          <div>
            <small class="text-muted">Máximun caracters</small>
            <small>:</small>
            <small>{{ bodylength }}</small>
            <small>
              /
              <strong>1000</strong>
            </small>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="customControlValidation1"
                v-model="mailtemplate.isActive"
              />
              <label class="custom-control-label" for="customControlValidation1"
                >Es Activo</label
              >
            </div>
          </div>
          <div class="col-md-8 mt-3">
            <div class="form-group text-right">
              <button
                type="button"
                @click="onCancel"
                class="btn btn-outline-warning btn-sm mx-1"
              >
                CANCELAR
              </button>
              <button
                type="submit"
                class="btn btn-primary btn-sm"
                :disabled="!isFormValid || isloading"
              >
                <span>GUARDAR</span>
                <btn-loader :isloading="isloading"></btn-loader>
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import { mapState } from 'vuex'
import BtnLoader from '@/components/BtnLoader.vue'
import ClassicEditor from '@ckeditor/ckeditor5-build-classic'

export default {
  props: ['title'],
  components: { BtnLoader },
  data() {
    return {
      isloading: false,
      editor: ClassicEditor,
      editorConfig: {
        toolbar: {
          items: [
            'heading',
            '|',
            'bold',
            'italic',
            'link',
            'bulletedList',
            'numberedList',
            'insertTable',
            'undo',
            'redo'
          ]
        }
      }
    }
  },
  computed: {
    ...mapState({
      mailtemplate: state => state.templates.mailtemplate,
      action: state => state.templates.action
    }),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid)
    },
    bodylength() {
      return this.mailtemplate &&
        this.mailtemplate.body &&
        this.mailtemplate.body.length
        ? this.mailtemplate.body.length
        : 0
    }
  },
  methods: {
    onSubmit() {
      if (this.bodylength > 1000) {
        this.$swal({
          text: 'Solo se permite maximun 400 carácteres',
          dangerMode: true,
          closeOnClickOutside: false
        })
        return
      }

      this.isloading = true
      axios
        .post('api-mailtemplate', this.mailtemplate)
        .then(response => {
          this.isloading = false
          if (response && response.data && response.data.id) {
            this.mailtemplate.id = response.data.id
            if (this.action === 'create') {
              this.$store.dispatch('loadTemplates')
            }
            this.$swal(response.data.message, {
              icon: 'success',
              closeOnClickOutside: false
            })
            this.onCancel()
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(error => {
          this.isloading = false
          console.log(error)
          this.$swal('No se pudo dar de alta al template', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onCancel() {
      this.$store.commit('SET_ACTION', 'read')
      this.$store.commit('SET_TEMPLATE', {})
      this.$store.dispatch('loadTemplates')
    }
  }
}
</script>
<style>
.ck-editor__editable {
  min-height: 150px !important;
}
</style>
