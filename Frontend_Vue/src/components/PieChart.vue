<template>
  <v-card flat class="elevation-5">
    <v-card-title class="d-flex align-center">
      <v-icon start color="primary" class="mr-2">mdi-chart-pie</v-icon>
      Project Status Distribution
    </v-card-title>
    <v-card-text>
      <div v-if="loading" class="d-flex justify-center align-center pa-4">
        <v-progress-circular
          indeterminate
          color="primary"
        ></v-progress-circular>
      </div>
      <div
        v-else-if="chartOption.series[0].data.length === 0"
        class="text-center pa-4"
      >
        No project data available
      </div>
      <e-charts v-else class="chart" :option="chartOption" autoresize />
    </v-card-text>
  </v-card>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import ECharts from "vue-echarts";
import { use } from "echarts/core";
import { PieChart } from "echarts/charts";
import {
  TitleComponent,
  TooltipComponent,
  LegendComponent,
} from "echarts/components";
import { CanvasRenderer } from "echarts/renderers";
import { fetchProjectStatusData } from "@/endpoints/projectEndpoint";
import { useTheme } from "vuetify";
// ...existing imports...

const theme = useTheme();

// Add computed property for text color
const textColor = computed(() =>
  theme.current.value.dark ? "#ffffff" : "#000000"
);

use([
  PieChart,
  CanvasRenderer,
  TitleComponent,
  TooltipComponent,
  LegendComponent,
]);

const loading = ref(true);
const chartOption = ref({
  tooltip: {
    trigger: "item",
    formatter: "{b}: {c} Projects ({d}%)",
  },
  legend: {
    orient: "vertical",
    left: "left",
    padding: 5,
    itemGap: 10,
    type: "scroll",
    textStyle: {
      fontSize: 12,
      color: computed(() => textColor.value),
    },
  },
  series: [
    {
      name: "Project Status",
      type: "pie",
      radius: ["40%", "70%"], //
      avoidLabelOverlap: true,
      itemStyle: {
        borderRadius: 10,
        borderColor: computed(() => textColor.value),
        borderWidth: 2,
      },
      label: {
        show: true,
        formatter: "{b}\n{c} ({d}%)",
        position: "outer",
        alignTo: "none",
        bleedMargin: 5,
        color: computed(() => textColor.value),
        textBorderColor: "rgba(0, 0, 0, 0.3)", // Add text border for better visibility
        textBorderWidth: 1,
        textShadowBlur: 0,
        textShadowColor: "rgba(0, 0, 0, 0.5)",
      },
      emphasis: {
        label: {
          show: true,
          fontSize: 14,
          fontWeight: "bold",
          color: computed(() => textColor.value),
        },
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: "rgba(0, 0, 0, 0.5)",
        },
      },
      data: [],
    },
  ],
});

const statusColors = {
  New: "#3498db", // Blue
  "On Going": "#2ecc71", // Green
  Open: "#95a5a6", // Gray
  "In Progress": "#f39c12", // Orange
  Pending: "#e74c3c", // Red
  "On Hold": "#f1c40f", // Yellow
  Resolved: "#27ae60", // Dark Green
  Closed: "#34495e", // Dark Blue
  Reopened: "#9b59b6", // Purple
  Cancelled: "#c0392b", // Dark Red
  Archived: "#7f8c8d", // Dark Gray
};

const getProjectStatusData = async () => {
  loading.value = true;
  try {
    const statusData = await fetchProjectStatusData();

    console.log("Received status data:", statusData); // Debug log

    const chartData = statusData.map((item) => ({
      name: item.name,
      value: parseInt(item.value), // Ensure value is a number
      itemStyle: {
        color: statusColors[item.name] || "#95a5a6",
      },
    }));

    console.log("Chart data:", chartData); // Debug log
    chartOption.value.series[0].data = chartData;
  } catch (error) {
    console.error("Error fetching project status data:", error);
  } finally {
    loading.value = false;
  }
};

onMounted(async () => {
  await getProjectStatusData();
});
</script>

<style scoped>
.chart {
  height: 400px;
}
</style>
