<template>
  <div class="card border-light">
    <div class="card-header">
      <h6 class="my-0">{{ title }}</h6>
      <button class="btn btn-link close" @click="onCancel">
        <i class="pe-7s-close fa-2x"></i>
      </button>
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
                maxlength="150"
                placeholder="Digite el nombre del formulario"
                v-model="formHd.name"
              />
            </div>
            <div class="form-group">
              <label class="mb-0" for="formTitle">
                <span>Título del formulario</span>
                <span class="i-required">*</span>
              </label>
              <input
                v-validate="'required'"
                name="formTitle"
                type="text"
                class="form-control"
                maxlength="150"
                placeholder="Digite el título del formulario"
                v-model="formHd.title"
              />
            </div>
            <div class="form-group">
              <label class="mb-0" for="landings">Landing pages asociados</label>
              <multiselect
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
            </div>

            <div class="form-group">
              <label class="mb-0" for="description"
                >Plantilla E-mail asociados</label
              >
              <select class="custom-select" v-model="formHd.mailTemplateId">
                <option
                  v-for="(item, index) in mailtemplateList"
                  :key="index"
                  :value="item.id"
                  >{{ item.name }}</option
                >
              </select>
            </div>

            <div class="form-group mb-2">
              <label class="mb-0" for>Ubicación del archivo a subir</label>
              <div class="custom-file">
                <input
                  type="file"
                  id="validatedCustomFile"
                  ref="file"
                  class="custom-file-input"
                  v-on:change="handleFileUpload"
                />
                <label class="custom-file-label" for="validatedCustomFile">
                  <span v-if="!formHd.filePath">Seleccione un archivo...</span>
                  <span v-if="formHd.filePath">{{ formHd.filePath }}</span>
                </label>
              </div>
            </div>

            <div class="form-group" v-if="formHd.id">
              <button
                type="button"
                class="btn btn-warning btn-sm mr-2"
                v-if="formHd && formHd.filePath && formHd.filePath.length"
                :disabled="isdeleting"
                @click="onRemoveFile"
              >
                <span>Eliminar archivo</span>
                <btn-loader :isloading="isdeleting" />
              </button>
              <button
                type="button"
                class="btn btn-secondary btn-sm mr-2"
                v-if="formHd && formHd.filePath && formHd.filePath.length"
                :disabled="isdownloading"
                @click="onDownloadFile(formHd.id, formHd.filePath)"
              >
                <span>Descargar archivo</span>
                <btn-loader :isloading="isdownloading" />
              </button>
            </div>
            <div class="form-group">
              <div class="custom-control custom-checkbox mb-3">
                <input
                  type="checkbox"
                  class="custom-control-input"
                  id="cboIsActive"
                  v-model="formHd.isActive"
                />
                <label class="custom-control-label" for="cboIsActive"
                  >Es Activo</label
                >
              </div>
            </div>
          </div>

          <div class="col-12 col-lg-12 col-xl-12">
            <div class="d-flex justify-content-between align-items-center">
              <strong>Detalle del Formulario</strong>
              <button
                type="button"
                class="btn btn-link btn-sm"
                @click="onNew"
                v-if="action == 'update'"
              >
                <i
                  class="pe-7s-close pe-rotate-45"
                  style="font-size:1.5rem"
                ></i>
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
                        :id="'cboIsActives' + index"
                        :disabled="item.isMandatory"
                        v-model="item.isActive"
                      />
                      <label
                        class="custom-control-label"
                        :for="'cboIsActives' + index"
                      ></label>
                    </div>
                  </td>
                  <td>{{ item.fieldLabel }}</td>
                  <td>{{ item.fieldType }}</td>
                  <td class="text-right">
                    <a
                      role="button"
                      class="btn btn-link btn-sm"
                      @click="onEdit(item)"
                      >editar</a
                    >
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
              >
                CANCELAR
              </button>
              <button
                type="submit"
                class="btn btn-primary btn-sm"
                :disabled="!isFormValid || isloading"
              >
                <span>GUARDAR</span>
                <btn-loader :isloading="isloading" />
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>

    <!-- begin Modal -->
    <div
      class="modal fade"
      id="formNewModal"
      tabindex="-1"
      role="dialog"
      aria-labelledby="formNewModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header py-1">
            <h5 class="modal-title" id="formNewModalLabel">
              Detalle del formulario
            </h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <create-update-form-item
              :formItem="selectedItem"
              :action="iaction"
            />
          </div>
        </div>
      </div>
    </div>
    <!-- end Modal -->
  </div>
</template>

<script>
import axios from 'axios'
import Multiselect from 'vue-multiselect'
import { mapState, mapActions } from 'vuex'
import CreateUpdateFormItem from '@/components/CreateUpdateFormItem.vue'
import BtnLoader from '@/components/BtnLoader.vue'

