<template>
	<main class="column is-four-fifths main is-pulled-right">
		<div id="app">
			<div id="myMap"></div>
		</div>
	</main>
</template>

<script>
import * as atlas from 'azure-maps-control';

export default {
	name: 'app',
	data: () => ({
		map: null,
	}),
	methods: {
		async initMap() {
			this.map = new atlas.Map('myMap', {
				center: [-110, 50],
				zoom: 2,
				view: 'Auto',
				authOptions: {
					authType: 'subscriptionKey',
					subscriptionKey: 'zXrEZijBp0cyk83B3HjR0ffIRLFJsLqxdXqQ9zKIdjU',
				},
			});
			await this.buildMap();
		},
		buildMap() {
			let self = this;
			self.map.events.add('ready', function() {
				//Create a data source and add it to the map.
				let datasource = new atlas.source.DataSource();
				self.map.sources.add(datasource);
				//Load a data set of points, in this case earthquake data from the USGS.
				datasource.importDataFromUrl(
					'https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_week.geojson'
				);

				//Create a heatmap and add it to the map.
				self.map.layers.add(
					new atlas.layer.HeatMapLayer(datasource, null, {
						radius: 10,
						opacity: 0.8,
					}),
					'labels'
				);
			});
		},
	},
	mounted() {
		this.initMap();
	},
};
</script>

<style scoped>
#myMap {
	height: 75vh;
	width: 100vw;
}
</style>
