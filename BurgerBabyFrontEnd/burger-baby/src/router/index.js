import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProductInfoView from '@/views/Product/ProductInfoView.vue'
import MemberInfoView from '@/views/Member/MemberInfoView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
 
  {
    path:'/Products',
    name:'Products',
    component: () => import('../views/Product/ProductListView.vue')
  },
  {
    path:'/Product/:id',
    name:'Product',
    component:ProductInfoView
  },
  {
    path:'/Member/:id',
    name:'Member',
    component:MemberInfoView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
