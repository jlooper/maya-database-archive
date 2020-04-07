<template>
	<section class="hero">
		<div class="hero-body">
			<div class="container has-text-centered">
				<div class="column is-4 is-offset-3">
					<h3 class="title has-text-black">Login</h3>
					<hr class="login-hr" />
					<p class="subtitle has-text-black">Please login to proceed.</p>
					<div v-if="message" class="notification is-light">
						<p>{{ message }}</p>
					</div>
					<div class="box">
						<form @submit.prevent="submit">
							<div class="field">
								<div class="control">
									<input
										class="input is-large"
										type="email"
										v-model="email"
										placeholder="Your Email"
										autofocus
									/>
								</div>
							</div>

							<div class="field">
								<div class="control">
									<input
										class="input is-large"
										v-model="password"
										type="password"
										placeholder="Your Password"
									/>
								</div>
							</div>
							<div class="field">
								<label class="checkbox">
									<input type="checkbox" />
									Remember me
								</label>
							</div>
							<button type="submit" class="button is-block is-primary is-large is-fullwidth">
								Login
							</button>
						</form>
					</div>
					<p class="has-text-grey">
						<router-link to="register">Register</router-link>&nbsp;·&nbsp;
						<a href="../">Forgot Password</a> &nbsp;·&nbsp;
					</p>
				</div>
			</div>
		</div>
	</section>
</template>
<script>
export default {
	data() {
		return {
			message: '',
			email: '',
			password: '',
		};
	},
	methods: {
		submit() {
			var loginRequest = {
				TitleId: 45299,
				Email: this.email,
				Password: this.password,
			};

			PlayFabClientSDK.LoginWithEmailAddress(loginRequest, this.LoginCallback);
		},

		LoginCallback(result, error) {
			if (result !== null) {
				this.$store.commit('setUserId', result.data.PlayFabId);
				this.$router.push({ path: '/profile' });
			} else if (error !== null) {
				this.message = error.errorMessage;
			}
		},

		logout() {
			/*this.$auth.logout();
      this.$router.push({ path: "/" });*/
		},
	},
};
</script>
