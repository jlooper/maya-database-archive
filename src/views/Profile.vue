<template web>
	<main class="column is-four-fifths main">
		<div class="box main-content">
			<h1 class="title is-size-3">Welcome</h1>
			<div>
				<img :src="$auth.user.picture" />
				<h2>{{ $auth.user.name }}</h2>
				<p>{{ $auth.user.email }}</p>
			</div>

			<div>
				<pre>{{ JSON.stringify($auth.user, null, 2) }}</pre>
			</div>
		</div>
	</main>
</template>

<script>
//import { mapState } from 'vuex';
export default {
	name: 'profile',
	computed: {
		//...mapState(['user']),
	},

	methods: {
		//...mapActions(["fetchUser", "fetchStudents", "fetchPractices"]),
	},

	created() {
		/*this.fetchUser(this.currentUser.uid)*/
	},
};
</script>
<style scoped>
.box {
	margin: 5px;
}
.card {
	background-color: transparent;
	box-shadow: none;
}
.card-content {
	padding: 1px;
}
.card-footer-item:not(:last-child) {
	border: none;
}
.card-footer {
	padding-top: 5px;
	margin: 5px;
}
h3 {
	padding-bottom: 20px;
}
</style>
