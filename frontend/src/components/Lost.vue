<template web>
  <main class="column is-four-fifths main is-pulled-right">
    <div class="box main-content">
      <h1 class="title has-text-centered">Sorry, friend, you're in the wrong place!</h1>
      <div class="card-image has-text-centered">
        <figure class="image is-inline-block">
          <img src="../assets/god.png" />
        </figure>
      </div>
      <p class="has-text-centered is-size-4">
        <router-link to="/home">Go Home</router-link>
      </p>
    </div>
  </main>
</template>

<style scoped>
p {
  margin-bottom: 20px;
}
.is-horizontal-centered {
  justify-content: center;
}
</style>
