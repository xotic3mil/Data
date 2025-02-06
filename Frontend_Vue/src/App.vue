<template>
  <v-app>
    <NavMenu />
    <v-main>
      <router-view />
    </v-main>
    <Footer />
  </v-app>
</template>

<script setup>
import NavMenu from "@/components/NavMenu.vue";
import Footer from "@/components/Footer.vue";
</script>
