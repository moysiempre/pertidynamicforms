<template>
  <div class="form-group">
    <!-- <label>{{label}}</label>  -->
    <input type="number" class="form-control" min="0" :name="name" :placeholder="placeholder">
  </div>
</template>
<script>
export default {
  name: "numberInput",
  props: ["placeholder", "name"]
};
</script>