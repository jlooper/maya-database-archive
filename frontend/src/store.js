import Vue from 'vue';
import Vuex from 'vuex';
//import createPersistedState from 'vuex-persistedstate';

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		user: null,
	},
	//plugins: [createPersistedState()],
	mutations: {
		setUser: (state, user) => {
			state.user = user;
		},
	},
	actions: {
		setUser({ commit }, user) {
			commit('setUser', user);
		},
	},
});
