/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from "@/plugins";
import { MotionPlugin } from "@vueuse/motion";

// Components
import App from "./App.vue";

// Composables
import { createApp } from "vue";

const app = createApp(App);

registerPlugins(app);
app.use(MotionPlugin);
app.mount("#app");
