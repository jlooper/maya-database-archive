import Vue from 'vue';
import Router from 'vue-router';

import Profile from '@/views/Profile.vue';
import About from '@/views/About.vue';
import Home from '@/views/Home.vue';
import Search from '@/views/Search.vue';
import Lost from '@/components/Lost.vue';
import store from './store.js';

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
			meta: {
				requiresAuth: true,
			},
		},
		{
			path: '/error',
			name: 'error',
			component: Lost,
		},
	],
});
router.beforeEach((to, from, next) => {
	if (to.matched.some((record) => record.meta.requiresAuth)) {
		if (store.getters.isLoggedIn) {
			next();
			return;
		}
		next('/');
	} else {
		next();
	}
});
export default router;
