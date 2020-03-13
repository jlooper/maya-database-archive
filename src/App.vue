<template>
  <div id="app">
    <Nav />
    <div class="wrapper">
      <Menu />
      <router-view />
    </div>

    <Footer />
  </div>
</template>
<script>
import Nav from "@/components/Nav";
import Footer from "@/components/Footer";
import Menu from "@/components/Menu";

export default {
  components: { Nav, Footer, Menu }
};
</script>

<style scoped>
footer {
  bottom: 0;
  left: 0;
  position: fixed;
  right: 0;
  z-index: 30;
  padding: 10px;
}
main {
  margin-top: 20px;
}
</style>
