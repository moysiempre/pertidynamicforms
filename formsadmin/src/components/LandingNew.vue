<template>
  <div class="card border-light">
    <div class="card-body">
      <h5 class="mt-2">{{title}}</h5>
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group" :class="{invalid: $v.formData.name.$error}">
          <label class="mb-0" for="name">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="name"
            placeholder="Digite el nombre del landing page"
            @blur="$v.formData.name.$touch()"
            v-model="formData.name"
          >
        </div>
        <div class="form-group" :class="{invalid: $v.formData.landingTypeId.$error}">
          <label class="mb-0" for="landingTypeId">
            Tipo de landing page
            <span class="i-required">*</span>
          </label>
          <select
            class="custom-select mr-sm-2"
            id="landingTypeId"
             @blur="$v.formData.landingTypeId.$touch()"
            v-model="formData.landingTypeId"
            required
            
          >
            <option value="">Seleccione...</option>
            <option value="1">One</option>
            <option value="2">Two</option>
            <option value="3">Three</option>
          </select>
        </div>

        <div class="form-group">
          <label class="mb-0" for>Descripción del landing page</label>
          <textarea
            class="form-control"
            placeholder="Digite una descripción del landing page"
            v-model="formData.description"
          ></textarea>
        </div>
        <div class="form-group">
          <label class="mb-0" for>Ubicación del Landing page</label>
          <div class="custom-file">
            <input type="file" class="custom-file-input" id="validatedCustomFile">
            <label class="custom-file-label" for="validatedCustomFile">Choose file...</label>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="customControlValidation1"
                v-model="formData.isActive"
              >
              <label class="custom-control-label" for="customControlValidation1">Es Activo</label>
            </div>
          </div>
          <div class="col-md-8 mt-3">
            <div class="form-group text-right">
              <button
                type="button"
                @click="$v.formData.$reset"
                class="btn btn-outline-warning btn-sm mx-1"
              >RESETEAR</button>
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
  props: ["title"],
  data() {
    return {
      //title: "ALTA LANDING PAGE",
      formData: { name: "", landingTypeId: "", firstName: "" }
    };
  },
  validations: {
    formData: {
      name: { required },
      landingTypeId: { required }
    }
  },
  methods: {
    onSubmit() {
      console.log("onSubmit " + new Date().toDateString());
      console.log(this.formData);
      this.resetForm();
    },
    resetForm() {
      this.formData = {
        name: "",
        landingTypeId: "",
        description: "",
        isActive: false
      };
    }
  }
};
</script>
