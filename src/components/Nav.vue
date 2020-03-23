<template web>
	<nav class="navbar is-primary" role="navigation" aria-label="main navigation">
		<div class="navbar-brand">
			<a class="navbar-item" href="./"></a>
			<span class="navbar-item is-size-3 has-text-white">Maya Hieroglyphic Database and Archive</span>

			<a
				id="burger"
				role="button"
				class="navbar-burger burger"
				aria-label="menu"
				aria-expanded="false"
				data-target="menu"
			>
				<span aria-hidden="true"></span>
				<span aria-hidden="true"></span>
				<span aria-hidden="true"></span>
			</a>
		</div>

		<div id="menu" class="navbar-menu">
			<div class="navbar-end">
				<div class="navbar-item">
					<router-link class="navbar-item has-text-white" :to="'about'">About</router-link>

					<div class="buttons">
						<span v-if="!$auth.isAuthenticated && !$auth.loading" class="nav-item">
							<button id="qsLoginBtn" class="button is-warning" @click.prevent="login">
								Login
							</button>
						</span>
						<span class="nav-item dropdown" v-if="$auth.isAuthenticated">
							<a class="nav-link dropdown-toggle" href="#" id="profileDropDown" data-toggle="dropdown">
								<img
									:src="$auth.user.picture"
									alt="User's profile picture"
									class="nav-user-profile rounded-circle"
									width="50"
								/>
							</a>
							<div class="dropdown-menu dropdown-menu-right">
								<div class="dropdown-header">{{ $auth.user.name }}</div>
								<router-link to="/profile" class="dropdown-item dropdown-profile">
									<font-awesome-icon class="mr-3" icon="user" />Profile
								</router-link>
								<a id="qsLogoutBtn" href="#" class="dropdown-item" @click.prevent="logout">
									<font-awesome-icon class="mr-3" icon="power-off" />Log out
								</a>
							</div>
						</span>
					</div>
				</div>
			</div>
		</div>
	</nav>
</template>

<script web>
import { mapState } from 'vuex';

export default {
	name: 'my-navbar',
	computed: {
		...mapState(['user']),
	},

	methods: {
		login() {
			this.$auth.loginWithRedirect();
		},
		logout() {
			this.$auth.logout();
			this.$router.push({ path: '/' });
		},
	},
	mounted: function() {
		var burger = document.getElementById('burger');
		var menu = document.getElementById('menu');
		burger.addEventListener('click', function() {
			burger.classList.toggle('is-active');
			menu.classList.toggle('is-active');
		});
	},
};
</script>

<style>
.main-content {
	margin-top: 30px;
}
.circle {
	width: 100px;
	border-radius: 50px;
	background-color: white;
}
.wrapper {
	flex: 1;
}
</style>
