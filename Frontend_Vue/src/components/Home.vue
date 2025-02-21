<template>
  <v-container fluid>
    <v-container>
      <v-row default>
        <v-col
          v-for="stat in statsList"
          :key="stat.title"
          cols="12"
          sm="6"
          md="3"
        >
          <v-card
            v-motion-pop-visible
            class="pa-10 stat-card elevation-10"
            outlined
            :to="stat.route"
          >
            <v-card-title class="justify-center">
              <v-icon size="36" color="primary">{{ stat.icon }}</v-icon>
            </v-card-title>
            <v-card-text class="text-center">
              <div style="font-size: 1rem" class="display-1font-weight-bold">
                {{ stat.value }}
              </div>
              <div style="font-size: 1rem" class="subtitle-1">
                {{ stat.title }}
              </div>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { fetchStats } from "../endpoints/apiService.js";

const stats = ref({
  employees: 0,
  services: 0,
  projects: 0,
  customers: 0,
  revenue: 0,
});

const formattedRevenue = computed(() => {
  return new Intl.NumberFormat("sv-SE", {
    style: "currency",
    currency: "SEK",
  }).format(stats.value.revenue);
});

const statsList = computed(() => [
  {
    title: "Employees",
    value: stats.value.employees,
    icon: "mdi-account-group",
    route: "/employees",
  },
  
  {
    title: "Services",
    value: stats.value.services,
    icon: "mdi-cog-outline",
    route: "/services",
  },
  {
    title: "Projects",
    value: stats.value.projects,
    icon: "mdi-briefcase-outline",
    route: "/projects",
  },
  {
    title: "Customers",
    value: stats.value.customers,
    icon: "mdi-account",
    route: "/customers",
  },
  {
    title: "Total Monthly Revenue",
    value: formattedRevenue.value,
    icon: "mdi-currency-usd",
    route: "/",
  },
]);

onMounted(async () => {
  try {
    stats.value = await fetchStats();
  } catch (error) {
    console.error("Error fetching stats:", error);
  }
});
</script>

<style scoped>
.stat-card {
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  cursor: pointer;
}
.stat-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.12);
}
</style>
