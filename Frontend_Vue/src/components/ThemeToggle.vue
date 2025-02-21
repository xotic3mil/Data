<template>
  <v-btn @click="toggleTheme" icon variant="text">
    <v-icon>{{ isDark ? "mdi-weather-sunny" : "mdi-weather-night" }}</v-icon>
  </v-btn>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useTheme } from "vuetify";

const theme = useTheme();
const isDark = ref(theme.global.current.value.dark);

function toggleTheme() {
  theme.global.name.value = isDark.value ? "light" : "dark"; // Change theme names
  isDark.value = !isDark.value;
  localStorage.setItem("theme", theme.global.name.value);
}

onMounted(() => {
  const savedTheme = localStorage.getItem("theme") || "light";
  theme.global.name.value = savedTheme;
  isDark.value = savedTheme === "dark";
});
</script>

<style scoped>
.theme-toggle {
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 999;
  transition: transform 0.3s ease;
}

.theme-toggle:hover {
  transform: rotate(30deg);
}
</style>
