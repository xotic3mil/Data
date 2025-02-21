<template>
  <v-app>
    <NavMenu />
    <v-main>
      <ThemeToggle class="theme-toggle elevation-10" />
      <router-view />
    </v-main>
    <Footer />
  </v-app>
</template>

<script setup>
import { onMounted } from "vue";
import NavMenu from "@/components/NavMenu.vue";
import Footer from "@/components/Footer.vue";
import ThemeToggle from "@/components/ThemeToggle.vue";

onMounted(() => {
  const savedTheme = localStorage.getItem("theme") || "dark";
  document.documentElement.setAttribute("data-theme", savedTheme);
});
</script>

<style>
.theme-toggle {
  position: fixed;
  top: 7px;
  right: 20px;
  z-index: 1015 !important; /* Increased z-index and added !important */
}
</style>
