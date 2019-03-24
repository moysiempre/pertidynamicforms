 import axios from 'axios';

 const state = {
   landingPages: [],
   landingPage: {},
   action: 'read'
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
   }
 };


 export default {
   state,
   mutations,
   actions,
   getters
 }