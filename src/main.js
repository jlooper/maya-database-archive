import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

Vue.config.productionTip = false;
import Vuelidate from 'vuelidate';
Vue.use(Vuelidate);

import VueMoment from 'vue-moment';
import moment from 'moment-timezone';

Vue.use(VueMoment, {
	moment,
});

let app = '';

if (!app) {
	app = new Vue({
		store,
		router,
		render: h => h(App),
	}).$mount('#app');
}
