<template>
  <div class="card border-light">
    <div class="card-body">
      <h5 class="mt-0">{{title}}</h5>
      <button type="button" class="btn btn-primary" @click="myToastr">toastr</button>
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group">
          <label class="mb-0" for="name">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="name"
            placeholder="Digite el nombre del landing page"
            v-model="landingPage.name"
          >
        </div>
        <div class="form-group">
          <label class="mb-0" for="typeId">
            Tipo de landing page
            <span class="i-required">*</span>
          </label>
          <select class="custom-select mr-sm-2" id="typeId" v-model="landingPage.typeId" required>
            <option value>Seleccione...</option>
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
            v-model="landingPage.description"
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
                v-model="landingPage.isActive"
              >
              <label class="custom-control-label" for="customControlValidation1">Es Activo</label>
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
  props: ["title", "landing"],
  data() {
    return {
      name: "",
      typeId: "",
      description: "",
      filePath: "",
      isActive: ""
    };
  },
  computed: {
    ...mapState(["landingPage"])
  },
  validations: {
    name: { required },
    typeId: { required }
  },

  methods: {
    updateStore(formData) {
      let action = this.$store.state.lpAction;
      if (action == "create") {
        this.$store.state.landingPages.push(formData);
      }  
    },
    onSubmit() {
      let landingPage = this.$store.state.landingPage;
      axios({ method: "POST", url: "api-landingpage", data: landingPage })
        .then(response => {
          landingPage.id = response.data.id;
          this.updateStore(landingPage);
          this.onCancel();
          this.$snotify.success("this.body for snotify");
        })
        .catch(err => {
          console.log(err);
        });
    },
    myToastr(){
      this.$snotify.success("this.body for snotify");
    },
    onCancel() {
      this.$store.state.lpAction = "read";
    }
  }
};
</script>
 