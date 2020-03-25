<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-one-quarter"></div>
    <div class="column">
      <div class="box main-content">
        <h1 class="title has-text-centered">Sorry, friend, you're in the wrong place!</h1>
        <div class="card-image has-text-centered">
          <figure class="image is-inline-block">
            <img src="../assets/god.png" />
          </figure>
        </div>
        <p class="has-text-centered is-size-4">
          <router-link to="./">Go Home</router-link>
        </p>
      </div>
    </div>
    <div class="column is-one-quarter"></div>
  </div>
</template>

<style scoped>
p {
  margin-bottom: 20px;
}
.is-horizontal-centered {
  justify-content: center;
}
</style>
<script>
export default {
  data() {
    return {
      //showMenu: false
    };
  }
};
</script>