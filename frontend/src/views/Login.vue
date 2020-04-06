<template>
  <section class="hero">
    <div class="hero-body">
      <div class="container has-text-centered">
        <div class="column is-4 is-offset-3">
          <h3 class="title has-text-black">Login</h3>
          <hr class="login-hr" />
          <p class="subtitle has-text-black">Please login to proceed.</p>
          <div class="box">
            <form @submit.prevent="submit">
              <div class="field">
                <div class="control">
                  <input
                    class="input is-large"
                    type="email"
                    v-model="email"
                    placeholder="Your Email"
                    autofocus
                  />
                </div>
              </div>

              <div class="field">
                <div class="control">
                  <input
                    class="input is-large"
                    v-model="password"
                    type="password"
                    placeholder="Your Password"
                  />
                </div>
              </div>
              <div class="field">
                <label class="checkbox">
                  <input type="checkbox" />
                  Remember me
                </label>
              </div>
              <button
                @click="submit"
                type="submit"
                class="button is-block is-primary is-large is-fullwidth"
              >Login</button>
            </form>
          </div>
          <!--<p class="has-text-grey">
						<a href="../">Sign Up</a> &nbsp;·&nbsp; <a href="../">Forgot Password</a> &nbsp;·&nbsp;
						<a href="../">Need Help?</a>
          </p>-->
        </div>
      </div>
    </div>
  </section>
</template>
<script>
export default {
  data() {
    return {
      email: "",
      password: ""
    };
  },
  methods: {
    submit() {
      var loginRequest = {
        TitleId: 45299,
        Email: this.email,
        Password: this.password,
        RequireBothUsernameAndEmail: false
      };

      PlayFabClientSDK.RegisterPlayFabUser(loginRequest, this.LoginCallback);
    },

    LoginCallback(result, error) {
      if (result !== null) {
        console.log(result);
      } else if (error !== null) {
        console.log(error);
      }
    },

    logout() {
      /*this.$auth.logout();
      this.$router.push({ path: "/" });*/
    }
  }
};
</script>
