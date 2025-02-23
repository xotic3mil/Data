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
            class="stat-card"
            :to="stat.route"
            elevation="0"
          >
            <div class="stat-card__content">
              <v-icon
                size="36"
                :color="stat.iconColor || 'primary'"
                class="stat-card__icon"
              >
                {{ stat.icon }}
              </v-icon>
              <div class="stat-card__value">{{ stat.value }}</div>
              <div class="stat-card__title">{{ stat.title }}</div>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
    <v-container>
      <v-row>
        <v-col cols="12" md="6">
          <Calendar />
        </v-col>
        <v-col cols="12" md="6">
          <PieChart />
          <br />
          <ServiceDistribution />
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { fetchStats } from "../endpoints/apiService.js";
import Calendar from "../components/Calendar.vue";
import PieChart from "./PieChart.vue";
import ServiceDistribution from "./ServiceDistribution.vue";

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
    title: "Services",
    value: stats.value.services,
    icon: "mdi-cog-outline",
    iconColor: "blue",
    route: "/services",
  },
  {
    title: "Projects",
    value: stats.value.projects,
    icon: "mdi-briefcase-outline",
    iconColor: "green",
    route: "/projects",
  },
  {
    title: "Customers",
    value: stats.value.customers,
    icon: "mdi-account",
    iconColor: "orange",
    route: "/customers",
  },
  {
    title: "Total Monthly Revenue",
    value: formattedRevenue.value,
    icon: "mdi-currency-usd",
    iconColor: "success",
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
  border-radius: 16px;
  background: #212121;
  backdrop-filter: blur(10px);
  border: 3px solid rgba(var(--v-border-color), 0.05);
}

.stat-card__content {
  padding: 2rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 1rem;
}

.stat-card__icon {
  padding: 1rem;
  border-radius: 12px;
  background: rgba(var(--v-theme-primary), 0.1);
  transition: transform 0.3s ease;
}

.stat-card__value {
  font-size: 1.5rem;
  font-weight: 600;
  color: var(--v-theme-on-surface);
}

.stat-card__title {
  font-size: 0.875rem;
  color: var(--v-theme-on-surface-variant);
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

/* Card hover effects */
.stat-card {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}

.stat-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.15), 0 10px 10px rgba(0, 0, 0, 0.12);
}

.stat-card:hover .stat-card__icon {
  transform: scale(1.1);
}

/* Responsive adjustments */
@media (max-width: 600px) {
  .stat-card__content {
    padding: 1.5rem;
  }

  .stat-card__value {
    font-size: 1.25rem;
  }
}
</style>
