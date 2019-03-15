<template>
  <div class="container-fluid">
    <h5>{{title}}</h5>
    <!-- <router-link to="/solicitudes/1">detalle Solicitudes</router-link> -->
    <div>
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
export default {
  name: "HelloWorld",
  data() {
    return {
      title: "SOLICITUDES",
      rowData: []
    };
  },
  beforeMount() {},
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
    }
  }
};
</script>
<style lang="css">
</style>
