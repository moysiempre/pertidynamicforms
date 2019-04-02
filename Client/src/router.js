import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import LandingList from './views/LandingList.vue'
import FormList from './views/FormList.vue'
import ContactList from './views/ContactList.vue'
import MailTemplateList from './views/MailTemplateList.vue'

Vue.use(Router)

let router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Login
    },
    {
      path: '/',
      name: 'login',
      component: Login
    },
    {
      path: '/resetepassword',
      name: 'resetepassword',
      component: () => import('./views/ResetePassword.vue')
    },
    {
      path: '/landings',
      name: 'landing-list',
      component: LandingList
    },
    {
      path: '/formularios',
      name: 'formularios',
      component: FormList
    },
    {
      path: '/solicitudes',
      name: 'solicitudes',
      component: ContactList
    },
    {
      path: '/mailTemplate',
      name: 'mailTemplate',
      component: MailTemplateList
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('./views/About.vue')
    }
  ]
})

export default router
