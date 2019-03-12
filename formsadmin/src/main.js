import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Vuelidate from 'vuelidate'
import store from './store'
import Axios from 'axios'

Vue.config.productionTip = false
Vue.use(Vuelidate)
//Vue.prototype.$http = Axios;
Axios.defaults.baseURL = 'http://localhost:60829/landing/';

const token = localStorage.getItem('access_token')
if (token) {
  Axios.defaults.headers.common['Authorization'] = 'bearer ' + token
}
// const token = localStorage.getItem('token')
// if (token) {
//   Vue.prototype.$http.defaults.headers.common['Authorization'] = token
// }
// Vue.http.options.credentials = true;
// Vue.http.options.root = 'http://localhost:60829/landing/';
// Vue.http.interceptors.push((request, next) => {
//   request.headers.set('Authorization', 'bearer ' + localStorage.getItem('bearerToken'))
//   next(function(response) {
//     if (response.status === 401) {
//       this.$router.push({ name: "login" });   
//     }
//   });
// })

// axios({ method: "POST", url: "api-landingpage", data: formData })
// .then(function(response) {})
// .catch(err => {});


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
  store,
  render: h => h(App)
}).$mount('#app')