<template>
  <div>
    <section class="hero">
      <div class="hero-body">
        <div class="columns is-vcentered">
          <div class="column is-half is-offset-1 is-centered home-elements">
            <h1 class="title is-1 is-bold home-elements">Welcome!</h1>
            <h2 class="subtitle is-3 home-elements">
              The Maya Database and Archive can help you discover Maya Glyphs
            </h2>

            <router-link class="button is-large is-link" :to="'search'"
              >Search the Database</router-link
            >

            <!--<div class="button-box">
              <b-button
                class="social-button twitter"
                @click="Auth('twitter')"
                size="is-medium"
                icon-left="twitter"
              >Login with Twitter</b-button>

              <b-button
                class="social-button facebook"
                @click="Auth('facebook')"
                size="is-medium"
                icon-left="facebook"
              >Login with Facebook</b-button>

              <b-button
                class="social-button google"
                @click="Auth('google')"
                size="is-medium"
                icon-left="google"
              >Login with Google</b-button>
            </div>-->
          </div>

          <div class="column is-half is-centered">
            <img src="../assets/home-glyph.png" />
          </div>
        </div>
      </div>
    </section>

    <section class="section has-background-link is-full">
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
                  <p>
                    Learn about the various glyphs that make up the Mayan
                    language, how they fit together, and how they are altered
                    depending on context.
                  </p>
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
                  <p>
                    Search for instances of glyphs using maps and tagged
                    searches. Discover where artifacts originated and where they
                    are currently stored.
                  </p>
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
                  <p>
                    Save your favorite searches by creating an account. Login,
                    and research your areas of interest.
                  </p>
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
  name: "app",
  methods: {
    Auth(provider) {
      const redirect = `post_login_redirect_uri=/home`;
      const url = `/.auth/login/${provider}?${redirect}`;
      window.location.href = url;
    },
  },
};
</script>

<style scoped>
.button-box {
  margin-top: 10px;
}
.social-button {
  margin-right: 5px;
}
.twitter {
  color: white;
  background-color: #55acee;
}
.facebook {
  color: white;
  background-color: #3b5998;
}
.google {
  color: white;
  background-color: #34a853;
}
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
  padding-bottom: 50px;
}
</style>
