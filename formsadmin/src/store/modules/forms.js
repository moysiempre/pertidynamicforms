import axios from 'axios';

const state = {
    formHds: [],
    formHd: {},
    fAction: 'read',
    values: [],
    options: [],
};

const getters = {
    formHds() {
        return state.formHds;
    },
    formHd() {
        return state.formHd;
    },
    fAction() {
        return state.fAction;
    },
    options() {
        return state.options;
    },
    values() {
        return state.values;
    }
};

const mutations = {
    setFormHds: (state, payload) => {
        state.formHds = payload;
    },
    addFormHds: (state, payload) => {
        console.log("addFormHds", payload);
        state.formHds.push(payload);
    },
    setFormHd(state, payload) {
        state.formHd = payload;
    },
    setfAction(state, payload) {
        state.fAction = payload;
    },
    setOptions(state, payload) {
        state.options = payload;
    },
    updateValues(state, payload) {
        state.values = payload
    },
    addDetalleItem: (state, detalle) => {
        if (state.formHd && state.formHd.formDetails && state.formHd.formDetails.length) {
            state.formHd.formDetails.push(detalle);
        }
    },
};

const actions = {
    loadFormHds({
        commit
    }) {
        axios.get("api-forms")
            .then(response => {
                commit("setFormHds", response.data)
            })
            .catch(console.error);
    },
    updateValueAction({
        commit
    }, payload) {
        commit('updateValues', payload)
    },
    loadOptions({
        commit
    }) {
        axios.get("api-landingpage")
            .then(response => {
                commit("setOptions", response.data)
            })
            .catch(console.error);
    }
};


export default {
    state,
    mutations,
    actions,
    getters
}