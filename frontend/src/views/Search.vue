<template>
	<main class="column is-four-fifths main is-pulled-right">
		<div class="box main-content">
			<h1 class="title">Search</h1>
			<hr />
			<form @submit.prevent="submit">
				<div class="field is-grouped">
					<p class="control is-expanded">
						<input
							:class="validClass"
							v-model.trim="$v.search_field_1.$model"
							type="text"
							placeholder="Search the database"
						/>
					</p>
					<div class="control">
						<div class="select">
							<select name="search_option_1" v-model.trim="search_option_1">
								<option selected>Select field</option>
								<option value="class">class</option>
								<option value="technique">technique</option>
								<option value="material">material</option>
							</select>
						</div>
					</div>
					<div class="field">
						<div class="control">
							<button
								type="submit"
								:disabled="submitStatus === 'PENDING'"
								class="button is-link"
								@click="submit"
							>
								Search
							</button>
						</div>
					</div>
				</div>
				<div v-if="submitStatus == 'ERROR'">
					<p class="help is-danger">Field is required</p>
				</div>
			</form>
		</div>
	</main>
</template>
<style scoped>
p {
	margin-bottom: 20px;
}
</style>
<script>
import { required } from 'vuelidate/lib/validators';

export default {
	validations: {
		search_field_1: {
			required,
		},
	},
	data: () => ({
		submitStatus: null,
		message: '',
		validClass: 'input',
		search_field_1: null,
		search_option_1: null,
	}),
	methods: {
		submit() {
			this.$v.$touch();
			if (this.search_field_1 == null) {
				this.submitStatus = 'ERROR';
				this.validClass = 'input is-danger';
			} else {
				this.submitStatus = 'PENDING';
				this.validClass = 'input';
				//start search
				let option1 = this.search_option_1;
				let param1 = this.search_field_1;
				this.axios.request('https://mayan-glyphs-api.azurewebsites.net/api/artifacts', {
					method: 'post',
					params: {
						param1: option1,
					},
				});
				setTimeout(() => {
					this.submitStatus = 'OK';
					this.validClass = 'input';
				}, 500);
			}
		},
	},
};
</script>
