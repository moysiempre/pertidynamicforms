<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">{{title}}</h6>
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
            v-model="landingPage.name"
          >
        </div>

        <div class="form-group">
          <label class="mb-0" for="description">Descripción del landing page</label>
          <textarea
            class="form-control"
            name="description"
            maxlength="400"
            placeholder="Digite una descripción del landing page"
            v-model="landingPage.description"
          ></textarea>
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
              <button type="submit" class="btn btn-primary btn-sm" :disabled="!isFormValid">GUARDAR</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { mapState } from "vuex";
export default {
  props: ["title"],
  computed: {
    ...mapState({
      landingPage: state => state.landings.landingPage,
      action: state => state.landings.action
    }),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid);
    }
  },
  methods: {
    onSubmit() {
      this.$store
        .dispatch("createOrUpdatelanding", this.landingPage)
        .then(response => {
          if (response && response.data && response.data.id) {
            if (this.action === "create") {
              this.landingPage.id = response.data.id;
              this.$store.commit("ADD_LANDING", this.landingPage);
            }
            this.$swal(response.data.message, {
              icon: "success"
            });
          } else {
            this.$swal(response.data.message, {
              icon: "warning"
            });
          }
          this.onCancel();
        })
        .catch(error => {
          console.log(error);
          this.$swal("No se pudo dar de alta al landing page", {
            icon: "warning"
          });
        });
    },
    onCancel() {
      this.$store.commit("SET_ACTION", "read");
      this.$store.commit("SET_LANDING", {});
    },
     
  }
};
</script>
 