<template web>
  <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <a class="navbar-item" href="./">
        <span class="navbar-item logo">
          <img src="/images/logo.png" />
        </span>
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
          <router-link class="navbar-item" :to="'about'">About</router-link>
          <span v-if="!$auth.isAuthenticated && loadAuth">
            <button id="qsLoginBtn" class="button is-warning" @click.prevent="login">Login</button>
          </span>

          <span v-if="$auth.isAuthenticated">
            <router-link :to="'profile'" class="navbar-item">Profile</router-link>
            <button id="qsLogoutBtn" class="button is-warning" @click.prevent="logout">Logout</button>
          </span>
        </div>
      </div>
    </div>
  </nav>
</template>

<script web>
//import { mapState } from "vuex";
import { getInstance } from "../auth/authWrapper";

export default {
  name: "my-navbar",
  computed: {
    //...mapState(["user"])
  },
  data() {
    return {
      loadAuth: false
    };
  },
  methods: {
    login() {
      this.$auth.loginWithRedirect().then(res => {
        this.$router.push({ path: "/profile" });
      });
    },
    logout() {
      this.$auth.logout();
      this.$router.push({ path: "/" });
    },
    init(fn) {
      // have to do this nonsense to make sure auth0Client is ready
      var instance = getInstance();
      instance.$watch("loading", loading => {
        if (loading === false) {
          fn(instance);
        }
      });
    },
    async checkAuth(instance) {
      await instance.getTokenSilently().then(authToken => {
        // eslint-disable-next-line no-console
        console.log(authToken);
        this.loadAuth = true;
        // do authorized API calls with auth0 authToken here
      });
    }
  },
  mounted() {
    var burger = document.getElementById("burger");
    var menu = document.getElementById("menu");
    burger.addEventListener("click", function() {
      burger.classList.toggle("is-active");
      menu.classList.toggle("is-active");
    });
  },
  created() {
    this.init(this.checkAuth);
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

.navbar-item img {
  min-height: 4rem;
}
</style>
