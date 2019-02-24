<template>
  <div class="container">YES
    <form @submit.prevent="onSubmit">
      <div class="input" :class="{invalid: $v.formData.email.$error}">
        <label for="email">Mail</label>
        <input type="email" id="email" @blur="$v.formData.email.$touch()" v-model="formData.email">
        <p v-if="!$v.formData.email.email">Please provide a valid email address.</p>
        <p v-if="!$v.formData.email.required">This field must not be empty.</p>
      </div>
      <div class="submit">
        <button class="btn btn-dark" type="submit" :disabled="$v.formData.$invalid">Submit</button>
      </div>
    </form>
  </div>
</template>
<script>
import { required, email } from "vuelidate/lib/validators";
export default {
  name: "HelloWorld",
  data() {
    return {
      formData: {
        email: ""
      }
    };
  },
  validations: {
    formData: {
      email: {
        required,
        email
      }
    }
  },
  methods: {
    navigateToHome() {
      this.$router.push({ name: "formsFactory" });
    },
    onSubmit() {
      const formData = {
        email: this.email,
        password: this.password
      };
      console.log(formData);
      this.$store.dispatch("login", {
        email: formData.email,
        password: formData.password
      });
    }
  }
};
</script>
<style lang="css">
.input.invalid input {
  border: 1px solid red;
  background-color: #ffc9aa;
}
</style>
