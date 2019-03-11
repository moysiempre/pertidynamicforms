<template>
  <div class="card border-light">
    <h5 class="card-header">
       <h5 class="mt-2">{{title}}</h5>
    </h5>
    <div class="card-body">     
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group">
          <label class="mb-0" for="vname">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="vname"
            placeholder="Digite el nombre del landing page"
            @blur="$v.landingPage.name.$touch()"
            v-model="landingPage.name"
          >
        </div>
      </form>
      <form @submit.prevent="onSubmit" class="mt-3">
        <div class="form-group" :class="{invalid: $v.name.$error}">
          <label class="mb-0" for="name">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            type="text"
            class="form-control"
            id="name"
            placeholder="Digite el nombre del landing page"
            @blur="$v.name.$touch()"
            v-model="name"
          >
        </div>
        <div class="form-group" :class="{invalid: $v.landingTypeId.$error}">
          <label class="mb-0" for="landingTypeId">
            Tipo de landing page
            <span class="i-required">*</span>
          </label>
          <select
            class="custom-select mr-sm-2"
            id="landingTypeId"
            @blur="$v.landingTypeId.$touch()"
            v-model="landingTypeId"
            required
          >
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
            v-model="description"
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
                v-model="isActive"
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
              <button type="submit" class="btn btn-primary btn-sm" :disabled="$v.$invalid">GUARDAR</button>
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
import {mapState} from "vuex";
export default {
  props: ["title", "landing"],
  data() {
    return {
      name: "",
      landingTypeId: "",
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
    landingTypeId: { required }
  },

  methods: {
    updateStore(formData) {
      this.$store.state.landingPages.push(formData);
    },
    setLandingPage() {},
    onSubmit() {
      let formData = {
        name: this.name,
        landingTypeId: this.landingTypeId,
        description: this.description,
        filePath: "filePath",
        isActive: this.isActive
      };

      axios({ method: "POST", url: "api-landingpage", data: formData })
        .then(response => {
          formData.id = response.data.id;
          this.updateStore(formData);
        })
        .catch(err => {
          console.log(err);
        });
    },
    onCancel() {
      this.$store.state.lpAction = "read";
    }
  }
};
</script>
 