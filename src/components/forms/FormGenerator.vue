<template>
  <div>
    <form>
      <component
        v-for="(field, index) in schema"
        :key="index"
        :is="field.fieldType"
        :value="formData[field.name]"
        @input="updateForm(field.name, $event)"
        v-bind="field"
      ></component>

      <div class="row">
        <div class="form-group">
          <button type="button" class="btn btn-sm btn-outline-warning" @click="resetForm">Reset form</button>
          <button type="button" class="btn btn-sm btn-outline-info">Enviar form</button>
        </div>
      </div>
 
   <p>
      Hello {{formData.title}} {{formData.firstName}} {{formData.lastName}}, I hear you are {{formData.age}} years old.
    </p>
    </form>
  </div>
</template>

<script>
import NumberInput from "./NumberInput";
import SelectList from "./SelectList";
import TextInput from "./TextInput";

export default {
  name: "FormGenerator",
  components: { TextInput, SelectList, NumberInput },
  props: ["schema", "value"],
  data() {
    return {
      formData: this.value || {}
    };
  },
  methods: {
    updateForm(fieldName, value) {
      this.$set(this.formData, fieldName, value);
      this.$emit("input", this.formData);
    },
    resetForm() {
       this.formData = { age : 0, name:  ""}
    }
  }
};
</script>
