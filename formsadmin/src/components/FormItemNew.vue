<template>
  <div class="card border-light bg-xlight">
    <div class="card-body">
      <h5 class="mt-0">{{title}}</h5>
      <form id="factoryForm" class="mt-3">
        <div class="row">
          <div class="col-md-5">
            <div class="form-group">
              <label for="fieldType" class="mb-0">Tipo de entrada</label>
              <select id="fieldType" class="custom-select" v-model="factoryData.fieldType">
                <option value="textInput" selected>Texto alfanumérico</option>
                <option value="numberInput">Numérico</option>
                <option value="radio">Radio</option>
                <option value="checkbox">Checkbox</option>
              </select>
            </div>
          </div>

          <div class="col-md-5">
            <div class="form-group">
              <label for="fieldLabel" class="mb-0">Etiqueta</label>
              <input
                type="text"
                class="form-control"
                id="fieldLabel"
                placeholder="digite la etiqueta"
                v-model="factoryData.fieldLabel"
              >
            </div>
          </div>
          <div class="col-md-2">
            <div class="form-group">
              <label for="position" class="mb-0">Posición</label>
              <input
                type="number"
                id="position"
                class="form-control"
                value="0"
                min="0"
                v-model="factoryData.position"
              >
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-md-6">
            <div class="d-flex justify-content-between">
              <div class="custom-control custom-checkbox">
                <input
                  type="checkbox"
                  class="custom-control-input"
                  id="cboIsRequired"
                  v-model="factoryData.isRequired"
                >
                <label class="custom-control-label" for="cboIsRequired">Es obligatorio</label>
              </div>
               
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group text-right">
              <button
                type="button"
                class="btn btn-outline-warning btn-sm mx-1"
                @click="resetForm"
              >RESETEAR</button>
              <button type="button" class="btn btn-dark btn-sm px-3" @click="onSubmit">GUARDAR</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
// import { required } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      title: "DETALLE FORMULARIO",
      factoryData: {}
    };
  },
  methods: {
    resetForm() {
      this.factoryData = {
        fieldType: "textInput",
        fieldLabel: "",
        position: 0,
        isRequired: false,
        attachFile: false
      };
    },
    onSubmit(){
      
    }
  }
};
</script>
