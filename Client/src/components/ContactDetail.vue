<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <div class="form-group">
          <small class="text-muted">Landing Page</small>
          <p>{{contact.landingPageName}}</p>
        </div>
        <div class="form-group">
          <small class="text-muted">Fecha</small>
          <p>{{contact.requestDateStr}}</p>
        </div>
        <div v-for="(item, index) in contact.infoRequestData" :key="index">
          <div class="form-group" v-if="item.fieldTypeId!=='submit'">
            <small class="text-muted">{{item.fieldLabel}}</small>
            <p>{{item.data}}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["contact"]
};
</script>
<style lang="css">
</style>
 