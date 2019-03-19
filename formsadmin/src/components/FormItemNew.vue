<template>
  <div>
    <form id="factoryForm" class="mt-3">
      <div class="row">
        <div class="col-md-5">
          <div class="form-group">
            <label for="fieldTypeId" class="mb-0">Tipo de entrada</label>
            <select id="fieldTypeId" class="custom-select" v-model="formItem.fieldTypeId">
              <option
                v-for="(item, index) in fieldTypes"
                :key="index"
                :value="item.fieldTypeId"
              >{{item.fieldLabel}}</option>
            </select>
          </div>
        </div>

        <div class="col-md-5">
          <div class="form-group">
            <label for="fieldLabel" class="mb-0">Etiqueta</label>
            <input
              type="text"
              class="form-control"
              id="fieldLabel"
              placeholder="digite la etiqueta"
              v-model="formItem.fieldLabel"
            >
          </div>
        </div>
        <div class="col-md-2">
          <div class="form-group">
            <label for="order" class="mb-0">Posición</label>
            <input
              type="number"
              id="order"
              class="form-control"
              value="1"
              min="1"
              v-model="formItem.order"
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
            <button hidden type="button" class="btn btn-outline-warning btn-sm mx-1">RESETEAR</button>
            
            <button
              type="button"
              class="btn btn-outline-warning btn-sm mx-1"
              data-dismiss="modal"
            >SALIR</button>
            
            <button type="button" class="btn btn-dark btn-sm px-3" @click="onSubmit">GUARDAR</button>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>
<script>
import { mapState } from "vuex";
import axios from "axios";
export default {
  props: ["formItem"],
  data() {
    return {
      fieldTypes: []
    };
  },
  beforeMount() {
    this.load();
  },
  computed: {
    ...mapState(["baseDetails"])
  },
  methods: {
    load() {
      axios.get("api-forms/fieldTypes").then(response => {
        this.fieldTypes = response.data;
      });
    },
    onSubmit() {
      console.log("OK");
    }
  }
};
</script>

