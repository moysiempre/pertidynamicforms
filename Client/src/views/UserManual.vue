<template>
  <div>
    <div class="col-10 offset-1">
      <div class="card">
        <div class="card-header d-flex justify-content-between">
          <h5 class="mb-0">MANUAL DE USUARIO</h5>
          <span>
            <span>Página</span>
            <input
              type="number"
              min="1"
              :max="pageCount"
              style="width:60px; text-align:center"
              v-model.number="page"
            />
            <span>de [{{ pageCount }}]</span>
          </span>
        </div>
        <div class="card-body">
          <pdf
            v-show="this.pageCount"
            src="/usermanual/PruebaDescargaManual.pdf"
            @num-pages="pageCount = $event"
            :page="page"
            style="width:100%;"
            @error="onError"
          ></pdf>
          <div v-if="!pageCount || pageCount <= 0" class="alert alert-warning">
            Manual de usuario no encontrado, favor refrescar la página,
            <br />si persiste favor comunicar con el administrador
          </div>
        </div>
      </div>
    </div>

    <div class="form-groups text-center"></div>
  </div>
</template>

<script>
import pdf from 'vue-pdf'
export default {
  components: {
    pdf
  },
  data() {
    return {
      currentPage: 0,
      pageCount: 0,
      page: 1,

      vprogress: -1
    }
  },
  methods: {
    onError(evt) {
      console.log(evt)
    }
  }
}
</script>

<style lang="scss" scoped>
.pdf-box {
  width: 90%;
  text-align: center;
  border: 1px solid #ddd;
  margin: 0 auto;
  padding-top: 6px;
  background: #efefef;
}
</style>
