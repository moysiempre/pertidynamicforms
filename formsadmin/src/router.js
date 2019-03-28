import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import LandingList from './views/LandingList.vue'
import FormList from './views/FormList.vue'
import Contact from './views/Contact.vue'
import MailTemplate from './views/MailTemplate.vue'

import store from './store/store'

Vue.use(Router)

let router = new Router({
  routes: [{
      path: '/',
      name: 'home',
      component: Login
    },
    {
      path: '/login',
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
      component: Contact
    },
    {
      path: '/mailTemplate',
      name: 'mailTemplate',
      component: MailTemplate
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import( /* webpackChunkName: "about" */ './views/About.vue')
    }
  ]
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next()
      return
    }
    next('/login')
  } else {
    next()
  }
})
export default router