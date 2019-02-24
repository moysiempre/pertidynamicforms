<template>
  <div>
    <form>
      <component
        v-for="(field, index) in schema"
        :key="index"
        :is="field.fieldType"
        :placeholder="field.fieldLabel"
      ></component>
      
      <div class="row">
        <div class="form-group">
          <button type="button" class="btn btn-sm btn-outline-warning">Reset form</button>
          <button type="button" class="btn btn-sm btn-outline-info">Enviar form</button>
        </div>
      </div>
   
    </form>
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
  methods: {
    
  }
};
</script>
