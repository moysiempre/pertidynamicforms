<template>
  <div class="form-group">
    <!-- <label>{{label}}</label>  -->
    <input type="text" class="form-control" :name="name" :placeholder="placeholder">
  </div>
</template>
<script>
export default {
  name: "textInput",
  props: ["placeholder", "name"]
};
</script>