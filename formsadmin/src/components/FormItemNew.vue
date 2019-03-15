<template>
  <div>
    <div class="card border-light bg-xlight" hidden>
      <div class="card-body">
        <h6 class="mt-0">{{title}}</h6>
      </div>
    </div>
    <div class="bg-white">
      <table
        class="table table-striped"
        v-if="formHd && formHd.formDetails && formHd.formDetails.length"
      >
        <thead>
          <tr>
            <th colspan="5" class="text-right my-1">
              <button
                type="button"
                class="btn btn-link btn-sm"
                data-toggle="modal"
                data-target="#exampleModal"
              >Agregar Detalle</button>
            </th>
          </tr>
          <tr>
            <th>Etiqueta</th>
            <th hidden>Tipo</th>
            <th>Posición</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in formHd.formDetails" :key="index">
            <td>{{item.fieldLabel}}</td>
            <td hidden>{{item.fieldTypeId}}</td>
            <td>{{item.order}}</td>
            <td class="text-right">
              <a
                role="button"
                v-if="item.operation !== 'update'"
                class="btn btn-link btn-sm"
                @click="onEdit(item)"
              >Editar</a>
              <a
                role="button"
                v-if="item.operation == 'update'"
                class="btn btn-link btn-sm text-warning"
                @click="onCancel(item)"
              >Cancelar</a>
              <a
                role="button"
                v-if="item.operation !== 'update'"
                class="btn btn-link btn-sm text-warning"
                @click="onDelete(item.id)"
                hidden
              >Inhabilitar</a>
            </td>
            <td>
              <input type="checkbox" name id>
            </td>
          </tr>
        </tbody>
      </table>
      <p v-else class="alert alert-warning">No registro encontrado</p>
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
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form id="factoryForm" class="mt-3">
              <div class="row">
                <div class="col-md-5">
                  <div class="form-group" :class="{invalid: $v.formItem.fieldTypeId.$error}">
                    <label for="fieldTypeId" class="mb-0">Tipo de entrada</label>
                    <select id="fieldTypeId" class="custom-select" v-model="formItem.fieldTypeId">
                      <option
                        v-for="(item, index) in fieldTypes"
                        :key="index"
                        :value="item.name"
                      >{{item.description}}</option>
                    </select>
                  </div>
                </div>

                <div class="col-md-5">
                  <div class="form-group" :class="{invalid: $v.formItem.fieldLabel.$error}">
                    <label for="fieldLabel" class="mb-0">Etiqueta</label>
                    <input
                      type="text"
                      class="form-control"
                      id="fieldLabel"
                      placeholder="digite la etiqueta"
                      v-model="formItem.fieldLabel"
                      @blur="$v.formItem.fieldLabel.$touch()"
                    >
                  </div>
                </div>
                <div class="col-md-2">
                  <div class="form-group" :class="{invalid: $v.formItem.order.$error}">
                    <label for="order" class="mb-0">Posición</label>
                    <input
                      type="number"
                      id="order"
                      class="form-control"
                      value="1"
                      min="1"
                      v-model="formItem.order"
                      @blur="$v.formItem.order.$touch()"
                    >
                  </div>
                </div>
              </div>

              <div class="row">
                <div class="col-md-6">
                  <div class="d-flex justify-content-between">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cboIsRequired"
                        v-model="formItem.isRequired"
                      >
                      <label class="custom-control-label" for="cboIsRequired">Es obligatorio</label>
                    </div>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group text-right">
                    <button
                      type="button"
                      class="btn btn-outline-warning btn-sm mx-1"
                      @click="onReset()"
                    >RESETEAR</button>
                    <button
                      type="button"
                      class="btn btn-dark btn-sm px-3"
                      :disabled="$v.formItem.$invalid"
                      @click="onSubmit"
                    >GUARDAR</button>
                  </div>
                </div>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { required } from "vuelidate/lib/validators";
import { mapState } from "vuex";
import axios from "axios";

export default {
  data() {
    return {
      title: "DETALLE FORMULARIO"
    };
  },
  computed: {
    ...mapState(["fhAction", "fdAction", "fieldTypes", "formItem", "formHd"])
  },
  validations: {
    formItem: {
      fieldTypeId: { required },
      fieldLabel: { required },
      order: { required }
    }
  },
  methods: {
    loadById(id) {
      axios.get("api-forms/" + id).then(response => {
        this.$store.state.formHd = response.data;
      });
    },
    onReset() {
      let _formItem = Object.assign({}, this.$store.state.formItem);
      let formHdId = _formItem.formHdId;
      this.$store.state.formItem = {
        fieldLabel: "",
        fieldTypeId: "textInput",
        order: 1,
        formHdId: formHdId,
        isRequired: false
      };
      this.$v.formItem.$reset();
      this.$store.state.formItem.operation = "read";
    },
    onSubmit() {
      let _formItem = Object.assign({}, this.$store.state.formItem);
      if (this.formItem && this.formItem.fieldTypeId) {
        axios({ method: "POST", url: "api-forms/detail", data: _formItem })
          .then(response => {
            if (response && response.data && response.data.id) {
              this.formItem.id = response.data.id;
              this.$snotify.success(response.data.message);
              this.loadById(this.formHd.id);
              this.onReset();
            } else {
              this.$snotify.warning(response.data.message);
            }
          })
          .catch(err => {
            console.log(err);
            this.$snotify.error("this.body for snotify");
          });
      } else {
        this.$snotify.warning("this.body for snotify");
      }
    },
    onEdit(selectedItem) {
      selectedItem.operation = "update";
      this.$store.state.formItem = selectedItem;
    },
    onCancel(selectedItem) {
      selectedItem.operation = "read";
      this.$store.state.formItem = selectedItem;
      this.onReset();
    },
    onDelete(id) {
      axios({ method: "DELETE", url: "api-forms/detail/" + id })
        .then(response => {
          if (response && response.data && response.data.id) {
            this.$snotify.success(response.data.message);
            this.loadById(this.formHd.id);
            this.onReset();
          } else {
            this.$snotify.warning(response.data.message);
          }
        })
        .catch(err => {
          console.log(err);
          this.$snotify.error("this.body for snotify");
        });
    }
  }
};
</script>
<style lang="scss" scoped>
.btn-link {
  &.text-danger {
    color: #dc3545 !important;
  }
  &.text-warning {
    color: #ffc107 !important;
  }
}
</style>
 