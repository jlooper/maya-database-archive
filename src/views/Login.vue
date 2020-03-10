<template>
  <div>
    <section class="hero">
      <div class="hero-body">
        <div class="columns is-vcentered">
          <div class="column is-half is-offset-1 is-centered home-elements">
            <h1 class="title is-1 is-bold home-elements">Welcome!</h1>
            <h2
              class="subtitle is-3 home-elements"
            >The Maya Database and Archive can help you discover Maya Glyphs</h2>

            <a
              class="home-elements button is-full is-warning is-rounded is-large"
              @click="loginWithGoogle()"
            >
              <span>Login</span>
            </a>
          </div>

          <div class="column is-half is-centered">
            <img src="../assets/home-glyph.jpg" />
          </div>
        </div>
      </div>
    </section>

    <section class="section has-background-white-ter is-full">
      <div class="container">
        <div class="content-wrapper">
          <div class="columns">
            <div class="column">
              <div class="card has-text-centered">
                <p class="title is-4">Learn</p>
                <div class="card-icon">
                  <img src="../assets/3.png" />
                </div>
                <div class="card-text">
                  <p>Learn about the various glyphs that make up the Mayan language, how they fit together, and how they are altered depending on context.</p>
                </div>
              </div>
            </div>
            <div class="column">
              <div class="card has-text-centered">
                <p class="title is-4">Search</p>
                <div class="card-icon">
                  <img src="../assets/2.png" />
                </div>
                <div class="card-text">
                  <p>Search for instances of glyphs using maps and tagged searches. Discover where artifacts originated and where they are currently stored.</p>
                </div>
              </div>
            </div>
            <div class="column">
              <div class="card has-text-centered">
                <p class="title is-4">Save</p>
                <div class="card-icon">
                  <img src="../assets/1.png" />
                </div>
                <div class="card-text">
                  <p>Save your favorite searches by creating an account. Login, and research your areas of interest.</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>
<script>
export default {
  name: "Home",
  props: {
    msg: String
  },
  data() {
    return {};
  },
  methods: {
    loginWithGoogle() {
      /*const provider = new firebase.auth.GoogleAuthProvider();
      firebase
        .auth()
        .signInWithPopup(provider)
        .then(res => {
          // save user to db
          let uid = res.user.uid;
          let data = {};
          data = {
            name: res.user.displayName,
            email: res.user.email
          };

          firebase
            .firestore()
            .collection("users")
            .doc(uid)
            .set(data, { merge: true });
        })
        .then(() => {
          this.$router.replace("home");
        })
        .catch(err => {
          // TODO:
          alert(err.message);
        });*/
    }
  }
};
</script>

<style scoped>
.card {
  border-radius: 5px;
  margin: 5px;
  padding: 10px;
  height: 100%;
}

.card-text,
.title {
  padding-top: 10px;
}
.content-wrapper {
  padding-bottom: 10px;
}
</style>
