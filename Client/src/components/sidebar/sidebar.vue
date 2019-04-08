<template>
  <aside>
    <ul>
      <li>
        <router-link to="/usermanual">
          <i class="pe-7s-help1 fa-2x"></i>
        </router-link>
        <span class="desc">MANUAL USUARIO</span>
      </li>
      <li class="last">
        <a class="c-pointer" @click="logout">
          <i class="pe-7s-power fa-2x"></i>
        </a>
        <span class="desc">SALIR</span>
      </li>
    </ul>
  </aside>
</template>
<script>
export default {
  methods: {
    logout: function() {
      this.$store
        .dispatch('logout')
        .then(() => {
          console.log('to login')
          this.$router.push({ name: 'login' })
        })
        .catch(err => console.log(err))
    }
  }
}
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
      position: relative;
      .fa-2x {
        font-size: 1.8rem;
      }
      .desc {
        position: absolute;
        border: 1px solid #405668;
        background: #f5f5f5;
        padding: 5px 10px;
        font-size: 0.75rem;
        display: none;
        top: 3px;
        left: 48px;
        width: auto;
        white-space: nowrap;
      }
      &:hover {
        .desc {
          display: block;
        }
      }
      a {
        color: #405668;
      }
    }
  }
}
</style>
