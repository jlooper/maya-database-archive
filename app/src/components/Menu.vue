<template>
  <div class>
    <aside v-show="showMenu" class="column is-one-fifth aside is-pulled-left">
      <nav class="menu">
        <ul class="menu-list">
          <!--<li>
            <router-link class="navbar-item" :to="'/profile'">
              <i class="fas fa-user"></i> My Profile
            </router-link>
          </li>-->

          <li>
            <router-link class="navbar-item" :to="'/map'">
              <i class="fas fa-map"></i> Map
            </router-link>
          </li>

          <li>
            <router-link class="navbar-item" :to="'/search'">
              <i class="fas fa-search"></i> Search
            </router-link>
          </li>
        </ul>
      </nav>
    </aside>
  </div>
</template>

<script>
//import { mapState, mapActions } from "vuex";
export default {
  computed: {
    //...mapState(["user"])
    showMenu() {
      return this.$router.app._route.name !== "home";
    },
  },
  data: () => ({
    //currentUser: firebase.auth().currentUser,
  }),
  methods: {
    //...mapActions(['fetchUser', 'fetchStudents', 'fetchPractices']),
  },
};
</script>
