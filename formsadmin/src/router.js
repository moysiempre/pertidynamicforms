import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Login from './views/Login.vue'
import Landing from './views/Landing.vue'
import FormFactory from './views/FormFactory.vue'
import Contact from './views/Contact.vue'
import ContactDetail from './views/ContactDetail.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/changepassword',
      name: 'changepassword',
      component: () => import('./views/ChangePassword.vue')
    },
    {
      path: '/landing',
      name: 'landing',
      component: Landing
    },
    {
      path: '/formularios',
      name: 'formularios',
      component: FormFactory
    },
    {
      path: '/solicitudes',
      name: 'solicitudes',
      component: Contact
    },
    {
      path: '/solicitudes/:id',
      name: 'solicitudesdet',
      component: ContactDetail
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    }
  ]
})
