<template>
  <div class="container-fluid">
    <h5>DETAILLE DE SOLICITUD

      <router-link to="/solicitudes">back to</router-link>
    </h5>
    
    <div class="row">
      <div class="col">
        <div class="form-group">
          <small class="text-muted">Nombre completo</small>
          <p>JULIO MATIAS HERNANDEZ</p>
        </div>
        <div class="form-group">
          <small class="text-muted">Email</small>
          <p>julio.hernandez@yahoo.es</p>
        </div>
         <div class="form-group">
          <small class="text-muted">Teléfono</small>
          <p>55 6543 6543</p>
        </div>
        <div class="form-group">
          <small class="text-muted">Nombre de la empresa</small>
          <p>JULIO MATIAS HERNANDEZ sa de cv</p>
        </div>

        <div class="form-group">
          <small class="text-muted">Puesto en la empresa</small>
          <p>Director/ CEO / Fundador</p>
        </div>
        <div class="form-group">
          <small class="text-muted">Tipo de empresa</small>
          <p>Agencias de marketing y comunicación</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      contactItem: []
    };
  }
};
</script>
<style lang="css">
</style>
