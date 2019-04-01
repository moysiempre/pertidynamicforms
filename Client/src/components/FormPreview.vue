<template>
  <div>
    <form id="contactForm">
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-header bg-white">
              <h5 class="text-center">{{ fields.title }}</h5>
            </div>
            <div class="card-body bg-light p-2">
              <div class="container-fluid bg-white pt-3 pb-3">
                <form action>
                  <div
                    v-for="(field, index) in fields.formDetails"
                    :key="index"
                  >
                    <div class="form-group" v-if="field.fieldTypeId === 'name'">
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <input
                        v-validate="'required'"
                        :name="field.name"
                        :id="field.name"
                        type="text"
                        class="form-control"
                        :placeholder="field.fieldLabel"
                        v-model="formData.name"
                      />
                      <span class="text-danger">
                        {{ getErrMsg(errors, field.name) }}
                      </span>
                    </div>
                    <div class="form-group" v-if="field.fieldTypeId === 'text'">
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <input
                        v-validate="'required'"
                        :name="field.name"
                        type="text"
                        class="form-control"
                        :placeholder="field.fieldLabel"
                        v-model="formData[field.name]"
                      />
                      <span class="text-danger">
                        {{ getErrMsg(errors, field.name) }}
                      </span>
                    </div>
                    <div
                      class="form-group"
                      v-if="field.fieldTypeId === 'email'"
                    >
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <input
                        v-validate="'required|email'"
                        :name="field.name"
                        type="text"
                        class="form-control"
                        :placeholder="field.fieldLabel"
                        v-model="formData.email"
                      />
                      <span class="text-danger">
                        {{ getErrMsg(errors, field.name) }}
                      </span>
                    </div>
                    <div
                      class="form-group"
                      v-if="field.fieldTypeId === 'telefono'"
                    >
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <input
                        v-validate="'required|numeric|min:10'"
                        :name="field.name"
                        maxlength="10"
                        type="text"
                        class="form-control"
                        :placeholder="field.fieldLabel"
                        v-model="formData.phone"
                      />
                      <span class="text-danger">
                        {{ errors.first(field.name) }}
                      </span>
                    </div>

                    <div
                      class="form-group"
                      v-if="field.fieldTypeId === 'textarea'"
                    >
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <textarea
                        :name="field.name"
                        class="form-control"
                        :placeholder="field.fieldLabel"
                        maxlength="450"
                        v-model="formData.comment"
                      ></textarea>
                    </div>

                    <div
                      class="form-group"
                      v-if="field.fieldTypeId === 'select'"
                    >
                      <label class="mb-0" :for="field.name">
                        {{ field.fieldLabel }}
                      </label>
                      <select
                        v-validate="'required'"
                        :name="field.name"
                        class="custom-select"
                        :placeholder="field.fieldLabel"
                        v-model="formData[field.name]"
                      >
                        <option
                          v-for="(item, index) in field.ddlCatalogs"
                          :key="index"
                          :value="item.id"
                          >{{ item.name }}</option
                        >
                      </select>
                      <span class="text-danger">
                        {{ errors.first(field.name) }}
                      </span>
                    </div>

                    <div
                      class="form-group text-center"
                      v-if="field.fieldTypeId === 'submit'"
                    >
                      <button
                        class="btn btn-outline-info mt-3 w-75"
                        :name="field.name"
                        v-on:click.prevent="onSubmit()"
                      >
                        {{ field.fieldLabel }}
                      </button>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
        <hr />
      </div>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  props: ['fields'],
  data() {
    return {
      formData: {}
    }
  },
  methods: {
    onSubmit: function() {
      this.$validator.validateAll().then(isValid => {
        if (isValid) {
          var infoRequestData = JSON.stringify(this.formData)
          let infoRequest = {
            infoRequestData: infoRequestData,
            landingPageId: this.pageid,
            email: this.formData.email
          }
          axios
            .post(this.baseUrl + 'api-inforequest', infoRequest)
            .then(() => {
              this.formData = {}
            })
            .catch(function(error) {
              console.log('Error: ' + error)
            })
        } else {
          console.log(isValid)
        }
      })
    },
    getErrMsg(errors, field) {
      var message = ''
      var error = errors.items.find(x => x.field == field)
      if (error) {
        switch (error.rule) {
          case 'required':
            message = `El campo ${field} es obligatorio.`
            break
          case 'email':
            message = `El campo ${field} debe ser un correo electrónico válido.`
            break
          default:
            break
        }
      } else {
        message = ''
      }

      return message
    }
  }
}
</script>
