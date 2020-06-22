import Vue from 'vue';
import Router from 'vue-router';
import PageNotFound from '@/components/Lost.vue';

Vue.use(Router);

export default new Router({
	mode: 'history',
	base: process.env.BASE_URL,
	routes: [
		{
			path: '/',
			redirect: '/home',
		},
		{
			path: '/profile',
			name: 'profile',
			component: () => import(/* webpackChunkName: "products" */ './views/Profile.vue'),
		},
		{
			path: '/about',
			name: 'about',
			component: () => import(/* webpackChunkName: "products" */ './views/About.vue'),
		},
		{
			path: '/search',
			name: 'search',
			component: () => import(/* webpackChunkName: "discount" */ './views/Search.vue'),
		},
		{
			path: '/home',
			name: 'home',
			// route level code-splitting
			// this generates a separate chunk (home.[hash].js) for this route
			// which is lazy-loaded when the route is visited.
			component: () => import(/* webpackChunkName: "home" */ './views/Home.vue'),
		},
		{
			path: '*',
			component: PageNotFound,
		},
	],
});
