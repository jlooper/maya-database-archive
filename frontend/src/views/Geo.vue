<template>
  <main class="column is-four-fifths main is-pulled-right">
    <div id="app">
      <div id="myMap"></div>
    </div>
  </main>
</template>
<script>
import * as atlas from "azure-maps-control";
import data from "@/assets/sites.json";

export default {
  name: "app",
  data: () => ({
    map: null,
  }),
  methods: {
    async initMap() {
      this.map = new atlas.Map("myMap", {
        center: [-90.128591, 16.823058],
        zoom: 5,
        view: "Auto",
        authOptions: {
          authType: "subscriptionKey",
          subscriptionKey: process.env["VUE_APP_MAP_KEY"],
        },
      });
      await this.buildMap();
    },
    buildMap() {
      let self = this;
      self.map.events.add("ready", function () {
        //Create a data source and add it to the map.
        let mapSource = new atlas.source.DataSource();
        self.map.sources.add(mapSource);
        mapSource.add(data);

        //Create a heatmap and add it to the map.
        /*self.map.layers.add(
          new atlas.layer.HeatMapLayer(datasource, null, {
            radius: 10,
            opacity: 0.8,
          }),
          "labels"
        );*/

        let popupSource = new atlas.source.DataSource();
        self.map.sources.add(popupSource);
        popupSource.add(data);
        //add a popup
        var symbolLayer = new atlas.layer.SymbolLayer(popupSource);

        //Add the polygon and line the symbol layer to the map.
        self.map.layers.add(symbolLayer);
        var popupTemplate =
          '<div class="customInfobox"><div class="name">{sitecode}<br/>{sitename}</div>{refShort}</div>';

        //Create a popup but leave it closed so we can update it and display it later.
        let popup = new atlas.Popup({
          pixelOffset: [0, -18],
          closeButton: false,
        });

        //Add a hover event to the symbol layer.
        self.map.events.add("mouseover", symbolLayer, function (e) {
          //Make sure that the point exists.
          if (e.shapes && e.shapes.length > 0) {
            var content, coordinate;
            var properties = e.shapes[0].getProperties();
            content = popupTemplate
              .replace(/{sitecode}/g, properties.sitecode)
              .replace(/{sitename}/g, properties.sitename)
              .replace(/{refShort}/g, properties.refShort);
            coordinate = e.shapes[0].getCoordinates();

            popup.setOptions({
              content: content,
              position: coordinate,
            });

            popup.open(self.map);
          }
        });

        self.map.events.add("mouseleave", symbolLayer, function () {
          popup.close();
        });
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

.name {
  padding: 3px;
  margin: 5px;
}
</style>

