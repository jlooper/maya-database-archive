import Vue from 'vue';
import Router from 'vue-router';

import Login from '@/views/Login.vue';
import About from '@/views/About.vue';
import Home from '@/views/Home.vue';

Vue.use(Router);

const router = new Router({
	mode: 'history',
	base: process.env.BASE_URL,
	routes: [
		{
			path: '*',
			redirect: '/login',
		},
		{
			path: '/',
			redirect: '/login',
		},
		{
			path: '/login',
			name: 'login',
			component: Login,
		},
		{
			path: '/about',
			name: 'about',
			component: About,
			meta: {
				requiresAuth: true,
			},
		},
		{
			path: '/home',
			name: 'home',
			component: Home,
			meta: {
				requiresAuth: true,
			},
		},
	],
});

router.beforeEach((to, from, next) => {
	/*const currentUser = firebase.auth().currentUser;
	const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
	if (requiresAuth && !currentUser) {
		next('login');
	} else if (!requiresAuth && currentUser) {
		next('home');
	} else {
		next();
	}*/
	next();
});

export default router;
