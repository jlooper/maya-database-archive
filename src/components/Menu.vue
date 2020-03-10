<template>
  <aside v-if="this.user != null" class="column is-2 aside">
    <nav class="menu">
      <ul class="menu-list">
        <li>
          <router-link class="navbar-item" :to="'/home'">
            <i class="fas fa-headphones"></i> 1
          </router-link>
        </li>

        <li>
          <router-link class="navbar-item" :to="'/practice'">
            <i class="fas fa-music"></i> 2
          </router-link>
        </li>

        <li>
          <router-link class="navbar-item" :to="'/settings'">
            <i class="fa fa-cog"></i> 3
          </router-link>
        </li>
      </ul>
    </nav>
  </aside>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  computed: {
    ...mapState(["user"])
  },
  data: () => ({
    //currentUser: firebase.auth().currentUser,
  }),
  methods: {
    //...mapActions(['fetchUser', 'fetchStudents', 'fetchPractices']),
  }
};
</script>
