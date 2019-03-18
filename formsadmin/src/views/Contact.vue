<template>
  <div class="container-fluid">
    <h5>{{title}}</h5>
    <!-- <router-link to="/solicitudes/1">detalle Solicitudes</router-link> -->
    <div class="row mb-1">
      <div class="col-12 col-sm-4 col-md-6"></div>
      <div class="col-12 col-sm-8 col-md-6">
        <div class="row no-gutters">
          <div class="col-sm-6">
            <select class="custom-select mr-sm-2" id="ddllandings">
              <option value>Seleccione...</option>
              <option v-for="item in landingPages" :key="item.id" :value="item.id">{{item.name}}</option>
            </select>
          </div>
          <div class="col-sm-3">
            <input type="text" class="form-control mr-1" placeholder="fecha inicio">
          </div>
          <div class="col-sm-3">
            <input type="text" class="form-control" placeholder="fecha fin">
          </div>
        </div>
      </div>
    </div>
    <div class="bg-white">
      <table class="table table-stripped" v-if="rowData">
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Email</th>
            <th>Teléfono</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in rowData" :key="index">
            <td>{{item.objData.name}}</td>
            <td>{{item.objData.email}}</td>
            <td>{{item.objData.phone}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
import axios from "axios";
// import Datepicker from "vuejs-datepicker";
export default {
  name: "HelloWorld",
 // components: { Datepicker },
  data() {
    return {
      title: "SOLICITUDES",
      rowData: [],
      landingPages: []
    };
  },
  beforeMount() {
    this.loadLandingPages();
  },
  mounted() {
    this.load();
  },
  methods: {
    load() {
      axios.get("api-inforequest").then(response => {
        this.rowData = response.data;
        this.rowData.forEach(element => {
          element.objData = JSON.parse(element.infoRequestData);
        });
        console.log(this.rowData);
      });
    },
    loadLandingPages() {
      axios.get("api-landingpage").then(response => {
        this.landingPages = response.data;
      });
    }
  }
};
</script>
<style lang="css">
</style>
 