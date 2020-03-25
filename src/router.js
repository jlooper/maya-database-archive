import Vue from 'vue';
import Router from 'vue-router';

import Profile from '@/views/Profile.vue';
import About from '@/views/About.vue';
import Home from '@/views/Home.vue';
import Search from '@/views/Search.vue';
import Error from '@/views/404.vue';

import { authGuard } from '@/auth';

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
			beforeEnter: authGuard,
		},
		{
			path: '/error',
			name: 'error',
			component: 404,
		},
	],
});

export default router;
