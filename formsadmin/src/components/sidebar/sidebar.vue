<template>
  <aside>
    <ul>
      <li>
        <a class="c-pointer">
          <i class="pe-7s-config fa-2x"></i>
        </a>
      </li>
      <li>
        <a class="c-pointer">
          <i class="pe-7s-help1 fa-2x"></i>
        </a>
      </li>
      <li class="last">
        <a class="c-pointer" @click="logout">
          <i class="pe-7s-power fa-2x"></i>
        </a>
      </li>
    </ul>
  </aside>
</template>
<script>
export default {
  methods: {
    logout: function() {
      this.$store
        .dispatch("logout")
        .then(() => {
          console.log("to login");
          this.$router.push({ name: "login" });
        })
        .catch(err => console.log(err));
    }
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

