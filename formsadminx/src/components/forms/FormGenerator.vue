<template>
  <div class="card bg-elight h-100">
    <div class="card-header bg-transparent" style="border-bottom-color: transparent">
      <h6 class="text-right mt-0 mb-0 mx-3">VISTA PREVIA</h6>
    </div>
    <div class="card mx-2">
      <div class="card-header bg-white">
        <h6 class="mt-2">Obtener asesoría especializada</h6>
      </div>
      <div class="card-body">
        <form v-if="formHd.formDetails">
          <component
            v-for="(field, index) in formHd.formDetails"
            :key="index"
            :is="field.fieldTypeId"
            :placeholder="field.fieldLabel"
          ></component>

           
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import NumberInput from "./NumberInput";
import TextInput from "./TextInput";
import SubmitInput from "./SubmitInput";
import SelectList from "./SelectList";
import { mapState } from "vuex";

export default {
  name: "FormGenerator",
  components: { TextInput, NumberInput, SelectList, SubmitInput},
  data() {
    return {
      formData: this.value || {}
    };
  },
  computed: {
    ...mapState(["formHd"])
  },
  methods: {}
};
</script>
