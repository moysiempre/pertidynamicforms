import axios from 'axios';

const state = {
    formHds: [],
    formHd: {},
    action: 'read',
    values: [],
    options: [],
    baseDetails: [],
    isOptSelected: false
};

const getters = {};

const mutations = {
    SET_FORM_HDS: (state, payload) => {
        state.formHds = payload;
    },
    SET_FORM_HD(state, payload) {
        state.formHd = payload;
    },
    ADD_FORM_HD: (state, payload) => {
        state.formHds.push(payload);
    },
    SET_ACTION(state, payload) {
        state.action = payload;
    },
    SET_BASE_DETAILS(state, payload) {
        state.baseDetails = payload;
    },
    SET_OPT_SELECTED(state, payload) {
        state.isOptSelected = payload;
    },

    SET_OPTIONS(state, payload) {
        state.options = payload;
    },
    SET_VALUES(state, payload) {
        state.values = payload
    },

    ADD_DETAIL_ITEM: (state, detalle) => {
        if (state.formHd && state.formHd.formDetails && state.formHd.formDetails.length) {
            state.formHd.formDetails.push(detalle);
        }
    },
};

const actions = {
    loadFormHds({
        commit
    }) {
        axios.get("api-forms").then(response => {
            commit("SET_FORM_HDS", response.data)
        }).catch(console.error);
    },

    loadBaseDetails({
        commit
    }) {
        axios.get("api-forms/basedetail").then(response => {
            commit("SET_BASE_DETAILS", response.data)
        }).catch(console.error);
    },

    updateValueAction({
        commit
    }, payload) {
        commit('SET_VALUES', payload)
    },
    loadOptions({
        commit
    }) {
        axios.get("api-landingpage").then(response => {
            commit("SET_OPTIONS", response.data)
        }).catch(console.error);
    }
};


export default {
    state,
    mutations,
    actions,
    getters
}