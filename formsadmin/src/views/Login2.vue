<template>
 <div>
   <form class="login" @submit.prevent="login">
     <h1>Sign in</h1>
     <label>Email</label>
     <input required v-model="userName" type="email" placeholder="Name" />
     <label>Password</label>
     <input required v-model="password" type="password" placeholder="Password"/>
     <hr/>
     <button type="submit">Login</button>
   </form>
 </div>
</template>
<script>
  export default {
    data(){
      return {
        userName : "jan@yahoo.es",
        password : "123456"
      }
    },
    methods: {
      login: function () {
        let userName = this.userName 
        let password = this.password
        this.$store.dispatch('login', { userName, password })
       .then(() => this.$router.push('/landing'))
       .catch(err => console.log(err))
      }
    }
  }
</script>