<template web>
  <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <a class="navbar-item" href="./">
        <span class="navbar-item is-size-3 has-text-white">Maya Hieroglyphic Database and Archive</span>
      </a>
      <a
        id="burger"
        role="button"
        class="navbar-burger burger"
        aria-label="menu"
        aria-expanded="false"
        data-target="menu"
      >
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div id="menu" class="navbar-menu">
      <div class="navbar-end">
        <div class="navbar-item">
          <router-link class="navbar-item has-text-white" :to="'about'">About</router-link>
          <span v-if="!$auth.isAuthenticated">
            <button id="qsLoginBtn" class="button is-warning" @click.prevent="login">Login</button>
          </span>

          <span v-if="$auth.isAuthenticated">
            <router-link :to="'profile'" class="navbar-item has-text-white">Profile</router-link>
            <button id="qsLogoutBtn" class="button is-warning" @click.prevent="logout">Logout</button>
          </span>
        </div>
      </div>
    </div>
  </nav>
</template>

<script web>
import { mapState } from "vuex";

export default {
  name: "my-navbar",
  computed: {
    ...mapState(["user"])
  },

  methods: {
    login() {
      this.$auth.loginWithRedirect().then(res => {
        console.log(res);
        this.$router.push({ path: "/profile" });
      });
    },
    logout() {
      this.$auth.logout();
      this.$router.push({ path: "/" });
    }
  },
  mounted: function() {
    var burger = document.getElementById("burger");
    var menu = document.getElementById("menu");
    burger.addEventListener("click", function() {
      burger.classList.toggle("is-active");
      menu.classList.toggle("is-active");
    });
  }
};
</script>

<style>
.main-content {
  margin-top: 30px;
}
.circle {
  width: 100px;
  border-radius: 50px;
  background-color: white;
}
.wrapper {
  flex: 1;
}

a.navbar-item:hover {
  background-color: transparent;
}
</style>
