<template>
	<main class="column is-four-fifths main is-pulled-right">
		<div id="search" class="box main-content">
			<h1 class="title">Search</h1>
			<hr />
			<form @submit.prevent="submit">
				<div class="field is-grouped">
					<div class="control">
						<div class="select">
							<select :class="validClass" name="search_option_1" v-model.trim="$v.search_option_1.$model">
								<option value="class">Class</option>
								<option value="technique">Technique</option>
								<option value="material">Material</option>
							</select>
						</div>
					</div>
					<div class="control">
						<div class="select">
							<select :class="validClass" name="search_option_2" v-model.trim="$v.search_option_2.$model">
								<option value="eq">=</option>
								<option value="like">Like</option>
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
		<p>{{ message }}</p>

		<section v-if="artifacts.length > 0" style="overflow-x: scroll;">
			<b-field grouped group-multiline>
				<b-select size="is-small" v-model="defaultSortDirection">
					<option value="asc">Default sort direction: ASC</option>
					<option value="desc">Default sort direction: DESC</option>
				</b-select>
				<b-select size="is-small" v-model="perPage" :disabled="!isPaginated">
					<option value="5">5 per page</option>
					<option value="10">10 per page</option>
					<option value="15">15 per page</option>
					<option value="20">20 per page</option>
				</b-select>

				<div class="control is-flex">
					<b-switch size="is-small" type="is-warning" v-model="isPaginated">Paginated</b-switch>
				</div>
				<div class="control is-flex">
					<b-switch size="is-small" type="is-warning" v-model="isPaginationSimple" :disabled="!isPaginated"
						>Simple pagination</b-switch
					>
				</div>
			</b-field>

			<b-field grouped group-multiline>
				<div v-for="(column, index) in columnsVisible" :key="index" class="control">
					<b-checkbox size="is-small" type="is-light" v-model="column.display">
						{{ column.title }}
					</b-checkbox>
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
				:striped="true"
				default-sort="RecId"
				aria-next-label="Next page"
				aria-previous-label="Previous page"
				aria-page-label="Page"
				aria-current-label="Current page"
			>
				<template slot-scope="props">
					<b-table-column
						field="RecId"
						:label="columnsVisible['RecId'].title"
						:visible="columnsVisible['RecId'].display"
						width="40"
						sortable
						numeric
					>
						{{ props.row.RecId }}
					</b-table-column>

					<b-table-column
						field="Class"
						:label="columnsVisible['Class'].title"
						:visible="columnsVisible['Class'].display"
						sortable
					>
						{{ props.row.Class }}
					</b-table-column>

					<b-table-column
						field="Material"
						:label="columnsVisible['Material'].title"
						:visible="columnsVisible['Material'].display"
						sortable
					>
						{{ props.row.Material }}
					</b-table-column>

					<b-table-column
						field="Technique"
						:label="columnsVisible['Technique'].title"
						:visible="columnsVisible['Technique'].display"
						sortable
					>
						{{ props.row.Technique }}
					</b-table-column>

					<b-table-column
						field="RegionOrigin"
						:label="columnsVisible['RegionOrigin'].title"
						:visible="columnsVisible['RegionOrigin'].display"
						width="40"
						sortable
					>
						{{ props.row.RegionOrigin }}
					</b-table-column>
					<b-table-column
						field="RegionDestination"
						:label="columnsVisible['RegionDestination'].title"
						:visible="columnsVisible['RegionDestination'].display"
						width="40"
						sortable
					>
						{{ props.row.RegionDestination }}
					</b-table-column>
					<b-table-column
						field="BlockSort"
						:label="columnsVisible['BlockSort'].title"
						:visible="columnsVisible['BlockSort'].display"
						width="40"
						sortable
					>
						{{ props.row.BlockSort }}
					</b-table-column>
					<b-table-column
						field="Jabbr1"
						:label="columnsVisible['Jabbr1'].title"
						:visible="columnsVisible['Jabbr1'].display"
						width="40"
						sortable
					>
						{{ props.row.Jabbr1 }}
					</b-table-column>
					<b-table-column
						field="SiteOrigin"
						:label="columnsVisible['SiteOrigin'].title"
						:visible="columnsVisible['SiteOrigin'].display"
						width="40"
						sortable
					>
						{{ props.row.SiteOrigin }}
					</b-table-column>
					<b-table-column
						field="SiteCodeDestination"
						:label="columnsVisible['SiteCodeDestination'].title"
						:visible="columnsVisible['SiteCodeDestination'].display"
						width="40"
						sortable
					>
						{{ props.row.SiteCodeDestination }}
					</b-table-column>
					<b-table-column
						field="MayanArtist"
						:label="columnsVisible['MayanArtist'].title"
						:visible="columnsVisible['MayanArtist'].display"
						width="40"
						sortable
					>
						{{ props.row.MayanArtist }}
					</b-table-column>
					<b-table-column
						field="Cal"
						:label="columnsVisible['Cal'].title"
						:visible="columnsVisible['Cal'].display"
						width="40"
						sortable
					>
						{{ props.row.Cal }}
					</b-table-column>
					<b-table-column
						field="LC"
						:label="columnsVisible['LC'].title"
						:visible="columnsVisible['LC'].display"
						width="40"
						sortable
					>
						{{ props.row.LC }}
					</b-table-column>
					<b-table-column
						field="Cycle260"
						:label="columnsVisible['Cycle260'].title"
						:visible="columnsVisible['Cycle260'].display"
						width="40"
						sortable
					>
						{{ props.row.Cycle260 }}
					</b-table-column>
					<b-table-column
						field="Cycle365"
						:label="columnsVisible['Cycle365'].title"
						:visible="columnsVisible['Cycle365'].display"
						width="40"
						sortable
					>
						{{ props.row.Cycle365 }}
					</b-table-column>
					<b-table-column
						field="HellmuthNum"
						:label="columnsVisible['HellmuthNum'].title"
						:visible="columnsVisible['HellmuthNum'].display"
						width="40"
						sortable
					>
						{{ props.row.HellmuthNum }}
					</b-table-column>
					<b-table-column
						field="MSNum"
						:label="columnsVisible['MSNum'].title"
						:visible="columnsVisible['MSNum'].display"
						width="40"
						sortable
					>
						{{ props.row.MSNum }}
					</b-table-column>
					<b-table-column
						field="Surface"
						:label="columnsVisible['Surface'].title"
						:visible="columnsVisible['Surface'].display"
						width="40"
						sortable
					>
						{{ props.row.Surface }}
					</b-table-column>
				</template>
			</b-table>
		</section>
		<b-loading :is-full-page="false" :active.sync="isLoading" :can-cancel="true"></b-loading>
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
		search_option_1: {
			required,
		},
		search_option_2: {
			required,
		},
	},

	data: function() {
		return {
			submitStatus: null,
			isLoading: false,
			artifacts: [],
			message: '',
			validClass: 'input',
			search_field_1: null,
			search_option_1: 'Select',
			search_option_2: 'eq',
			isPaginated: true,
			isPaginationSimple: false,
			paginationPosition: 'bottom',
			defaultSortDirection: 'asc',
			sortIcon: 'arrow-up',
			sortIconSize: 'is-small',
			currentPage: 1,
			perPage: 5,
			columnsVisible: {
				RecId: { title: 'Id', display: true },
				Class: { title: 'Class', display: true },
				Material: { title: 'Material', display: true },
				Technique: { title: 'Technique', display: true },
				RegionOrigin: { title: 'Region Origin', display: true },
				RegionDestination: { title: 'Region Destination', display: true },
				BlockSort: { title: 'Block Sort', display: true },
				Jabbr1: { title: 'Jabbr1', display: true },
				SiteOrigin: { title: 'Site Origin', display: true },
				SiteCodeDestination: { title: 'Site Code Destination', display: true },
				MayanArtist: { title: 'Mayan Artist', display: true },
				Cal: { title: 'Cal', display: true },
				LC: { title: 'LC', display: true },
				Cycle260: { title: 'Cycle260', display: true },
				Cycle365: { title: 'Cycle365', display: true },
				HellmuthNum: { title: 'HellmuthNum', display: true },
				MSNum: { title: 'MSNum', display: true },
				Surface: { title: 'Surface', display: true },
			},
		};
	},

	methods: {
		submit() {
			this.isLoading = true;
			this.message = '';
			this.artifacts = [];
			this.$v.$touch();
			if (this.search_field_1 == null) {
				this.submitStatus = 'ERROR';
				this.validClass = 'input is-danger';
			} else {
				this.submitStatus = 'PENDING';
				this.validClass = 'input';
				//start search
				let option1 = this.search_option_1;
				let option2 = this.search_option_2;
				let field1 = this.search_field_1;
				axios
					.get(
						`https://mayan-glyphs.azurewebsites.net/odata/Artifacts?$filter=${option1}%20${option2}%20%27${field1}%27&$top=30`
					)
					.then(response => {
						this.isLoading = false;
						if (response.data.value.length > 0) {
							this.artifacts = response.data.value;
						} else {
							this.message = 'Sorry, no records found.';
						}
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
