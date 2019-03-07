<template>
  <div class="card border-light">
    <div class="card-body">
      <h5 class="mt-2">NUEVO</h5>
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group" :class="{invalid: $v.formData.name.$error}">
          <label class="mb-0" for="name">
            Nombre del formulario
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="name"
            placeholder="Digite el nombre del formulario"
            @blur="$v.formData.name.$touch()"
            v-model="formData.name"
          >
        </div>
        <div class="form-group" :class="{invalid: $v.formData.formTitle.$error}">
          <label class="mb-0" for="formTitle">
            Título del formulario
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="formTitle"
            placeholder="Digite el título del formulario"
            @blur="$v.formData.formTitle.$touch()"
            v-model="formData.formTitle"
          >
        </div>
        <div class="form-group" :class="{invalid: $v.formData.landings.$error}">
          <label class="mb-0" for="landings">Landing pages Asociados</label>
          <select
            class="custom-select mr-sm-2"
            id="landings"
            @blur="$v.formData.landings.$touch()"
            v-model="formData.landings"
            required
          >
            <option value>Seleccione...</option>
            <option value="1">One</option>
            <option value="2">Two</option>
            <option value="3">Three</option>
          </select>
        </div>
        <div class="form-group">
          <label class="mb-0" for>Ubicación del archivo a subir</label>
          <div class="custom-file">
            <input type="file" class="custom-file-input" id="validatedCustomFile" required>
            <label class="custom-file-label" for="validatedCustomFile">Choose file...</label>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="cboIsActive"
                v-model="formData.isActive"
              >
              <label class="custom-control-label" for="cboIsActive">Es Activo</label>
            </div>
          </div>
          <div class="col-md-8 mt-3">
            <div class="form-group text-right">
              <button
                type="button"
                @click="onCancel"
                class="btn btn-outline-warning btn-sm mx-1"
              >CANCELAR</button>
              <button
                type="submit"
                class="btn btn-primary btn-sm"
                :disabled="$v.formData.$invalid"
              >GUARDAR</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { required } from "vuelidate/lib/validators";

export default {
  data() {
    return {
      title: "ALTA LANDING PAGE",
      formData: { name: "", formTitle: "" }
    };
  },
  validations: {
    formData: {
      name: { required },
      formTitle: { required },
      landings: { required }
    }
  },
  methods: {
    onSubmit() {
      this.$emit("onSaved");
    },
    onCancel() {
      this.$emit("onCancel");
    }
  }
};
</script>
<style lang="scss">
</style>


