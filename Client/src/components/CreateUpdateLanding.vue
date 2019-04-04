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
        <div class="form-group">
          <label class="mb-0" for="name">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            v-validate="'required'"
            type="text"
            class="form-control"
            name="name"
            maxlength="150"
            placeholder="Digite el nombre del landing page"
            v-model="landingPage.name"
          />
        </div>

        <div class="form-group">
          <label class="mb-0" for="description"
            >Descripción del landing page</label
          >
          <textarea
            class="form-control"
            name="description"
            maxlength="400"
            placeholder="Digite una descripción del landing page"
            v-model="landingPage.description"
          ></textarea>
        </div>

        <div class="form-group">
          <label class="mb-0" for="description">Formulario Asignado</label>
          <select class="custom-select" v-model="landingPage.formHdId">
            <option value>seleccione</option>
            <option
              v-for="(item, index) in formOpts"
              :key="index"
              :value="item.id"
              >{{ item.name }}</option
            >
          </select>
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="customControlValidation1"
                v-model="landingPage.isActive"
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
                <btn-loader :isloading="isloading" />
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

export default {
  props: ['title'],
  components: { BtnLoader },
  data() {
    return {
      isloading: false
    }
  },
  computed: {
    ...mapState({
      landingPage: state => state.landings.landingPage,
      formOpts: state => state.landings.formOpts,
      action: state => state.landings.action
    }),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid)
    }
  },
  methods: {
    onSubmit() {
      this.isloading = true
      axios
        .post('api-landingpage', this.landingPage)
        .then(response => {
          this.isloading = false
          if (response && response.data && response.data.id) {
            this.landingPage.id = response.data.id
            this.$store.dispatch('loadLandings')
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
          this.$swal('No se pudo dar de alta al landing page', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onCancel() {
      this.$store.commit('SET_ACTION', 'read')
      this.$store.commit('SET_LANDING', {})
      this.$store.dispatch('loadLandings')
    }
  }
}
</script>
