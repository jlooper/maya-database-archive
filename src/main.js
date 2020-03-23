import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

Vue.config.productionTip = false;
import Vuelidate from 'vuelidate';
Vue.use(Vuelidate);

import { Auth0Plugin } from './auth';
import { domain, clientId } from './auth/auth_config.json';
Vue.use(Auth0Plugin, {
	domain,
	clientId,
	onRedirectCallback: appState => {
		router.push(appState && appState.targetUrl ? appState.targetUrl : window.location.pathname);
	},
});

/*import VueMoment from 'vue-moment';
import moment from 'moment-timezone';

Vue.use(VueMoment, {
	moment,
});*/

new Vue({
	store,
	router,
	render: h => h(App),
}).$mount('#app');
