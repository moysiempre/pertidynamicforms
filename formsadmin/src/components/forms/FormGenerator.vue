<template>
  <div class="card bg-elight h-100">
    <h6 class="text-right mt-0 mb-3 mx-3">VISTA PREVIA</h6>
    <div class="card mx-2">
      <div class="card-header bg-white">
        <h6 class="mt-2">Obtener asesoría especializada</h6>
      </div>
      <div class="card-body">
        <form>
          <component
            v-for="(field, index) in schema"
            :key="index"
            :is="field.fieldType"
            :placeholder="field.fieldLabel"
          ></component>

          
            <div class="form-group text-center">
              <button type="button" class="btn btn-sm btn-outline-info">Enviar form</button>
            </div>
          
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import NumberInput from "./NumberInput";
import TextInput from "./TextInput";

export default {
  name: "FormGenerator",
  components: { TextInput, NumberInput },
  props: ["schema"],
  data() {
    return {
      formData: this.value || {}
    };
  },
  methods: {}
};
</script>
