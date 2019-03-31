<template>
  <div class="form-group text-center">
    <input type="submit"  class="btn btn-outline-primary" :value="placeholder">
  </div>
</template>
<script>
export default {
  name: "submitInput",
  props: ["placeholder","name"]
};
</script>