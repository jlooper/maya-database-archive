<template>
  <main class="column is-four-fifths main is-pulled-right">
    <div id="search" class="box main-content">
      <h1 class="title">Search</h1>
      <p class="has-text-danger">{{ message }}</p>
      <hr />
      <form @submit.prevent="submit">
        <div class="field is-grouped">
          <div class="control">
            <div class="select">
              <div class="select">
                <select
                  :class="validClass"
                  v-model.trim="$v.search_option_1.$model"
                >
                  <option value>Select</option>
                  <option v-for="c in cols" v-bind:key="c">{{ c }}</option>
                </select>
              </div>
            </div>
          </div>
          <div class="field and">=</div>
          <p class="control is-expanded">
            <input
              :class="validClass"
              v-model.trim="$v.search_field_1.$model"
              type="text"
            />
          </p>
        </div>

        <div class="field is-grouped">
          <p class="and">and</p>
          <div class="control">
            <div class="select">
              <select v-model="search_option_2">
                <option value>Select</option>
                <option v-for="c in cols" v-bind:key="c">{{ c }}</option>
              </select>
            </div>
          </div>
          <div class="field and">=</div>
          <p class="control is-expanded">
            <input class="input" v-model="search_field_2" type="text" />
          </p>
        </div>
        <div class="field">
          <div class="control">
            <button type="reset" class="button is-info">Clear</button>

            <button
              type="submit"
              :disabled="submitStatus === 'PENDING'"
              class="button is-link"
            >
              Search
            </button>
          </div>
        </div>
        <div v-if="submitStatus == 'ERROR'">
          <p class="help is-danger">Field is required</p>
        </div>
      </form>
    </div>

    <section v-if="artifacts.length > 0" style="overflow-x: scroll">
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
          <b-switch size="is-small" type="is-warning" v-model="isPaginated"
            >Paginated</b-switch
          >
        </div>
        <div class="control is-flex">
          <b-switch
            size="is-small"
            type="is-warning"
            v-model="isPaginationSimple"
            :disabled="!isPaginated"
            >Simple pagination</b-switch
          >
        </div>
      </b-field>

      <b-field grouped group-multiline>
        <div
          v-for="(column, index) in columnsVisible"
          :key="index"
          class="control"
        >
          <b-checkbox
            size="is-small"
            type="is-light"
            v-model="column.display"
            >{{ column.title }}</b-checkbox
          >
        </div>
      </b-field>
      <div class="wrapper">
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
          default-sort="Id"
          aria-next-label="Next page"
          aria-previous-label="Previous page"
          aria-page-label="Page"
          aria-current-label="Current page"
        >
          <template slot-scope="props">
            <b-table-column
              field="Almanpgs"
              :label="columnsVisible['Almanpgs'].title"
              :visible="columnsVisible['Almanpgs'].display"
              width="40"
              sortable
              numeric
              >{{ props.row.Almanpgs }}</b-table-column
            >
            <b-table-column
              field="BImage1"
              :label="columnsVisible['BImage1'].title"
              :visible="columnsVisible['BImage1'].display"
              width="40"
              sortable
              numeric
            >
              <b-button
                @click="openImageModal(props.row.BImage1)"
                v-if="props.row.BImage1 !== null"
                icon-right="image"
              ></b-button>
            </b-table-column>
            <b-table-column
              field="BImage2"
              :label="columnsVisible['BImage2'].title"
              :visible="columnsVisible['BImage2'].display"
              width="40"
              sortable
              numeric
            >
              <b-button
                @click="openImageModal(props.row.BImage2)"
                v-if="props.row.BImage2 !== null"
                icon-right="image"
              ></b-button>
            </b-table-column>

            <b-table-column
              field="Class"
              :label="columnsVisible['Class'].title"
              :visible="columnsVisible['Class'].display"
              sortable
              >{{ props.row.Class }}</b-table-column
            >

            <b-table-column
              field="Material"
              :label="columnsVisible['Material'].title"
              :visible="columnsVisible['Material'].display"
              sortable
              >{{ props.row.Material }}</b-table-column
            >

            <b-table-column
              field="Technique"
              :label="columnsVisible['Technique'].title"
              :visible="columnsVisible['Technique'].display"
              sortable
              >{{ props.row.Technique }}</b-table-column
            >

            <b-table-column
              field="RegionOrigin"
              :label="columnsVisible['RegionOrigin'].title"
              :visible="columnsVisible['RegionOrigin'].display"
              width="40"
              sortable
              >{{ props.row.RegionOrigin }}</b-table-column
            >
            <b-table-column
              field="RegionDestination"
              :label="columnsVisible['RegionDestination'].title"
              :visible="columnsVisible['RegionDestination'].display"
              width="40"
              sortable
              >{{ props.row.RegionDestination }}</b-table-column
            >
            <b-table-column
              field="BlockSort"
              :label="columnsVisible['BlockSort'].title"
              :visible="columnsVisible['BlockSort'].display"
              width="40"
              sortable
              >{{ props.row.BlockSort }}</b-table-column
            >
            <b-table-column
              field="Jabbr1"
              :label="columnsVisible['Jabbr1'].title"
              :visible="columnsVisible['Jabbr1'].display"
              width="40"
              sortable
              >{{ props.row.Jabbr1 }}</b-table-column
            >
            <b-table-column
              field="SiteOrigin"
              :label="columnsVisible['SiteOrigin'].title"
              :visible="columnsVisible['SiteOrigin'].display"
              width="40"
              sortable
              >{{ props.row.SiteOrigin }}</b-table-column
            >
            <b-table-column
              field="SiteCodeDestination"
              :label="columnsVisible['SiteCodeDestination'].title"
              :visible="columnsVisible['SiteCodeDestination'].display"
              width="40"
              sortable
            >
              <router-link :to="'map/' + props.row.SiteCodeDestination + ''">{{
                props.row.SiteCodeDestination
              }}</router-link>
            </b-table-column>
            <b-table-column
              field="MayanArtist"
              :label="columnsVisible['MayanArtist'].title"
              :visible="columnsVisible['MayanArtist'].display"
              width="40"
              sortable
              >{{ props.row.MayanArtist }}</b-table-column
            >
            <b-table-column
              field="Cal"
              :label="columnsVisible['Cal'].title"
              :visible="columnsVisible['Cal'].display"
              width="40"
              sortable
              >{{ props.row.Cal }}</b-table-column
            >
            <b-table-column
              field="LC"
              :label="columnsVisible['LC'].title"
              :visible="columnsVisible['LC'].display"
              width="40"
              sortable
              >{{ props.row.LC }}</b-table-column
            >
            <b-table-column
              field="Cycle260"
              :label="columnsVisible['Cycle260'].title"
              :visible="columnsVisible['Cycle260'].display"
              width="40"
              sortable
              >{{ props.row.Cycle260 }}</b-table-column
            >
            <b-table-column
              field="Cycle365"
              :label="columnsVisible['Cycle365'].title"
              :visible="columnsVisible['Cycle365'].display"
              width="40"
              sortable
              >{{ props.row.Cycle365 }}</b-table-column
            >
            <b-table-column
              field="HellmuthNum"
              :label="columnsVisible['HellmuthNum'].title"
              :visible="columnsVisible['HellmuthNum'].display"
              width="40"
              sortable
              >{{ props.row.HellmuthNum }}</b-table-column
            >
            <b-table-column
              field="MSNum"
              :label="columnsVisible['MSNum'].title"
              :visible="columnsVisible['MSNum'].display"
              width="40"
              sortable
              >{{ props.row.MSNum }}</b-table-column
            >
            <b-table-column
              field="Surface"
              :label="columnsVisible['Surface'].title"
              :visible="columnsVisible['Surface'].display"
              width="40"
              sortable
              >{{ props.row.Surface }}</b-table-column
            >
          </template>
        </b-table>
      </div>
    </section>
    <b-loading
      :is-full-page="isFullPage"
      :active.sync="artifacts.length == 0 && isLoading"
      :can-cancel="true"
    ></b-loading>
  </main>
