<template web>
  <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <a class="navbar-item" href="./"></a>
      <span class="navbar-item is-size-3 has-text-white">Maya Hieroglyphic Database and Archive</span>
      <a
        id="example-burger"
        role="button"
        class="navbar-burger burger"
        aria-label="menu"
        aria-expanded="false"
        data-target="example-menu"
      >
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div id="example-menu" class="navbar-menu">
      <div class="navbar-end">
        <div v-if="user != null" class="navbar-item">
          <router-link class="navbar-item has-text-white" :to="'about'">About</router-link>

          <div class="buttons" @click="logout">
            <a class="button is-danger">Logout</a>
          </div>
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
    logout() {}
  },
  mounted: function() {
    var burger = document.getElementById("example-burger");
    var menu = document.getElementById("example-menu");
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
</style>
