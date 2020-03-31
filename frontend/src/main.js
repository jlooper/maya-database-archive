import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import axios from 'axios';
import { Model } from 'vue-api-query';
Model.$http = axios;

Vue.config.productionTip = false;
import Vuelidate from 'vuelidate';
Vue.use(Vuelidate);

new Vue({
	store,
	router,
	render: h => h(App),
}).$mount('#app');
