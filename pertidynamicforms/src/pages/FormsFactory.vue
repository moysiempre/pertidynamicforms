<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-md-3">
        <div class="card border-light helix">
          <div class="card-header">
            <h5 class="mt-2">LANDING PAGES</h5>
          </div>
          <div class="card-body p-0">
            <ul class="list-group">
              <li class="list-group-item">
                <h6 class="mb-0">MOBILE XT</h6>
                <p class="mb-0" style="color:#75818b">Obtener asesoría especializada</p>
                <p class="mb-0 small text-muted">
                  <small style="color:#a3abb2">very good always</small>
                </p>
              </li>
              <li class="list-group-item">
                <h6 class="mb-0">MOBILE XT</h6>
                <p class="mb-0" style="color:#75818b">Obtener asesoría especializada</p>
                <p class="mb-0 small text-muted">
                  <small style="color:#a3abb2">very good always</small>
                </p>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="card border-light bg-xlight">
          <div class="card-body">
            <h5 class="mt-2">NUEVO</h5>
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
                      @blur="$v.factoryData.fieldLabel.$touch()"
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
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cboAttachFile"
                        v-model="factoryData.attachFile"
                      >
                      <label class="custom-control-label" for="cboAttachFile">Adjunta archivo</label>
                    </div>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group text-right">
                    <button
                      type="button"
                      class="btn btn-outline-warning btn-sm"
                      @click="resetForm"
                    >RESETEAR</button>
                    <button
                      type="button"
                      class="btn btn-dark btn-sm px-3"
                      :disabled="$v.factoryData.$invalid"
                      @click="onSubmit"
                    >GUARDAR</button>
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
        <div class="row">
          <div class="col-12 mt-3">
            <ag-grid-vue
              :gridOptions="gridOptions"
              style="width:100%"
              class="ag-theme-balham"
              :columnDefs="columnDefs"
              :rowData="rowData"
              @grid-ready="onGridReady"
              :paginationPageSize="5"
              :pagination="true"
            ></ag-grid-vue>
          </div>
        </div>
      </div>

      <div class="col-md-3">
        <div class="card bg-elight h-100">
          <h6 class="text-right mt-4 mb-3 mx-3">VISTA PREVIA</h6>
          <div class="card mx-2">
            <div class="card-header bg-white">
              <h6 class="mt-2">Obtener asesoría especializada</h6>
            </div>
            <div class="card-body">
              <form-generator :schema="formItems"></form-generator>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import FormGenerator from "../components/forms/FormGenerator";
import { required } from "vuelidate/lib/validators";
import { AgGridVue } from "ag-grid-vue";

export default {
  components: { FormGenerator, AgGridVue },
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      factoryData: {},
      formItems: [
        {
          fieldType: "textInput",
          fieldLabel: "Nombre completo",
          position: 1,
          isRequired: false,
          attachFile: false
        }
      ],
      gridColumns: [
        "fieldType",
        "fieldLabel",
        "position",
        "isRequired",
        "attachFile"
      ],
      columnDefs: null,
      rowData: null
    };
  },
  validations: {
    factoryData: {
      fieldLabel: {
        required: required
      }
    }
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
    onSubmit() {
      this.formItems.push(this.factoryData);
      this.resetForm();
    },
    onGridReady(params) {
      params.api.sizeColumnsToFit();

      params.api.sizeColumnsToFit();
      window.addEventListener("resize", function() {
        setTimeout(function() {
          params.api.sizeColumnsToFit();
        });
      });
    }
  },
  beforeMount() {
    this.gridOptions = {
      rowHeight: 40
    };
    this.columnDefs = [
      { headerName: "Make", field: "make" },
      { headerName: "Model", field: "model" },
      { headerName: "Price", field: "price" },
      { headerName: "Position", field: "position" }
    ];

    this.rowData = [
      { make: "Toyota", model: "Celica", price: 35000, position: 1 },
      { make: "Ford", model: "Mondeo", price: 32000, position: 2 },
      { make: "Porsche", model: "Boxter", price: 72000, position: 3 },
      { make: "Toyota", model: "Celica", price: 35000, position: 1 },
      { make: "Ford", model: "Mondeo", price: 32000, position: 2 },
      { make: "Porsche", model: "Boxter", price: 72000, position: 3 },
      { make: "Toyota", model: "Celica", price: 35000, position: 1 },
      { make: "Ford", model: "Mondeo", price: 32000, position: 2 },
      { make: "Porsche", model: "Boxter", price: 72000, position: 3 }
    ];
  },
  mounted() {
    this.resetForm();
    this.gridApi = this.gridOptions.api;
    this.gridColumnApi = this.gridOptions.columnApi;
  }
};
</script>
 
 
<style lang="scss">
.ag-theme-balham .ag-row-odd {
  background-color: #fafafa;
}
.ag-theme-balham .ag-cell {
  line-height: 32px;
}
.ag-theme-balham .ag-header {
  background-color: #fff;
  border-bottom: 1px solid #ddd;
  font-weight: bold;
}
.ag-theme-balham .ag-row {
  border-color: #ddd;
}
.ag-theme-balham .ag-root {
  border: 1px solid #ededed;
}
.ag-theme-balham .ag-paging-panel {
  border: none;
}
</style>

 
 