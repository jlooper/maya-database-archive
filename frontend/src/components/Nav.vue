<template web>
  <div>
    <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <router-link class="navbar-item" :to="'./'">
          <span class="navbar-item logo">
            <img src="/images/logo.png" />
          </span>
          <span class="navbar-item is-size-3">Maya Hieroglyphic Database and Archive</span>
        </router-link>
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

            <nav class="menu auth">
              <div class="menu-list auth">
                <div v-if="userInfo" class="navbar-item" @click="logout">Logout</div>
              </div>
            </nav>

            <span class="navbar-item">
              <div class="dropdown is-hoverable">
                <div class="dropdown-trigger">
                  <button class="button" aria-haspopup="true" aria-controls="dropdown-menu">
                    <span>Language</span>
                    <span class="icon is-small">
                      <i class="fas fa-angle-down" aria-hidden="true"></i>
                    </span>
                  </button>
                </div>
                <div class="dropdown-menu" id="dropdown-menu" role="menu">
                  <div class="dropdown-content">
                    <a href="#" class="dropdown-item">English</a>
                    <a class="dropdown-item">Español</a>
                  </div>
                </div>
              </div>
            </span>
          </div>
        </div>
      </div>
    </nav>

    <div class="has-background-link" height="5px">
      <div v-if="userInfo">
        <p class="welcome">
          Welcome, {{ userInfo.userDetails }}! View your
          <router-link :to="'profile'">profile</router-link>
        </p>
      </div>
      <div v-else>&nbsp;</div>
    </div>
  </div>
</template>

<script web>
import { mapState } from "vuex";

export default {
  name: "my-navbar",

  data() {
    return {
      isLoggedIn: false,
      userInfo: Object
    };
  },
  computed: {
    ...mapState(["userId"])
  },
  methods: {
    logout() {
      this.$store.commit("clearUserId");
      this.isLoggedIn = false;
      const redirect = `post_logout_redirect_uri=/home`;
      const url = `/.auth/logout?${redirect}`;
      window.location.href = url;
    },
    async getUserInfo() {
      try {
        const response = await fetch("/.auth/me");
        const payload = await response.json();
        const { clientPrincipal } = payload;
        this.$store.commit("setUserId", clientPrincipal.userId);
        this.isLoggedIn = true;
        return clientPrincipal;
      } catch (error) {
        return undefined;
      }
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
  async created() {
    this.userInfo = await this.getUserInfo();
    // eslint-disable-next-line no-console
    console.log(this.userInfo);
  }
};
</script>

<style>
.main-content {
  margin-top: 30px;
}
.welcome,
.welcome a {
  color: white;
  font-weight: bold;
  padding: 5px;
}
.welcome a {
  text-decoration: underline;
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
