import Vue from 'vue';
import Router from 'vue-router';

import Profile from '@/views/Profile.vue';
import About from '@/views/About.vue';
import Home from '@/views/Home.vue';
import Search from '@/views/Search.vue';
import Lost from '@/views/Lost.vue';
import Login from '@/views/Login.vue';
import Register from '@/views/Register.vue';

Vue.use(Router);

const router = new Router({
	mode: 'history',
	base: process.env.BASE_URL,
	routes: [
		{
			path: '/',
			name: 'home',
			component: Home,
		},
		{
			path: '/login',
			name: 'login',
			component: Login,
		},
		{
			path: '/register',
			name: 'register',
			component: Register,
		},

		{
			path: '/search',
			name: 'search',
			component: Search,
		},

		{
			path: '/about',
			name: 'about',
			component: About,
		},
		{
			path: '/profile',
			name: 'profile',
			component: Profile,
			//beforeEnter: authGuard,
		},
		{
			path: '/error',
			name: 'error',
			component: Lost,
		},
	],
});

export default router;
