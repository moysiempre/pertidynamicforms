import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/store'
import Axios from 'axios'
import Snotify from 'vue-snotify';
import VeeValidate from 'vee-validate';
import VueSwal from 'vue-swal'


Vue.config.productionTip = false
Vue.use(VueSwal)
Vue.use(VeeValidate);
Vue.use(Snotify, {
  toast: {
    timeout: 2000,
    showProgressBar: false
  }
})

//Vue.prototype.$http = Axios;
Axios.defaults.baseURL = 'http://localhost:60829/landing/';
//Axios.defaults.baseURL = 'http://localhost:83/landing/';
 
const token = localStorage.getItem('access_token')
if (token) {
  Axios.defaults.headers.common['Authorization'] = 'bearer ' + token
}

 
// Axios.interceptors.response.use(undefined, function (error) {
//   const originalRequest = error.config;
//   const refresh_token = localStorage.getItem('refresh_token')
//   if (
//     error.response.status === 401 &&
//     !originalRequest._retry &&
//     refresh_token
//   ) {
//     originalRequest._retry = true;
//     avata++;
//     console.log("HACIENDO RARO AQUI: ", avata, originalRequest._retry);
//     if (avata == 10) {
//       console.log("HACIENDO RARO AQUI --- ya superando 10: ", avata, originalRequest._retry);
//       originalRequest._retry = false;
//       store.commit("logout");
//       router.push({
//         path: "/"
//       });
//     }
//     const payload = {
//       refresh_token: refresh_token
//     };


//     // return Axios
//     //   .post("api-security/refreshToken", payload)
//     //   .then(response => {
//     //     const auth = response.data;
//     //     Axios.defaults.headers.common["Authorization"] = `Bearer ${
//     //       auth.access_token
//     //     }`;
//     //     originalRequest.headers["Authorization"] = `Bearer ${
//     //       auth.access_token
//     //     }`;
//     //     store.commit("loginSuccess", auth);
//     //     console.log("loginSuccess refreshToken", auth);
//     //     return Axios(originalRequest);
//     //   })
//     //   .catch(error => {
//     //     store.commit("logout");
//     //     router.push({
//     //       path: "/"
//     //     });
//     //     delete Axios.defaults.headers.common["Authorization"];
//     //     return Promise.reject(error);
//     //   });
//   }

//   return Promise.reject(error);
// });

// const token = localStorage.getItem('token')
// if (token) {
//   Vue.prototype.$http.defaults.headers.common['Authorization'] = token
// }
// Vue.http.options.credentials = true;
// Vue.http.options.root = 'http://localhost:60829/landing/';

Axios.interceptors.response.use(function (response) {
  return response;
}, function (error) {
  if (401 === error.response.status) {
    store.commit("logout");
    router.push({
      path: "/"
    });
    delete Axios.defaults.headers.common["Authorization"];
  }
  return Promise.reject(error);
});

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