<template>
  <div>
    <div class="row">
      <div class="col-12 bg-light">
        <form id="factoryForm" class="mt-3 bg-light">
          <div class="row no-gutters">
            <div class="col-md-5">
              <div class="form-group mx-1">
                <label for="fieldTypeId" class="mb-0">
                  Tipo de entrada
                  <span class="i-required">*</span>
                </label>

                <select
                  v-if="action === 'update'"
                  v-validate="'required'"
                  id="fieldTypeId"
                  name="fieldTypeId"
                  class="custom-select"
                  v-model="formItem.fieldTypeId"
                  disabled
                  @change="onChangeOption($event)"
                >
                  <option
                    v-for="(item, index) in fieldTypes_s"
                    :key="index"
                    :value="item.fieldTypeId"
                    >{{ item.fieldLabel }}</option
                  >
                </select>
                <select
                  v-if="action === 'create'"
                  v-validate="'required'"
                  id="fieldTypeId"
                  name="fieldTypeId"
                  class="custom-select"
                  v-model="formItem.fieldTypeId"
                  @change="onChangeOption($event)"
                >
                  <option
                    v-for="(item, index) in fieldTypes"
                    :key="index"
                    :value="item.fieldTypeId"
                    >{{ item.fieldLabel }}</option
                  >
                </select>
              </div>
            </div>

            <div class="col-md-5">
              <div class="form-group mx-1">
                <label for="fieldLabel" class="mb-0">
                  Etiqueta
                  <span class="i-required">*</span>
                </label>
                <input
                  v-validate="'required'"
                  type="text"
                  class="form-control"
                  id="fieldLabel"
                  name="fieldLabel"
                  maxlength="150"
                  placeholder="digite la etiqueta"
                  v-model="formItem.fieldLabel"
                />
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-group mx-1">
                <label for="order" class="mb-0">Posición</label>
                <input
                  type="number"
                  id="order"
                  name="order"
                  class="form-control"
                  value="1"
                  min="1"
                  v-model="formItem.order"
                />
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
                  />
                  <label class="custom-control-label" for="cboIsRequired"
                    >Es obligatorio</label
                  >
                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="form-group text-right">
                <button
                  hidden
                  type="button"
                  class="btn btn-outline-warning btn-sm mx-1"
                >
                  RESETEAR
                </button>

                <button
                  type="button"
                  class="btn btn-outline-warning btn-sm mx-1"
                  data-dismiss="modal"
                >
                  SALIR
                </button>

                <button
                  type="button"
                  data-dismiss="modal"
                  class="btn btn-dark btn-sm px-3"
                  :disabled="!isFormItemValid || isloading"
                  v-if="action == 'update'"
                >
                  <span>GUARDAR</span>
                </button>
                <button
                  type="button"
                  class="btn btn-dark btn-sm px-3"
                  @click="onSubmit"
                  :disabled="!isFormItemValid || isloading"
                  v-if="action == 'create'"
                >
                  <span>GUARDAR</span>
                  <btn-loader :isloading="isloading" />
                </button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-12">
        <ul class="list-group" v-if="isOptSelected">
          <li class="list-group-item bg-light py-1">
            <small>
              <strong>DESCRIPCIÓN</strong>
            </small>
          </li>
          <li class="list-group-item bg-light py-1">
            <form @submit.prevent="onSubmitItemOpt">
              <div class="input-group mb-0 search-by">
                <input
                  type="text"
                  class="form-control"
                  placeholder="agregar opciones"
                  v-model="ddlCatalogItem.name"
                />
                <div class="input-group-append">
                  <button type="submit" class="btn btn-primary btn-sm">
                    <span>GUARDAR</span>
                    <btn-loader :isloading="isloadingCat" />
                  </button>
                </div>
              </div>
            </form>
          </li>
          <li
            class="list-group-item py-0"
            v-for="(cat, index) in formItem.ddlCatalogs"
            :key="index"
          >
            <div class="d-flex justify-content-between align-items-center">
              <!-- <span>{{ cat.name }}</span> -->
              <span v-html="cat.name"></span>
              <div>
                <a
                  role="button"
                  class="btn btn-link btn-sm"
                  @click="onEditDDLCatItem(cat)"
                >
                  <span>editar</span>
                </a>
                <a
                  role="button"
                  class="btn btn-link btn-sm"
                  @click="onDeleteDDLCatItem(cat.id, index)"
                >
                  <span class="text-danger">eliminar</span>
                </a>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex'
