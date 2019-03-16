<template>
  <span class="spinners white" v-if="isloading"></span>
</template>
<script>
export default {
  name: "btnLoader",
  props: ["isloading"]
};
</script>

<style>
@keyframes spinners {
  to {
    transform: rotate(360deg);
  }
}
.spinners {
  position: relative;
  width: 20px;
  display: inline-block;
  margin-left: 10px;
  margin-top: 5px;
}
.spinners:before {
  content: "";
  box-sizing: border-box;
  position: absolute;
  top: -15px;
  left: 0;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  border-top: 2px solid #07d;
  border-right: 2px solid transparent;
  animation: spinners 0.6s linear infinite;
}

.spinners.v-page:before {
  right: 15px !important;
}

.spinners.white:before {
  border-top-color: #fff !important;
}
</style>
