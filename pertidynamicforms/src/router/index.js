import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/pages/Login'
import Register from '@/pages/Register'
import LandingNew from '@/pages/LandingNew'
import FormsFactory from '@/pages/FormsFactory'
import ContactsVisor from '@/pages/ContactsVisor'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'login',
      component: Login
    },{
      path: '/register',
      name: 'register',
      component: Register
    },{
      path: '/landingNew',
      name: 'landingNew',
      component: LandingNew
    },{
      path: '/formsFactory',
      name: 'formsFactory',
      component: FormsFactory
    },{
      path: '/contactsVisor',
      name: 'contactsVisor',
      component: ContactsVisor
    }
  ]
})