import axios from 'axios'
import BtnLoader from '@/components/BtnLoader.vue'

export default {
  props: ['formItem', 'action'],
  components: { BtnLoader },
  data() {
    return {
      fieldTypes: [],
      ddlCatalogItem: {},
      ddlCatAction: 'create',
      isloading: false,
      isloadingCat: false
    }
  },
  beforeMount() {
    this.load()
  },
  mounted() {},
  computed: {
    ...mapState({
      baseDetails: state => state.forms.baseDetails,
      isOptSelected: state => state.forms.isOptSelected
    }),
    isFormItemValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid)
    },
    fieldTypes_s() {
      return this.baseDetails
    }
  },
  methods: {
    load() {
      axios.get('api-forms/fieldTypes').then(response => {
        this.fieldTypes = response.data
      })
    },
    onChangeOption(event) {
      var value = event.target.value
      this.$store.commit('SET_OPT_SELECTED', false)
      if (value == 'select' && this.action == 'update') {
        this.$store.commit('SET_OPT_SELECTED', true)
      }
    },
    onSubmit() {
      this.isloading = true
      axios({ method: 'POST', url: 'api-forms/detail', data: this.formItem })
        .then(response => {
          this.isloading = false
          if (response && response.data && response.data.id) {
            if (this.action == 'create') {
              this.formItem.id = response.data.id
              this.setFieldType()
              this.$store.commit('ADD_DETAIL_ITEM', this.formItem)
            }
            this.$store.commit('SET_OPT_SELECTED', false)
            this.$swal(response.data.message, {
              icon: 'success',
              closeOnClickOutside: false
            })
            this.onCancel()
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(err => {
          this.isloading = false
          console.log(err)
          this.$swal('No se pudo dar de alta al formulario', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onCancel() {
      window.$('#formNewModal').modal('hide')
    },
    setFieldType() {
      var ft = this.formItem.fieldTypeId
      switch (ft) {
        case 'numeric':
          this.formItem.fieldType = 'Numérico'
          break
        case 'text':
          this.formItem.fieldType = 'Texto'
          break
        case 'select':
          this.formItem.fieldType = 'Selección'
          break
      }
    },
    onSubmitItemOpt() {
      if (!this.ddlCatalogItem.name) {
        this.$swal('Favor digite el nombre del catálogo', {
          icon: 'warning',
          closeOnClickOutside: false
        })
        return
      }
      this.isloadingCat = true
      const id = this.formItem.id
      if (this.ddlCatAction == 'create') {
        this.ddlCatalogItem.formDetailId = id
      }
      axios({ method: 'POST', url: 'api-catalog', data: this.ddlCatalogItem })
        .then(response => {
          this.isloadingCat = false
          if (response && response.data && response.data.id) {
            if (this.ddlCatAction == 'create') {
              this.ddlCatalogItem.id = response.data.id

              if (
                !this.formItem.ddlCatalogs ||
                !this.formItem.ddlCatalogs.length
              ) {
                this.formItem.ddlCatalogs = []
              }

              let cat = Object.assign({}, this.ddlCatalogItem)
              this.formItem.ddlCatalogs.push(cat)
            }
            //clear inputs
            this.ddlCatalogItem = {}
            this.$swal(response.data.message, {
              icon: 'success',
              closeOnClickOutside: false
            })
            //this.onCancel();
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(err => {
          this.isloadingCat = false
          console.log(err)
          this.$swal('No se pudo dar de alta al formulario', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onEditDDLCatItem(item) {
      this.ddlCatAction = 'update'
      this.ddlCatalogItem = item
    },
    onDeleteDDLCatItem(id, index) {
      axios({ method: 'DELETE', url: 'api-catalog/' + id })
        .then(response => {
          if (response && response.data && response.data.id) {
            console.log('DELETE: ', response.data.id, this.formItem.ddlCatalogs)
            if (
              this.formItem &&
              this.formItem.ddlCatalogs &&
              this.formItem.ddlCatalogs.length
            ) {
              const catalog = this.formItem.ddlCatalogs.find(x => x.id == id)
              if (catalog) {
                this.$delete(this.formItem.ddlCatalogs, index)
              }
            }
            this.$swal(response.data.message, {
              icon: 'success',
              closeOnClickOutside: false
            })

            //this.onCancel();
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(err => {
          console.log(err)
          this.$swal('No se pudo dar de alta al formulario', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    }
  }
}
</script>
