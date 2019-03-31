 import axios from 'axios';

 const state = {
   landingPages: [],
   landingPage: {},
   action: 'read',
   formOpts: {},
 };

 const getters = {

 };

 const mutations = {
   SET_LANDINGS: (state, payload) => {
     state.landingPages = payload;
   },
   SET_LANDING: (state, payload) => {
     state.landingPage = payload;
   },
   ADD_LANDING: (state, payload) => {
     state.landingPages.push(payload);
   },
   SET_ACTION: (state, payload) => {
     state.action = payload;
   },

   SET_FORMOPTS: (state, payload) => {
    state.formOpts = payload;
  },

 };

 const actions = {
   loadLandings({
     commit
   }) {
     axios.get("api-landingpage")
       .then(response => {
         commit("SET_LANDINGS", response.data)
       })
       .catch(console.error);
   },
   loadFormsForOptions({
     commit
   }) {
     axios.get("api-forms/getall").then(response => {
       commit("SET_FORMOPTS", response.data)
     }).catch(console.error);
   },
 };


 export default {
   state,
   mutations,
   actions,
   getters
 }