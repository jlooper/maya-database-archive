import Vue from 'vue';
import Router from 'vue-router';

import Login from '@/views/Login.vue';
import About from '@/views/About.vue';
import Home from '@/views/Home.vue';
import ErrorView from '@/views/404.vue';
import Callback from '@/views/Callback.vue';

Vue.use(Router);

const router = new Router({
	mode: 'history',
	base: process.env.BASE_URL,
	routes: [
		{
			path: '*',
			component: ErrorView,
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
			path: '/callback',
			name: 'callback',
			component: Callback,
		},
		{
			path: '/about',
			name: 'about',
			component: About,
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
	// Allow finishing callback url for logging in
	if (to.matched.some(record => record.path == '/callback')) {
		console.log('router.beforeEach found /callback url');
		//Store.dispatch('auth0HandleAuthentication');
		next(false);
	}

	// check if user is logged in (start assuming the user is not logged in = false)
	let routerAuthCheck = false;
	// Verify all the proper access variables are present for proper authorization
	if (
		localStorage.getItem('access_token') &&
		localStorage.getItem('id_token') &&
		localStorage.getItem('expires_at')
	) {
		console.log('found local storage tokens');
		// Check whether the current time is past the Access Token's expiry time
		let expiresAt = JSON.parse(localStorage.getItem('expires_at'));
		// set localAuthTokenCheck true if unexpired / false if expired
		routerAuthCheck = new Date().getTime() < expiresAt;
	}

	if (to.matched.some(record => record.meta.requiresAuth)) {
		// Check if user is Authenticated
		if (routerAuthCheck) {
			// user is Authenticated - allow access
			next();
		} else {
			// user is not authenticated - redirect to login
			router.replace('/login');
		}
	}
	// Allow page to load
	else {
		next();
	}
});
export default router;
