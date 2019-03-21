<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">{{title}}</h6>
    </div>
    <div class="card-body">
      <form @submit.prevent="onSubmit">
        <div class="row">
          <div class="col-12 col-lg-12 col-xl-12">
            <div class="form-group">
              <label class="mb-0" for="name">
                Nombre del formulario
                <span class="i-required">*</span>
              </label>
              <input
                v-validate="'required'"
                name="name"
                type="text"
                class="form-control"
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
                v-validate="'required'"
                name="formTitle"
                type="text"
                class="form-control"
                placeholder="Digite el título del formulario"
                v-model="formHd.title"
              >
            </div>
            <div class="form-group">
              <label class="mb-0" for="landings">
                Landing pages asociados
                <span class="i-required">*</span>
              </label>
              <multiselect
                v-validate="'required'"
                name="landings"
                placeholder="Agregar landing pages"
                :value="values"
                :options="options"
                label="name"
                track-by="id"
                :multiple="true"
                :searchable="false"
                @input="updateValueAction"
              ></multiselect>
              <!-- <multiselect
                v-validate="'required'"
                name="landings"
                v-model="values"
                placeholder="Agregar landing pages"
                label="name"
                track-by="id"
                :options="options"
                :multiple="true"
                :taggable="true"
                @tag="addTag"
                @input="updateValueAction"
              ></multiselect>-->
            </div>

            <div class="form-group" v-if="formHd.id">
              <label class="mb-0" for>Ubicación del archivo a subir</label>
              <input
                type="file"
                class="w-100"
                id="file"
                ref="file"
                v-on:change="handleFileUpload()"
              >
            </div>

            <div class="form-group">
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
          </div>
          <div class="col-12 col-lg-12 col-xl-12">
            <div class="d-flex justify-content-between align-items-center">
              <strong>Detalle del Formulario</strong>
              <button type="button" class="btn btn-link btn-sm" @click="onNew">
                <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
              </button>
            </div>
            <table class="table table-striped">
              <tbody>
                <tr v-for="(item, index) in formHd.formDetails" :key="index">
                  <td style="width:20px;">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        :id="'cboIsActives'+ index"
                        v-model="item.isActive"
                      >
                      <label class="custom-control-label" :for="'cboIsActives'+ index"></label>
                    </div>
                  </td>
                  <td>{{ item.fieldLabel}}</td>
                  <td class="text-right">
                    <a role="button" class="btn btn-link btn-sm" @click="onEdit(item)">editar</a>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="col-12">
            <div class="form-group text-right">
              <button
                type="button"
                class="btn btn-outline-warning btn-sm mx-1"
                @click="onCancel"
              >CANCELAR</button>
              <button type="submit" class="btn btn-primary btn-sm" :disabled="!isFormValid">GUARDAR</button>
              <!-- <button type="button" @click="test()">ALERT</button> -->
            </div>

            <div class="row">
              <div class="col-6">
                <!-- <pre class="language-json"><code>{{ options  }}</code></pre> -->
              </div>
              <div class="col-6">
                <!-- <pre class="language-json"><code>{{ values  }}</code></pre> -->
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>

    <!-- Modal -->
    <div
      class="modal fade"
      id="exampleModal"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header py-1">
            <h5 class="modal-title" id="exampleModalLabel">Detalle del formulario</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <FormItemNew :formItem="selectedItem" :action="action"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Multiselect from "vue-multiselect";
import { mapState, mapActions, mapGetters } from "vuex";
import FormItemNew from "@/components/FormItemNew.vue";

export default {
  components: { Multiselect,FormItemNew },
  data() {
    return {
      title: "ALTA FORMULARIO",
      file: {},
      action: "read",
      selectedItem: {}
    };
  },
  created() {
    this.$store.dispatch("loadOptions");
  },
  mounted() {
    window.$("#exampleModal").on("hidden.bs.modal", function() {
      this.selectedItem = {};
    });
  },
  computed: {
    ...mapState(["values"]),
    ...mapGetters(["formHd", "options", "values"]),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid);
    }
  },
  methods: {
    ...mapActions(["updateValueAction"]),
    handleFileUpload() {},
    onSubmit() {
      var formHd = this.$store.getters.formHd;
      let hasDetail = formHd.formDetails.filter(x => x.isActive == true);

      if (hasDetail && hasDetail.length) {
        formHd.formHdLandingPage = this.values.map(item => {
          return {
            landingPageId: item.id,
            formHdId: formHd.id,
            isActive: true
          };
        });

        axios({ method: "POST", url: "api-forms", data: formHd })
          .then(response => {
            if (response && response.data && response.data.id) {
              formHd.id = response.data.id;
              console.log(formHd);
              this.updateStore(formHd);
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
          .catch(err => {
            console.log(err);
            this.$swal("No se pudo dar de alta al formulario", {
              icon: "warning"
            });
          });
      } else {
        this.$swal({
          icon: "warning",
          dangerMode: true,
          text: "Favor seleccionar detalle del formulario."
        });
      }
    },
    updateStore(formData) {
      let action = this.$store.getters.fAction;
      if (action == "create") {
        this.$store.commit("addFormHds", formData);
        this.$store.commit("updateValues", []);
      }
    },
    onEdit(item) {
      console.log("onEdit", item);
      this.selectedItem = item;
      this.action = "update";
      this.$store.state.isOptSelected = false;
      if (item && item.fieldTypeId == "selectList") {
        this.$store.state.isOptSelected = true;
      }
      window.$("#exampleModal").modal("show");
    },
    onNew() {
      console.log("onNew");
      this.action = "create";
      this.selectedItem = {
        fieldTypeId: "textInput",
        isActive: true,
        order: 1,
        isRequired: true,
        formHdId: this.formHd.id
      };
      this.$store.state.isOptSelected = false;
      window.$("#exampleModal").modal("show");
    },

    onCancel() {
      this.title = "ALTA FORMULARIO";
      this.$store.commit("setfAction", "read");
      this.$store.state.isOptSelected = false;
      this.$store.commit("setFormHd", {});
    }
  }
};
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style lang="scss">
.multiselect__tags {
  padding: 6px 40px 0 10px;
  border: 1px solid #ced4da;
}
.multiselect__tag {
  padding: 4px 26px 5px 10px;
  color: #fff;
  background: #41b883;
  margin-bottom: 0px;
}
.is-danger input {
  border: 1px solid red;
}
.swal-modal {
  width: 250px;
}
.custom-control-label {
  position: relative;
  margin-bottom: 17px;
}
</style>
