 import axios from 'axios';

 const state = {
   landingPages: []
 };

 const getters = {
   getLandingPages() {
     return state.landingPages;
   }
 };

 const mutations = {
   setLandingPages: (state, payload) => {
     state.landingPages = payload;
   },
   addLandingPage: (state, payload) => {
     state.landingPages.push(payload);
   },
 };

 const actions = {
   loadLandings({
     commit
   }) {
     axios.get("api-landingpage")
       .then(response => {
         commit("setLandingPages", response.data)
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