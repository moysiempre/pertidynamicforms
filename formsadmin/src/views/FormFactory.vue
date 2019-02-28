<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-5 col-md-4 col-lg-3">
        <landing-list/>
      </div>

      <div class="col-12 col-sm-7 col-md-6 col-lg-5 mt-3 mt-sm-0">
        <FormNew/>
      </div>

      <div class="row" hidden>
        <div class="col-md-6">
          
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
          <form-generator :schema="formItems"></form-generator>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import FormGenerator from "@/components/forms/FormGenerator";
import { AgGridVue } from "ag-grid-vue";
import FormNew from "@/components/FormNew.vue";
import LandingList from "@/components/LandingList.vue";

export default {
  components: { LandingList, FormNew, FormGenerator, AgGridVue },
  data() {
    return {
      msg: "Welcome to Your Vue.js App",     
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

 
 