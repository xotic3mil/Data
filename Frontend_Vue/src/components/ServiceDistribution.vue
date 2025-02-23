<template>
  <v-card flat class="elevation-5">
    <v-card-title class="d-flex align-center">
      <v-icon start color="primary" class="mr-2">mdi-chart-bar</v-icon>
      Service Distribution
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
        No service data available
      </div>
      <e-charts v-else class="chart" :option="chartOption" autoresize />
    </v-card-text>
  </v-card>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import ECharts from "vue-echarts";
import { use } from "echarts/core";
import { BarChart } from "echarts/charts";
import {
  TitleComponent,
  TooltipComponent,
  LegendComponent,
  GridComponent,
} from "echarts/components";
import { CanvasRenderer } from "echarts/renderers";
import { useTheme } from "vuetify";
import { fetchServiceDistribution } from "@/endpoints/serviceEndpoint";

// Register ECharts components
use([
  BarChart,
  CanvasRenderer,
  TitleComponent,
  TooltipComponent,
  LegendComponent,
  GridComponent,
]);

const theme = useTheme();
const loading = ref(true);
const textColor = computed(() =>
  theme.current.value.dark ? "#ffffff" : "#000000"
);

const chartOption = ref({
  tooltip: {
    trigger: "axis",
    axisPointer: {
      type: "shadow",
    },
  },
  grid: {
    left: "10%",
    right: "4%",
    bottom: "15%",
    containLabel: true,
  },
  xAxis: {
    type: "category",
    data: [],
    axisLabel: {
      interval: 0,
      rotate: 45,
      color: computed(() => textColor.value),
    },
  },
  yAxis: {
    type: "value",
    name: "Number of Customers",
    nameLocation: "middle",
    nameGap: 50,
    axisLabel: {
      interval: 0,
      rotate: 45,
      color: computed(() => textColor.value),
    },
  },
  series: [
    {
      name: "Customers",
      type: "bar",
      data: [],
      itemStyle: {
        color: "#3498db",
      },
      label: {
        show: true,
        position: "top",
        color: computed(() => textColor.value),
      },
    },
  ],
});

const getServiceData = async () => {
  loading.value = true;
  try {
    const data = await fetchServiceDistribution();

    // Format data for chart
    chartOption.value.xAxis.data = data.map((item) => item.serviceName);
    chartOption.value.series[0].data = data.map((item) => ({
      value: item.customerCount,
      itemStyle: {
        color: "#3498db",
      },
    }));
  } catch (error) {
    console.error("Error loading service distribution:", error);
  } finally {
    loading.value = false;
  }
};

onMounted(async () => {
  await getServiceData();
});
</script>

<style scoped>
.chart {
  height: 400px;
}
</style>
