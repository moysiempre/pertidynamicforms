<template>
  <aside>
    <ul>
      <li>
        <a>
          <i class="pe-7s-config fa-2x"></i>
        </a>
      </li>
      <li>
        <a>
          <i class="pe-7s-help1 fa-2x"></i>
        </a>
      </li>
      <li class="last">
        <a>
          <i class="pe-7s-power fa-2x"></i>
        </a>
      </li>
    </ul>
  </aside>
</template>
<script>
export default {
  data() {
    return {
      title: "ASIDE"
    };
  }
};
</script>

 
<style lang="scss" scoped>
aside {
  position: fixed;
  width: 50px;
  min-height: 80vh;
  top: 100px;
  z-index: 20;
  border-left: 8px solid #405668;
  ul {
    list-style: none;
    margin: 0;
    position: absolute;
    bottom: 10px;
    left: 10px;
    padding: 0;
    li {
      padding: 5px 5px;
      .fa-2x {
        font-size: 1.8rem;
      }
    }
  }
}
</style>

