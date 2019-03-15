import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import Landing from './views/Landing.vue'
import FormFactory from './views/FormFactory.vue'
import Contact from './views/Contact.vue'
import ContactDetail from './views/ContactDetail.vue'
import Demo from './views/Demo.vue'
import store from './store.js'

Vue.use(Router)

let router = new Router({
  routes: [
    {
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
    },{
      path: '/demo',
      name: 'demo',
      component: Demo
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

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
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