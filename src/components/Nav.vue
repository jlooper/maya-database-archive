<template web>
	<!--<nav class="navbar is-primary" role="navigation" aria-label="main navigation">
		<div class="navbar-brand">
			<a class="navbar-item" href="./"></a>
			<span class="navbar-item is-size-3 has-text-white">Maya Hieroglyphic Database and Archive</span>

			<a
				id="example-burger"
				role="button"
				class="navbar-burger burger"
				aria-label="menu"
				aria-expanded="false"
				data-target="example-menu"
			>
				<span aria-hidden="true"></span>
				<span aria-hidden="true"></span>
				<span aria-hidden="true"></span>
			</a>
		</div>

		<div id="example-menu" class="navbar-menu">
			<div class="navbar-end">
				<div class="navbar-item">
					<router-link class="navbar-item has-text-white" :to="'about'">About</router-link>

					<div class="buttons" @click="$auth.logout()">
						<a class="button is-danger">Logout</a>
					</div>
				</div>
			</div>
		</div>
	</nav>-->
	<div class="nav-container mb-3">
		<nav class="navbar navbar-expand-md navbar-light bg-light">
			<div class="container">
				<div class="navbar-brand logo"></div>
				<button
					class="navbar-toggler"
					type="button"
					data-toggle="collapse"
					data-target="#navbarNav"
					aria-controls="navbarNav"
					aria-expanded="false"
					aria-label="Toggle navigation"
				>
					<span class="navbar-toggler-icon"></span>
				</button>

				<div class="collapse navbar-collapse" id="navbarNav">
					<ul class="navbar-nav mr-auto">
						<li class="nav-item">
							<router-link to="/" class="nav-link">Home</router-link>
						</li>
					</ul>
					<ul class="navbar-nav d-none d-md-block">
						<li v-if="!$auth.isAuthenticated && !$auth.loading" class="nav-item">
							<button id="qsLoginBtn" class="btn btn-primary btn-margin" @click.prevent="login">
								Login
							</button>
						</li>

						<li class="nav-item dropdown" v-if="$auth.isAuthenticated">
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
						</li>
					</ul>

					<!--<ul class="navbar-nav d-md-none" v-if="!$auth.isAuthenticated && !$auth.loading">
						<button id="qsLoginBtn" class="btn btn-primary btn-block" @click="login">Log in</button>
					</ul>

					<ul id="mobileAuthNavBar" class="navbar-nav d-md-none d-flex" v-if="$auth.isAuthenticated">
						<li class="nav-item">
							<span class="user-info">
								<img
									:src="$auth.user.picture"
									alt="User's profile picture"
									class="nav-user-profile d-inline-block rounded-circle mr-3"
									width="50"
								/>
								<h6 class="d-inline-block">{{ $auth.user.name }}</h6>
							</span>
						</li>
						<li>
							<font-awesome-icon icon="user" class="mr-3" />
							<router-link to="/home">Profile</router-link>
						</li>

						<li>
							<font-awesome-icon icon="power-off" class="mr-3" />
							<a id="qsLogoutBtn" href="#" class @click.prevent="logout">Log out</a>
						</li>
					</ul>-->
				</div>
			</div>
		</nav>
	</div>
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
		/*var burger = document.getElementById('example-burger');
		var menu = document.getElementById('example-menu');
		burger.addEventListener('click', function() {
			burger.classList.toggle('is-active');
			menu.classList.toggle('is-active');
		});*/
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
