<template>
  <v-card flat class="elevation-5">
    <v-card-title class="d-flex align-center">
      <v-icon start color="primary" class="mr-2">mdi-timeline</v-icon>
      Active Projects Timeline
    </v-card-title>
    <v-card-text>
      <v-timeline density="compact" align="start">
        <v-timeline-item
          v-for="project in activeProjects"
          :key="project.id"
          :dot-color="getPriorityColor(project.priority)"
          size="small"
        >
          <template v-slot:opposite>
            {{ formatDate(project.startDate) }}
          </template>
          <div class="d-flex justify-space-between align-center">
            <div>
              <div class="text-subtitle-2">{{ project.name }}</div>
              <div class="text-caption">
                Due: {{ formatDate(project.endDate) }}
              </div>
            </div>
            <v-chip
              :color="getStatusColor(project.status.status)"
              size="small"
              class="ml-2"
            >
              {{ project.status.status }}
            </v-chip>
          </div>
        </v-timeline-item>
      </v-timeline>
    </v-card-text>
  </v-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { format } from "date-fns";
import { fetchTimelineProjects } from "@/endpoints/projectEndpoint";

const activeProjects = ref([]);
const loading = ref(false);
const error = ref(null);

const formatDate = (date) => {
  return format(new Date(date), "MMM dd, yyyy");
};

const getPriorityColor = (priority) => {
  const colors = {
    Low: "blue",
    Medium: "orange",
    High: "red",
    Critical: "deep-purple-darken-2",
  };
  return colors[priority] || "grey";
};

const getStatusColor = (status) => {
  const colors = {
    New: "blue",
    "On Going": "green",
    Open: "grey",
    "In Progress": "orange",
    Pending: "red",
    "On Hold": "yellow",
    Resolved: "green-darken-2",
    Closed: "blue-grey",
    Reopened: "purple",
    Cancelled: "red-darken-2",
    Archived: "grey-darken-2",
  };
  return colors[status] || "grey";
};

const fetchActiveProjects = async () => {
  loading.value = true;
  error.value = null;

  try {
    const projects = await fetchTimelineProjects();

    if (!projects || projects.length === 0) {
      error.value = "No projects found";
      return;
    }

    // Filter and sort active projects
    activeProjects.value = projects
      .filter((project) => {
        const status = project.status?.status;
        return (
          status &&
          status !== "Closed" &&
          status !== "Cancelled" &&
          status !== "Archived"
        );
      })
      .sort((a, b) => new Date(a.startDate) - new Date(b.startDate));

    console.log("Active projects:", activeProjects.value);
  } catch (err) {
    console.error("Error fetching active projects:", err);
    error.value = "Failed to load projects timeline";
  } finally {
    loading.value = false;
  }
};

onMounted(async () => {
  await fetchActiveProjects();
});
</script>

<style scoped>
.v-timeline {
  max-height: 500px;
  overflow-y: auto;
}

.v-timeline::-webkit-scrollbar {
  width: 8px;
}

.v-timeline::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

.v-timeline::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 4px;
}

.v-timeline::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>
