<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">ALTA LANDING PAGE</h6>
    </div>
    <div class="card-body">
      <form @submit.prevent="onSubmit">
        <div class="form-group">
          <label class="mb-0" for="name">
            Nombre del landing page
            <span class="i-required">*</span>
          </label>
          <input
            v-validate="'required'"
            type="text"
            class="form-control"
            name="name"
            maxlength="150"
            placeholder="Digite el nombre del landing page"
            v-model="landingItem.name"
          >
        </div>

        <div class="form-group">
          <label class="mb-0" for="description">Descripción del landing page</label>
          <textarea
            class="form-control"
            name="description"
            maxlength="400"
            placeholder="Digite una descripción del landing page"
            v-model="landingItem.description"
          ></textarea>
        </div>

        <div class="row">
          <div class="col-md-4">
            <div class="custom-control custom-checkbox mb-3">
              <input
                type="checkbox"
                class="custom-control-input"
                id="customControlValidation1"
                v-model="landingItem.isActive"
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
              <button type="submit" class="btn btn-primary btn-sm" :disabled="!isFormValid">GUARDAR</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  props: ["title", "landingItem"],
  computed: {
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid);
    }
  },
  methods: {
    onSubmit() {
      axios
        .post("api-landingpage", this.landingItem)
        .then(response => {
          if (response && response.data && response.data.id) {
            this.landingItem.id = response.data.id;
            this.$emit("onItemSaved", this.landingItem);
            this.$swal(response.data.message, {
              icon: "success"
            });
            this.onCancel();
          } else {
            this.$swal(response.data.message, {
              icon: "warning"
            });
          }
        })
        .catch(error => {
          console.log(error);
          this.$swal("No se pudo dar de alta al landing page", {
            icon: "warning"
          });
        });
    },
    onCancel() {
      this.$emit("onCancel");
    }
  }
};
</script>
 