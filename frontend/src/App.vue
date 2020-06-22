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
import Nav from '@/components/Nav';
import Footer from '@/components/Footer';
import Menu from '@/components/Menu';

export default {
	components: { Nav, Footer, Menu },
};
</script>

<style lang="scss">
@import '~bulma/sass/utilities/_all';
$primary: #bd8d46;
$base: #fcfae1;
$info: #f6e497;
$warning: #b9121b;
$link: #9a0b09;
$colors: (
	'white': (
		$white,
		$black,
	),
	'black': (
		$black,
		$white,
	),
	'light': (
		$light,
		$light-invert,
	),
	'dark': (
		$dark,
		$dark-invert,
	),
	'primary': (
		$primary,
		$primary-invert,
	),
	'info': (
		$info,
		$info-invert,
	),
	'success': (
		$success,
		$success-invert,
	),
	'warning': (
		$warning,
		$warning-invert,
	),
	'danger': (
		$danger,
		$danger-invert,
	),
);

@import '~bulma';
@import '~buefy/src/scss/buefy';

footer {
	bottom: 0;
	left: 0;
	position: fixed;
	right: 0;
	z-index: 30;
	padding: 10px;
}
</style>
