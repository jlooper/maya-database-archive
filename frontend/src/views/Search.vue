<template>
	<main class="column is-four-fifths main is-pulled-right">
		<div id="search" class="box main-content">
			<h1 class="title">Search</h1>
			<hr />
			<form @submit.prevent="submit">
				<div class="field is-grouped">
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
					<p class="control is-expanded">
						<input
							:class="validClass"
							v-model.trim="$v.search_field_1.$model"
							type="text"
							placeholder="Search the database"
						/>
					</p>
					<div class="field">
						<div class="control">
							<button type="submit" :disabled="submitStatus === 'PENDING'" class="button is-link">
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
		<section v-if="artifacts.length > 0" style="overflow-x: scroll;">
			<b-field grouped group-multiline>
				<b-select v-model="defaultSortDirection">
					<option value="asc">Default sort direction: ASC</option>
					<option value="desc">Default sort direction: DESC</option>
				</b-select>
				<b-select v-model="perPage" :disabled="!isPaginated">
					<option value="5">5 per page</option>
					<option value="10">10 per page</option>
					<option value="15">15 per page</option>
					<option value="20">20 per page</option>
				</b-select>

				<div class="control is-flex">
					<b-switch v-model="isPaginated">Paginated</b-switch>
				</div>
				<div class="control is-flex">
					<b-switch v-model="isPaginationSimple" :disabled="!isPaginated">Simple pagination</b-switch>
				</div>
			</b-field>

			<b-table
				:data="artifacts"
				:paginated="isPaginated"
				:per-page="perPage"
				:current-page.sync="currentPage"
				:pagination-simple="isPaginationSimple"
				:pagination-position="paginationPosition"
				:default-sort-direction="defaultSortDirection"
				:sort-icon="sortIcon"
				:sort-icon-size="sortIconSize"
				default-sort="RecId"
				aria-next-label="Next page"
				aria-previous-label="Previous page"
				aria-page-label="Page"
				aria-current-label="Current page"
			>
				<template slot-scope="props">
					<b-table-column field="RecId" label="ID" width="40" sortable numeric>
						{{ props.row.RecId }}
					</b-table-column>

					<b-table-column field="Class" label="Class" sortable>
						{{ props.row.Class }}
					</b-table-column>

					<b-table-column field="Material" label="Material" sortable>
						{{ props.row.Material }}
					</b-table-column>

					<b-table-column field="Technique" label="Technique" sortable centered>
						{{ props.row.Technique }}
					</b-table-column>

					<b-table-column field="RegionOrigin" label="Region Origin" width="40" sortable numeric>
						{{ props.row.RegionOrigin }}
					</b-table-column>
					<b-table-column field="RegionDestination" label="Region Destination" width="40" sortable numeric>
						{{ props.row.RegionDestination }}
					</b-table-column>
					<b-table-column field="BlockSort" label="Block Sort" width="40" sortable numeric>
						{{ props.row.BlockSort }}
					</b-table-column>
					<b-table-column field="Jabbr1" label="Jabbr1" width="40" sortable numeric>
						{{ props.row.Jabbr1 }}
					</b-table-column>
					<b-table-column field="SiteOrigin" label="Site Origin" width="40" sortable numeric>
						{{ props.row.SiteOrigin }}
					</b-table-column>
					<b-table-column
						field="SiteCodeDestination"
						label="Site Code Destination"
						width="40"
						sortable
						numeric
					>
						{{ props.row.SiteCodeDestination }}
					</b-table-column>
					<b-table-column field="MayanArtist" label="Mayan Artist" width="40" sortable numeric>
						{{ props.row.MayanArtist }}
					</b-table-column>
					<b-table-column field="Cal" label="Cal" width="40" sortable numeric>
						{{ props.row.Cal }}
					</b-table-column>
					<b-table-column field="LC" label="LC" width="40" sortable numeric>
						{{ props.row.LC }}
					</b-table-column>
					<b-table-column field="Cycle260" label="Cycle260" width="40" sortable numeric>
						{{ props.row.Cycle260 }}
					</b-table-column>
					<b-table-column field="Cycle365" label="Cycle365" width="40" sortable numeric>
						{{ props.row.Cycle365 }}
					</b-table-column>
					<b-table-column field="HellmuthNum" label="HellmuthNum" width="40" sortable numeric>
						{{ props.row.HellmuthNum }}
					</b-table-column>
					<b-table-column field="MSNum" label="MSNum" width="40" sortable numeric>
						{{ props.row.MSNum }}
					</b-table-column>
					<b-table-column field="Surface" label="Surface" width="40" sortable numeric>
						{{ props.row.Surface }}
					</b-table-column>
				</template>
			</b-table>
		</section>
	</main>
</template>

<script>
import { required } from 'vuelidate/lib/validators';
import axios from 'axios';
export default {
	validations: {
		search_field_1: {
			required,
		},
	},

	data: function() {
		return {
			submitStatus: null,
			artifacts: [],
			message: '',
			validClass: 'input',
			search_field_1: null,
			search_option_1: null,
			isPaginated: true,
			isPaginationSimple: false,
			paginationPosition: 'bottom',
			defaultSortDirection: 'asc',
			sortIcon: 'arrow-up',
			sortIconSize: 'is-small',
			currentPage: 1,
			perPage: 5,
		};
	},

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
				let option2 = this.search_field_1;
				axios
					.get(
						`https://mayan-glyphs.azurewebsites.net/odata/Artifacts?$filter=${option1}%20eq%20%27${option2}%27&$top=30`
					)
					.then(response => {
						this.artifacts = response.data.value;
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

<style scoped>
p {
	margin-bottom: 20px;
}
</style>