export default {
  props: ['title'],
  components: { Multiselect, BtnLoader, CreateUpdateFormItem },
  data() {
    return {
      file: '',
      selectedItem: {},
      isloading: false,
      isdeleting: false,
      isdownloading: false,
      iaction: 'read'
    }
  },
  mounted() {
    window.$('#formNewModal').on('hidden.bs.modal', function() {
      this.selectedItem = {}
    })
  },
  computed: {
    ...mapState({
      formHd: state => state.forms.formHd,
      action: state => state.forms.action,
      values: state => state.forms.values,
      options: state => state.forms.options,
      mailtemplates: state => state.templates.mailtemplates
    }),
    isFormValid() {
      return !Object.keys(this.fields).some(key => this.fields[key].invalid)
    },
    mailtemplateList() {
      return this.mailtemplates && this.mailtemplates.length
        ? this.mailtemplates.filter(x => x.isActive)
        : []
    }
  },
  methods: {
    ...mapActions(['updateValueAction']),
    handleFileUpload() {
      let formData = new FormData()
      this.file = this.$refs.file.files[0]
      formData.append('file', this.file)
      formData.append('formHdId', this.formHd.id)

      axios
        .post('api-filemanager/upload', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
        .then(response => {
          if (response && response.data && response.data.success) {
            this.formHd.filePath = this.file.name
            this.$swal(response.data.message, {
              icon: 'success',
              closeOnClickOutside: false
            })
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(() => {
          //console.log(err)
          this.$swal('No se pudo dar de alta al formulario', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onSubmit() {
      let hasDetail = this.formHd.formDetails.filter(x => x.isActive == true)
      if (hasDetail && hasDetail.length) {
        this.isloading = true
        //var _values = this.$store.state.forms.values
        //console.log('COMPARE VALUES: ', _values, this.values)
        this.formHd.landingPages = this.values.map(item => {
          return {
            id: item.id,
            name: '[name]',
            formHdId: this.formHd.id,
            isActive: true
          }
        })

        // hasDetail.forEach(element => {
        //   if (element.ddlCatalogs && this.action === "create") {
        //     for (let item in element.ddlCatalogs) {
        //       var ert = element.ddlCatalogs[item];
        //       ert.formDetailId = "";
        //       console.log(ert);
        //     }
        //   }
        // });

        axios({ method: 'POST', url: 'api-forms', data: this.formHd })
          .then(response => {
            this.isloading = false
            if (response && response.data && response.data.id) {
              this.formHd.id = response.data.id
              this.updateStore()
              this.$swal(response.data.message, {
                icon: 'success',
                closeOnClickOutside: false
              })
              this.onCancel()
            } else {
              //console.log('RESPONSE: ', response)
              this.$swal(response.data.message, {
                icon: 'warning',
                closeOnClickOutside: false
              })
            }
          })
          .catch(() => {
            this.isloading = false
            //console.log(err)
            this.$swal('No se pudo dar de alta al formulario', {
              icon: 'warning',
              closeOnClickOutside: false
            })
          })
      } else {
        this.$swal({
          icon: 'warning',
          dangerMode: true,
          text: 'Favor seleccionar detalle del formulario.',
          closeOnClickOutside: false
        })
      }
    },
    updateStore() {
      if (this.action === 'create') {
        //this.$store.commit("ADD_FORM_HD", item);
        //console.log(item)
        this.$store.dispatch('loadFormHds')
      }
      this.$store.dispatch('loadBaseDetails')
      this.$store.commit('SET_VALUES', [])
    },
    onEdit(item) {
      this.selectedItem = item
      //console.log('item', item)
      this.iaction = 'update'
      this.$store.commit('SET_OPT_SELECTED', false)
      if (item && item.fieldTypeId == 'select') {
        this.$store.commit('SET_OPT_SELECTED', true)
      }
      window.$('#formNewModal').modal({
        backdrop: 'static'
      })
    },
    onNew() {
      this.iaction = 'create'
      this.selectedItem = {
        fieldTypeId: 'text',
        isActive: true,
        order: 1,
        isRequired: true,
        formHdId: this.formHd.id
      }
      this.$store.commit('SET_OPT_SELECTED', false)
      window.$('#formNewModal').modal({
        backdrop: 'static'
      })
    },
    onRemoveFile() {
      this.isdeleting = true
      var formHdId = this.formHd.id
      var fileName = this.formHd.filePath
      axios({
        method: 'DELETE',
        url: `api-filemanager/remove/${formHdId}/${fileName}`
      })
        .then(response => {
          this.isdeleting = false
          if (response && response.data && response.data.success) {
            this.formHd.filePath = ''
            this.$swal(response.data.message, {
              icon: 'success'
            })
          } else {
            this.$swal(response.data.message, {
              icon: 'warning',
              closeOnClickOutside: false
            })
          }
        })
        .catch(() => {
          // console.log(err)
          this.isdeleting = false
          this.$swal('No se pudo eliminar el archivo', {
            icon: 'warning',
            closeOnClickOutside: false
          })
        })
    },
    onDownloadFile(formHdId, fileName) {
      this.isdownloading = true
      axios({
        url: `api-filemanager/download/${formHdId}/${fileName}`,
        method: 'GET',
        responseType: 'blob' // important
      })
        .then(response => {
          this.isdownloading = false
          if (response.status === 200 && response.data.size > 0) {
            const url = window.URL.createObjectURL(new Blob([response.data]))
            const link = document.createElement('a')
            link.href = url
            link.setAttribute('download', fileName) //or any other extension
            document.body.appendChild(link)
            link.click()
          }
        })
        .catch(() => {
          this.isdownloading = false
        })
    },
    onCancel() {
      this.$store.commit('SET_ACTION', 'read')
      this.$store.commit('SET_FORM_HD', {})
      this.$store.commit('SET_OPT_SELECTED', false)
      //reset form
      this.$store.dispatch('loadFormHds')
      this.$store.dispatch('loadOptions')
    }
  }
}
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
.custom-file-label::after {
  content: 'Buscar' !important;
}
</style>
