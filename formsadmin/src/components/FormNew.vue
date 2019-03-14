<template>
  <div class="card border-light">
    <div class="card-body">
      <h5 class="mt-0">{{title}}</h5>
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group">
          <label class="mb-0" for="name">
            Nombre del formulario
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="name"
            placeholder="Digite el nombre del formulario"
            v-model="formHd.name"
          >
        </div>
        <div class="form-group">
          <label class="mb-0" for="formTitle">
            Título del formulario
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="formTitle"
            placeholder="Digite el título del formulario"
            v-model="formHd.title"
          >
        </div>
        <div class="form-group">
          <label class="mb-0" for="landings">Landing pages Asociados</label>
          <select class="custom-select mr-sm-2" id="landings" v-model="formHd.landings" required>
            <option value>Seleccione...</option>
            <option  v-for="item in landingPages" :key="item.id" :value="item.id">{{item.name}}</option>
          </select>
        </div>

        <div class="form-group" v-if="formHd.id">
          <label class="mb-0" for>Ubicación del archivo a subir</label>
          <input type="file" id="file" ref="file" v-on:change="handleFileUpload()">
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="cboIsActive"
                v-model="formHd.isActive"
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
              <button type="submit" class="btn btn-primary btn-sm">GUARDAR</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { required } from "vuelidate/lib/validators";
import axios from "axios";
import { mapState } from "vuex";

export default {
  data() {
    return {
      title: "ALTA FORMULARIO",
      formData: { name: "", formTitle: "" },
      file: {},
      landingPages: []
    };
  },
  validations: {
    formData: {
      name: { required },
      formTitle: { required },
      landings: { required }
    }
  },
  beforeMount(){
    this.loadLandingPages();
  },
  computed: {
    ...mapState(["formHd"])
  },
  methods: {
    loadLandingPages() {
      axios.get("api-landingpage").then(response => {
        this.landingPages = response.data;
      });
    },
    updateStore(formData) {
      let action = this.$store.state.fhAction;
      if (action == "create") {
        this.$store.state.formHds.push(formData);
      }
    },
    onSubmit() {
      let formHd = this.$store.state.formHd;

      console.log("formHd: ", formHd);
      axios({ method: "POST", url: "api-forms", data: formHd })
        .then(response => {
          formHd.id = response.data.id;
          this.updateStore(formHd);
          this.onCancel();
          this.$snotify.success("this.body for snotify");
        })
        .catch(err => {
          console.log(err);
          this.$snotify.error("this.body for snotify");
        });
    },
    handleFileUpload() {
      this.file = this.$refs.file.files[0];
      console.log("file handled", this.file.name);
    },
    fileUpload() {
      let formData = new FormData();
      formData.append("file", this.file);
      axios
        .post("api-fileupload", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        })
        .then(function() {
          console.log("SUCCESS!!");
        })
        .catch(function(error) {
          console.log("FAILURE!!", error);
        });
    },
    onCancel() {
      this.$store.state.fhAction = "read";
    }
  }
};
</script>
<style lang="scss">
</style>