</template>

<script>
import { required } from "vuelidate/lib/validators";
import axios from "axios";

export default {
  validations: {
    search_field_1: {
      required,
    },
    search_option_1: {
      required,
    },
  },
  computed: {
    cols() {
      var keys = Object.keys(this.columnsVisible);
      return keys;
    },
  },

  data: function () {
    return {
      submitStatus: null,
      isLoading: false,
      isFullPage: false,
      artifacts: [],
      message: "",
      validClass: "input",
      search_field_1: null,
      search_option_1: null,
      search_option_2: null,
      search_field_2: null,
      isPaginated: true,
      isPaginationSimple: false,
      paginationPosition: "bottom",
      defaultSortDirection: "asc",
      sortIcon: "arrow-up",
      sortIconSize: "is-small",
      currentPage: 1,
      perPage: 5,
      columnsVisible: {
        //Id: { title: "Id", display: false },
        Almanpgs: { title: "Almanpgs", display: true },
        BImage1: { title: "BImage1", display: true },
        BImage2: { title: "BImage2", display: true },
        Class: { title: "Class", display: true },
        Material: { title: "Material", display: true },
        Technique: { title: "Technique", display: true },
        RegionOrigin: { title: "Region Origin", display: true },
        RegionDestination: { title: "Region Destination", display: true },
        BlockSort: { title: "Block Sort", display: true },
        Jabbr1: { title: "Jabbr1", display: true },
        SiteOrigin: { title: "Site Origin", display: true },
        SiteCodeDestination: { title: "Site Code Destination", display: true },
        MayanArtist: { title: "Mayan Artist", display: true },
        Cal: { title: "Cal", display: true },
        LC: { title: "LC", display: true },
        Cycle260: { title: "Cycle260", display: true },
        Cycle365: { title: "Cycle365", display: true },
        HellmuthNum: { title: "HellmuthNum", display: true },
        MSNum: { title: "MSNum", display: true },
        Surface: { title: "Surface", display: true },
      },
    };
  },

  methods: {
    openImageModal(img) {
      this.$buefy.modal.open(
        `<p class="image is-4by3">
            <img :src=https://mayadatabaseblob.blob.core.windows.net/maya-db-container/${img}>
          </p>`
      );
    },
    submit() {
      this.isLoading = true;
      this.message = "";
      this.artifacts = [];
      this.$v.$touch();
      if (this.search_field_1 == null) {
        this.isLoading = false;
        this.submitStatus = "ERROR";
        this.validClass = "input is-danger";
      } else {
        this.submitStatus = "PENDING";
        this.validClass = "input";
        //start search
        this.isLoading = true;

        let option1 = this.search_option_1;
        let option2 = this.search_option_2;
        let field1 = this.search_field_1;
        let field2 = this.search_field_2;

        let filter1 = `` + option1 + ` eq "` + field1 + `"`;
        let filter2 = `&` + option2 + ` eq "` + field2 + `"`;

        let filter = filter1;

        if (option2 != null) {
          filter = filter1 + filter2;
        }

        axios
          .get("/api/artifacts", {
            params: {
              filter: filter,
              limit: 10,
            },
          })
          .then((response) => {
            if (response.data.items.length > 0) {
              this.isLoading = false;
              this.submitStatus = "COMPLETE";
              this.artifacts = response.data.items;
            } else {
              this.isLoading = false;
              this.submitStatus = "COMPLETE";
              this.message = "Sorry, no records found";
            }
          })
          .catch((err) => {
            this.isLoading = false;
            this.submitStatus = "COMPLETE";
            this.message = err;
          });
      }
    },
  },
};
</script>

<style scoped>
p {
  margin-bottom: 20px;
}
.wrapper {
  overflow-x: auto;
}
.wrapper table {
  white-space: nowrap;
}
.and {
  padding: 5px;
  font-weight: bold;
}
.button {
  margin: 5px;
}
</style>
