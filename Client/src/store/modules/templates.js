import axios from 'axios'

const state = {
  mailtemplates: [],
  mailtemplate: {},
  action: 'read'
}

const getters = {}

const mutations = {
  SET_TEMPLATES: (state, payload) => {
    state.mailtemplates = payload
  },
  SET_TEMPLATE: (state, payload) => {
    state.mailtemplate = payload
  },
  ADD_TEMPLATE: (state, payload) => {
    state.mailtemplates.push(payload)
  },
  SET_ACTION: (state, payload) => {
    state.action = payload
  }
}

const actions = {
  loadTemplates({ commit }) {
    axios
      .get('api-mailtemplate')
      .then(response => {
        commit('SET_TEMPLATES', response.data)
      })
      .catch(console.error)
  }
}

export default {
  state,
  mutations,
  actions,
  getters
}
