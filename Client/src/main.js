import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/store'
import Axios from 'axios'
import VeeValidate from 'vee-validate'
import VueSwal from 'vue-swal'
import CKEditor from '@ckeditor/ckeditor5-vue'

Vue.config.productionTip = false
Vue.use(VueSwal)
Vue.use(VeeValidate)
Vue.use(CKEditor)

//Vue.prototype.$http = Axios;
Axios.defaults.baseURL = 'http://192.168.15.12:88/landing/'
Axios.defaults.baseURL = 'http://localhost:60829/landing/'

const token = localStorage.getItem('access_token')
if (token) {
  Axios.defaults.headers.common['Authorization'] = 'bearer ' + token
}

Axios.interceptors.response.use(
  function(response) {
    return response
  },
  function(error) {
    if (401 === error.response.status) {
      store.commit('logout')
      router.push({
        path: '/'
      })
      delete Axios.defaults.headers.common['Authorization']
    }

    return Promise.reject(error)
  }
)

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
