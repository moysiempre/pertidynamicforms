import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource';

Vue.config.productionTip = false
Vue.use(Vuelidate)
Vue.use(VueResource);

Vue.http.options.credentials = true;
//Vue.http.options.root = 'http://localhost:64423/landing/';
Vue.http.interceptors.push((request, next) => {
  request.headers.set('Authorization', 'bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJqYW5AeWFob28uZXMiLCJqdGkiOiJmODZmYTNkZi03ZTNhLTRkZTctOGRmMS01YzYyYThiMzgxMDQiLCJ1c2VySWQiOiI4MjkxMDM2NC1jNWIzLTQ3NTItYWRhMC1hMmFhYWZiYjY4ZmMiLCJuYmYiOjE1NTE5Njk4NjEsImV4cCI6MTU1MjAxMzA2MSwiaXNzIjoiaHR0cDovL3Btc3ZrYXBpLmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoiZm9ybXNKd3QifQ.j-e8ojNaNcXU3aXCLY_Jf6cnt5umV0YkSjJTHKPcFQI')
  next()
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
