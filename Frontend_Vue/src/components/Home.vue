<template>
  <v-container fluid class="dashboard-container">
    <v-container class="mt-10">
      <v-row dense>
        <v-col
          v-for="stat in statsList"
          :key="stat.title"
          cols="12"
          sm="3"
          md="3"
        >
          <v-card
            v-motion-pop-visible
            class="pa-4 stat-card elevation-10 mx-15"
            outlined
            :to="stat.route"
          >
            <v-card-title class="justify-center">
              <v-icon size="36" color="primary">{{ stat.icon }}</v-icon>
            </v-card-title>
            <v-card-text class="text-center">
              <div class="display-1 font-weight-bold">{{ stat.value }}</div>
              <div class="subtitle-1">{{ stat.title }}</div>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row class="mt-12">
        <v-col cols="12">
          <v-card v-motion-pop-visible outlined class="pa-4 elevation-10">
            <v-card-title>Data Trends</v-card-title>
            <v-divider></v-divider>
            <v-card-text>
              <v-row>
                <v-col cols="12" md="6">
                  <div class="mb-2 font-weight-bold">Employee Growth</div>
                  <v-sparkline
                    :model-value="sparklineData.employees"
                    height="50"
                    line-width="5"
                    color="blue-lighten-2"
                  />
                </v-col>
                <v-col cols="12" md="6">
                  <div class="mb-2 font-weight-bold">Project Trends</div>
                  <v-sparkline
                    :model-value="sparklineData.projects"
                    line-width="5"
                    color="green-lighten-2"
                  />
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";

const stats = ref({
  employees: 0,
  services: 0,
  projects: 0,
  customers: 0,
});

const sparklineData = ref({
  employees: [3, 4, 5, 6, 7, 7, 9],
  projects: [1, 0, 3, 1, 7, 8, 9],
});

// Update statsList reactively based on stats values
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
]);

const selectedDate = ref("2025-02-11");

const events = ref([
  {
    name: "Project Deadline",
    start: "2025-02-15",
    end: "2025-02-15",
    color: "red",
  },
  {
    name: "Team Meeting",
    start: "2025-02-20",
    end: "2025-02-20",
    color: "blue",
  },
]);

async function fetchStats() {
  try {
    const responseEmployees = await fetch(
      "http://192.168.1.6:5000/api/employees"
    );
    const dataEmployees = await responseEmployees.json();
    console.log("Employees:", dataEmployees);
    stats.value.employees = dataEmployees.count || dataEmployees.length || 0;

    const responseServices = await fetch(
      "http://192.168.1.6:5000/api/services"
    );
    const dataServices = await responseServices.json();
    console.log("Services:", dataServices);
    stats.value.services = dataServices.count || dataServices.length || 0;

    const responseProjects = await fetch(
      "http://192.168.1.6:5000/api/projects"
    );
    const dataProjects = await responseProjects.json();
    console.log("Projects:", dataProjects);
    stats.value.projects = dataProjects.count || dataProjects.length || 0;

    const responseCustomers = await fetch(
      "http://192.168.1.6:5000/api/customers"
    );
    const dataCustomers = await responseCustomers.json();
    console.log("Customers:", dataCustomers);
    stats.value.customers = dataCustomers.count || dataCustomers.length || 0;
  } catch (error) {
    console.error("Error fetching stats:", error);
  }
}

onMounted(() => {
  fetchStats();
});
</script>

<style scoped>
.dashboard-container {
  background: linear-gradient(180deg, #121212, #363636, #555555);
  min-height: 100vh;
}

.mt-10 {
  margin-top: 10rem;
}

.stat-card {
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  cursor: pointer;
}
.stat-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.12);
}
</style>
