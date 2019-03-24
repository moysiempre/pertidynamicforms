<template>
  <div>
    <h6 class="mb-0">{{landing.name}}</h6>
    <p class="mb-0" style="color:#75818b">
      <small>{{landing.description}}</small>
    </p>
  </div>
</template>

<script>
export default {
  props: ["landing"]
};
</script>

<style scoped>
</style>