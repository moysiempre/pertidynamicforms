import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    status: '',
    token: localStorage.getItem('token') || '',
    user : {},
    landingPages: [],
    landingPage: {},
    lpAction: "read"
  },
  mutations: {
    SET_LANDING_PAGES: (state, landingPages) =>{
      state.landingPages = landingPages;
    },
    auth_request(state){
        state.status = 'loading'
      },
      auth_success(state, token, user){
        state.status = 'success'
        state.token = token
        state.user = user
      },
      auth_error(state){
        state.status = 'error'
      },
      logout(state){
        state.status = ''
        state.token = ''
      },
  },
  actions: {
    login({commit}, user){
        return new Promise((resolve, reject) => {
          commit('auth_request')
          axios({url: 'api-security/login', data: user, method: 'POST' })
          .then(resp => {           
            const token = resp.data.access_token
            const user = {userName: 'jan@yahoo.es'}
            localStorage.setItem('token', token)
            console.log("token: ", token);
            axios.defaults.headers.common['Authorization'] = 'bearer ' + token
            commit('auth_success', token, user)
            resolve(resp)
          })
          .catch(err => {
            commit('auth_error')
            localStorage.removeItem('token')
            reject(err)
          })
        })
    },
    logout({commit}){
        return new Promise((resolve, reject) => {
          commit('logout')
          localStorage.removeItem('token')
          delete axios.defaults.headers.common['Authorization']
          resolve()
        })
      }, 
    getLandingPages({commit}){
        axios({url: 'api-landingpage', method: 'GET' })
        .then(resp => { 
           commit("SET_LANDING_PAGES", resp.data);
        })
        .catch(err => {
          reject(err)
        })
      }
  },
  getters : {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
  }
})