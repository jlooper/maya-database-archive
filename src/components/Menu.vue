<template>
  <aside v-if="this.user != null" class="column is-2 aside">
    <nav class="menu">
      <ul class="menu-list">
        <li>
          <router-link class="navbar-item" :to="'/profile'">
            <i class="fas fa-user"></i>My Profile
          </router-link>
        </li>

        <li>
          <router-link class="navbar-item" :to="'/search'">
            <i class="fas fa-search"></i>Search
          </router-link>
        </li>
      </ul>
    </nav>
  </aside>
</template>

<script>
//import { mapState, mapActions } from "vuex";
export default {
  computed: {
    //...mapState(["user"])
  },
  data: () => ({
    //currentUser: firebase.auth().currentUser,
  }),
  methods: {
    //...mapActions(['fetchUser', 'fetchStudents', 'fetchPractices']),
  }
};
</script>
