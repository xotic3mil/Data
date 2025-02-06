<script setup>
import { ref, onMounted } from "vue";
import StatCard from "./StatCard.vue";

// Dummy stats; replace with API calls as needed.
const stats = ref({
  employees: 25,
  services: 10,
  projects: 3,
  customers: 40,
});

onMounted(() => {
  // Fetch your statistics from APIs and update stats.value here.
});
</script>

<template>
  <div>
    <h1 class="text-center py-10">Dashboard</h1>
    <v-container>
      <v-row dense>
        <v-col cols="12" sm="6" md="3">
          <StatCard :value="stats.employees" icon="mdi-account-group" />
        </v-col>
        <v-col cols="12" sm="6" md="3">
          <StatCard :value="stats.services" icon="mdi-cog-outline" />
        </v-col>
        <v-col cols="12" sm="6" md="3">
          <StatCard :value="stats.projects" icon="mdi-briefcase-outline" />
        </v-col>
        <v-col cols="12" sm="6" md="3">
          <StatCard :value="stats.customers" icon="mdi-account" />
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
