import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import landingsModule from "./modules/landings"


Vue.use(Vuex)

export default new Vuex.Store({
  state: {

    landingPage: {},
    formHds: [],
    formHd: {},
    lpAction: "read",
    fhAction: "read",
    auth: null,
    loading: false,
    baseDetails: [],
    flatLayaout: false,

    values: [],
    options: [],

    isOptSelected: false
  },
  mutations: {
 
    GET_options: (state, options) => {
      state.options = options;
    },

    ADD_Detalle_Item: (state, detalle) => {
      if (state.formHd && state.formHd.formDetails && state.formHd.formDetails.length) {
        state.formHd.formDetails.push(detalle);
      }
    },

    updateValues(state, values) {
      state.values = values
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
    updateValueAction({
      commit
    }, value) {
      commit('updateValues', value)
    },    
  },
  getters: {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,

  },
  modules: {
    landings: landingsModule,
  },
})