<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">{{title}}</h6>
    </div>
    <div class="card-body">
      <form @submit.prevent="onSubmit">
        <div class="row">
          <div class="col-12 col-lg-6">
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
                v-model="values"
                placeholder="Agregar landing pages"
                label="name"
                track-by="id"
                :options="ddlLandings"
                :multiple="true"
                :taggable="true"
              ></multiselect>
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
          <div class="col-12 col-lg-6">
            <p class="mb-0">Detalle</p>
            <table class="table table-striped">
              <tbody>
                <tr v-for="(item, index) in formHd.formDetails" :key="index">
                  <td>
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
                  <td>
                    <a href="#">editar</a>
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
                <pre class="language-json"><code>{{ values  }}</code></pre>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Multiselect from "vue-multiselect";
import { mapState } from "vuex";
export default {
  components: { Multiselect },
  data() {
    return {
      title: "ALTA FORMULARIO",
      file: {},
      values: []
    };
  },
  beforeMount() {
    this.load();
  },
  mounted() {
    var data = this.setDdlLandings;
    this.values = data;
    console.log("data", data);
  },
  computed: {
    ...mapState(["formHd", "ddlLandings"]),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid);
    },
    setDdlLandings() {
      //this.title = "ACTUALIZA FORMULARIO";
      var _values = []
      if (this.$store.state.fhAction === "update") {
        
        if (
          this.formHd &&
          this.formHd.formHdLandingPage &&
          this.formHd.formHdLandingPage.length
        ) {
          this.formHd.formHdLandingPage.forEach(element => {
            var landing = this.ddlLandings.find(
              x => x.id == element.landingPageId
            );
            if (landing) {
              _values.push(landing);
            }
          });
        }
      }
      return _values
    }
  },
  methods: {
    load() {
      axios.get("api-landingpage").then(response => {
        this.$store.state.ddlLandings = response.data;
      });
    },
    handleFileUpload() {},
    onSubmit() {
      let formHd = this.$store.state.formHd;
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
      let action = this.$store.state.fhAction;
      if (action == "create") {
        this.$store.state.formHds.push(formData);
      }
    },
    onCancel() {
      this.title = "ALTA FORMULARIO";
      this.$store.state.fhAction = "read";
      this.$store.state.formHd = {};
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
