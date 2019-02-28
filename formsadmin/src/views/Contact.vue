<template>
  <div class="container-fluid">
    <h5>SOLICITUDES</h5>
    <!-- <router-link to="/solicitudes/1">detalle Solicitudes</router-link> -->
     
    <div>
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
</template>
<script>
import { AgGridVue } from "ag-grid-vue";

export default {
  components: { AgGridVue },
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      columnDefs: null,
      rowData: null
    };
  },
  beforeMount() {
    this.gridOptions = {
      rowHeight: 40
    };
    this.columnDefs = [
      { headerName: "NOMBRE COMPLETO", field: "fullName" },
      { headerName: "EMAIL", field: "email" },
      { headerName: "TELÉFONO", field: "phone" },
      { headerName: "NOMBRE DE LA EMPRESA", field: "companyName" },
      { headerName: "", field: "options", width:50 }
    ];

    this.rowData = [
      { fullName: "JULIO MATIAS HERNANDEZ", email: "julio.hernandez@yahoo.es", phone: 3500035000, companyName: "JULIO MATIAS HERNANDEZ sa de cv", options: "detalle" },
      { fullName: "Alejandro Soto Jiménez", email: "alejandra.jimenez@yahoo.es", phone: 3200032000, companyName: 2, options: "detalle" },
      { fullName: "Jane Doe", email: "janedoe@yahoo.es", phone: 7200072000, companyName: 3, options: "detalle" },
      { fullName: "Maria de los Angeles Rios", email: "maria_rios@yahoo.es", phone: 3500035000, companyName: 1, options: "detalle" },
      { fullName: "PEDRO ALE MAXIMILLIAN", email: "pedro.max@yahoo.es", phone: 3200320000, companyName: 2 , options: "detalle"},
      { fullName: "WILLIE GOUSTAZO RETES", email: "willie.retes@yahoo.es", phone: 7200720000, companyName: 3 , options: "detalle"},
      { fullName: "PIERRE MARCHAND MERISIER", email: "pierremerisier@yahoo.es", phone: 3535000000, companyName: 1, options: "detalle" },
      { fullName: "BONNIE BENNETT", email: "bonnie.bennett@yahoo.es", phone: 3200032000, companyName: 2, options: "detalle" },
      { fullName: "Jennifer Ally", email: "jennyontheblock@yahoo.es", phone: 7200072000, companyName: 3, options: "detalle" }
    ];      
  },
  mounted() {
    this.gridApi = this.gridOptions.api;
    this.gridColumnApi = this.gridOptions.columnApi;   
  },
  methods: {
    onGridReady(params) {
      params.api.sizeColumnsToFit();

      params.api.sizeColumnsToFit();
      window.addEventListener("resize", function() {
        setTimeout(function() {
          params.api.sizeColumnsToFit();
        });
      });
    }
  }
};
</script>
<style lang="css">
</style>
