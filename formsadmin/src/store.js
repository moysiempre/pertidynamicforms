import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    landingPages: [],
    landingPage: {},
    lpAction: "read",
    auth: null,
    loading: false,
  },
  mutations: {
    SET_LANDING_PAGES: (state, landingPages) => {
      state.landingPages = landingPages;
    },
    logout(state) {
      state.auth = null;
    },
    loginRequest(state) {
      state.loading = true;
    },
    loginSuccess(state, payload) {
      state.auth = payload;
      state.loading = false;
    },
    loginError(state) {
      state.loading = false;
    }
  },
  actions: {
    login({
      commit
    }, payload) {
      return new Promise((resolve, reject) => {
        commit("loginRequest");
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
            commit("loginSuccess", auth);
            resolve(response);
          })
          .catch(error => {
            commit("loginError");
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
        commit('logout')
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
  }
})