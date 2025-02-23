/**
 * plugins/vuetify.js
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import "@mdi/font/css/materialdesignicons.css";
import DayJsAdapter from "@date-io/dayjs";
import "vuetify/styles";

// Composables
import { createVuetify } from "vuetify";

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
const light = {
  dark: false,
  colors: {
    background: "#ECEFF1",
    surface: "#FAFAFA",
    "surface-variant": "#F2F2F2",
    "on-surface-variant": "#424242",
    primary: "#1867C0",
    "primary-darken-1": "#1F5592",
    secondary: "#48A9A6",
    "secondary-darken-1": "#018786",
    error: "#B00020",
    info: "#2196F3",
    success: "#4CAF50",
    warning: "#FB8C00",
  },
};

const dark = {
  dark: true,
  colors: {
    background: "#1A1A1A",
    surface: "#212121",
    "surface-variant": "#303030",
    "on-surface-variant": "#EEEEEE",
    primary: "#2196F3",
    "primary-darken-1": "#1976D2",
    secondary: "#BB86FC",
    "secondary-darken-1": "#9C27B0",
    error: "#CF6679",
    info: "#03A9F4",
    success: "#4CAF50",
    warning: "#FB8C00",
  },
};

export default createVuetify({
  date: {
    adapter: DayJsAdapter,
  },
  theme: {
    defaultTheme: "light",
    themes: {
      light,
      dark,
    },
    variations: {
      colors: ["primary", "secondary"],
      lighten: 3,
      darken: 3,
    },
  },
});
