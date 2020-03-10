<template web>
  <main class="column is-four-fifths main">
    <div class="box main-content">
      <h1 class="title is-size-3">Welcome</h1>

      <router-link></router-link>
    </div>
  </main>
</template>

<script>
//import { mapState, mapActions } from "vuex";
export default {
  name: "landing",
  computed: {
    //...mapState([["students"], "teacher", "user", ["practices"]])
  },
  data: () => ({
    //currentUser: firebase.auth().currentUser
  }),

  methods: {
    //...mapActions(["fetchUser", "fetchStudents", "fetchPractices"]),
  },

  created() {
    /*this.fetchUser(this.currentUser.uid)*/
  }
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
