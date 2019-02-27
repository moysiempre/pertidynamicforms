<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-md-3">
        <div class="card border-light helix">
          <div class="card-header">
            <h5 class="mt-2">LANDING PAGES</h5>
          </div>
          <div class="card-body p-0">
            <ul class="list-group">
              <li class="list-group-item">
                <h6 class="mb-0">MOBILE XT</h6>
                <p class="mb-0" style="color:#75818b">Obtener asesoría especializada</p>
                <p class="mb-0 small text-muted">
                  <small style="color:#a3abb2">very good always</small>
                </p>
              </li>
              <li class="list-group-item">
                <h6 class="mb-0">MOBILE XT</h6>
                <p class="mb-0" style="color:#75818b">Obtener asesoría especializada</p>
                <p class="mb-0 small text-muted">
                  <small style="color:#a3abb2">very good always</small>
                </p>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-md-5">
        <div class="card border-light">
          <div class="card-body">
            <h5 class="mt-2">NUEVO</h5>
            <form action class="mt-3">
              <div class="form-group">
                <label class="mb-0" for>Nombre del landing page</label>
                <input type="text" name id class="form-control">
              </div>
              <div class="form-group">
                <label class="mb-0" for>Texto del encabezado a mostrar del landing page</label>
                <input type="text" name id class="form-control">
              </div>
              <div class="form-group">
                <label class="mb-0" for>Descripción del landing page</label>
                <textarea name id class="form-control"></textarea>
              </div>
              <div class="form-group text-right">
                <button type="button" class="btn btn-default">CANCELAR</button>
                <button type="button" class="btn btn-dark">GUARDAR</button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="col-md-3"></div>
    </div>
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
 
