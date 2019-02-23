<template>
  <div class="container-fluid">
    {{msg}}
    <div class="row">
      <div class="col-md-3">4</div>
      <div class="col-md-6">
        <div class="card">
          <div class="card-body bg-fa">
            <h3></h3>
          </div>
        </div>
      </div>
      <div class="col-md-3">3</div>
    </div>

    <hr>
    <form-generator :schema="schema" v-model="formData"></form-generator>
    <hr>
  </div>
</template>
<script>
import FormGenerator from "../components/forms/FormGenerator";
export default {
  name: "HelloWorld",
  components: { FormGenerator },
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      formData: {
        firstName: "Evan",
        lastName: "Vzskez",
        title: "seleccione",
        age: 34
      },
      schema: [
        {
          fieldType: "SelectList",
          name: "title",
          multi: false,
          label: "Title",
          options: [
            { key: "", value: "seleccione" },
            { key: "1", value: "Text Input" }
          ]
        },
        {
          fieldType: "TextInput",
          placeholder: "First Name",
          label: "First Name",
          name: "firstName"
        },
        {
          fieldType: "TextInput",
          placeholder: "Last Name",
          label: "Last Name",
          name: "lastName"
        },
        {
          fieldType: "NumberInput",
          placeholder: "Age",
          name: "age",
          label: "Age",
          minValue: 0
        }
      ]
    };
  }
};
</script>
<style lang="css">
  .bg-fa{
    background-color: #fafafa;
  }
</style>
