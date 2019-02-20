<template>
  <div class="container">
    <h1>HELLO WORLD</h1>
    <p>
        {{msg}}
    </p>
  </div>
</template>
<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome to Your Vue.js App"
    };
  }
};
</script>
<style lang="css">
</style>
