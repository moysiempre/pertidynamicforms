import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import landingsModule from "./modules/landings"
import formHdsModule from "./modules/forms"

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    showFullLayout: true,
    auth: null,
  },
  mutations: {

    SET_FULL_LAYOUT(state, payload) {
      state.showFullLayout = payload;
    },

    LOGOUT(state) {
      state.auth = null;
    },

    LOGIN_SUCCESS(state, payload) {
      state.auth = payload;
    },

  },
  actions: {
    login({
      commit
    }, payload) {
      return new Promise((resolve, reject) => {
        axios
          .post("api-security/login", payload)
          .then(response => {
            const auth = response.data;
            auth.user = {
              username: ""
            }
            axios.defaults.headers.common["Authorization"] = `Bearer ${
              auth.access_token
            }`;
            localStorage.setItem('access_token', auth.access_token);
            localStorage.setItem('refresh_token', auth.refresh_token)
            commit("LOGIN_SUCCESS", auth);
            resolve(response);
          })
          .catch(error => {
            delete axios.defaults.headers.common["Authorization"];
            localStorage.removeItem('access_token')
            localStorage.removeItem('refresh_token')
            reject(error.response);
          });
      });
    },
    logout({
      commit
    }) {
      return new Promise((resolve) => {
        commit('LOGOUT')
        localStorage.removeItem('access_token')
        localStorage.removeItem('refresh_token')
        delete axios.defaults.headers.common['Authorization']
        resolve()
      })
    },

  },
  getters: {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
    showFullLayout(state) {
      state.showFullLayout;
    }

  },
  modules: {
    landings: landingsModule,
    forms: formHdsModule
  },
})