import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource';

Vue.config.productionTip = false
Vue.use(Vuelidate)
Vue.use(VueResource);

Vue.http.options.credentials = true;
Vue.http.options.root = 'http://localhost:64423/landing/';
Vue.http.interceptors.push((request, next) => {
  request.headers.set('Authorization', 'bearer ' + localStorage.getItem('bearerToken'))
  next(function(response) {
    if (response.status === 401) {
      this.$router.push({ name: "login" });   
    }
  });
})




// Vue.http.interceptors.push((request, next) => {
//   console.log(request);
//   if (request.method == 'POST') {
//     request.method = 'PUT';
//   }
//   next(response => {
//     response.json = () => { return {messages: response.body} }
//   });
// });

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')