 import axios from 'axios';

 const state = {
   landingPages: [],
   landingPage: {},
   action: "read"
 };

 const getters = {

 };

 const mutations = {
   SET_LANDINGS: (state, payload) => {
     state.landingPages = payload;
   },
   ADD_LANDING: (state, payload) => {
     state.landingPages.push(payload);
   },
   SET_LANDING: (state, payload) => {
     state.landingPage = payload;
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
   },
   createOrUpdatelanding({
     commit
   }, payload) {
     return new Promise((resolve, reject) => {
       axios
         .post("api-landingpage", payload)
         .then(response => {            
           resolve(response);
         })
         .catch(error => {
           reject(error.response);
         });
     })
   }
 };


 export default {
   state,
   mutations,
   actions,
   getters
 }