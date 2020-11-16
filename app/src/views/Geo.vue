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
import axios from "axios";
export default {
  name: "app",
  data: () => ({
    map: null,
    zoom: 5,
    center: [-90.128591, 16.823058],
  }),
  methods: {
    async initMap(key) {
      this.map = new atlas.Map("myMap", {
        center: this.center,
        zoom: this.zoom,
        view: "Auto",
        authOptions: {
          authType: "subscriptionKey",
          subscriptionKey: key,
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

        let popupSource = new atlas.source.DataSource();
        self.map.sources.add(popupSource);
        popupSource.add(data);
        //add a popup
        var symbolLayer = new atlas.layer.SymbolLayer(popupSource);

        //Add the polygon and line the symbol layer to the map.
        self.map.layers.add(symbolLayer);
        var popupTemplate =
          '<div style="padding:10px;color:white;font-size:11pt;font-weight:bold">{sitecode}<br/>{sitename}</div>';

        //Create a popup but leave it closed so we can update it and display it later.
        let popup = new atlas.Popup({
          pixelOffset: [0, -18],
          closeButton: true,
          fillColor: "rgba(0,0,0,0.8)",
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
  async mounted() {
    await axios
      .get("/api/map")
      .then((response) => {
        console.log(response);
        let key = response.data;

        this.initMap(key);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  created() {
    if (this.$route.params.site) {
      //zoom in to a particular site if one is specified
      for (var i = 0; i < data.features.length; i++) {
        if (data.features[i].properties.sitecode == this.$route.params.site) {
          this.center = data.features[i].geometry.coordinates;
          this.zoom = 12;
        }
      }
    }
  },
};
</script>

<style scoped>
#myMap {
  height: 75vh;
  width: 100vw;
}
</style>

