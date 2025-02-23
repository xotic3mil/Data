<template>
  <v-row class="fill-height">
    <v-col>
      <v-card flat class="elevation-5 rounded-lg">
        <v-card-title class="d-flex align-center justify-space-between">
          <div class="d-flex align-center">
            <v-icon start color="primary" class="mr-2">mdi-calendar</v-icon>
            Project Calendar
          </div>
        </v-card-title>

        <v-divider></v-divider>

        <v-card-text>
          <div v-if="loading" class="d-flex justify-center align-center pa-4">
            <v-progress-circular
              indeterminate
              color="primary"
            ></v-progress-circular>
          </div>

          <v-alert
            v-if="error"
            type="error"
            variant="tonal"
            closable
            class="mb-4"
          >
            {{ error }}
          </v-alert>

          <v-calendar
            ref="calendar"
            :model-value="selectedDate"
            :events="events"
            :type="calendarType"
            :weekdays="[1, 2, 3, 4, 5, 6, 7]"
            color="primary"
            show-current
            @click:event="showEvent"
          >
            <template v-slot:event="{ event }">
              <div class="d-flex align-center">
                <v-icon
                  :color="
                    event.details.type === 'start'
                      ? 'green'
                      : getPriorityColor(event.details?.priority)
                  "
                  size="small"
                  class="mr-1"
                >
                  {{
                    event.details.type === "start"
                      ? "mdi-flag-plus"
                      : "mdi-flag-checkered"
                  }}
                </v-icon>
                <div class="text-truncate">
                  {{ event.name }} <br />
                  {{ event.projectName }}
                </div>
                <v-tooltip activator="parent" location="bottom">
                  <v-card flat class="pa-2">
                    <div class="text-subtitle-1 font-weight-bold mb-2">
                      {{ event.projectName }}
                    </div>
                    <div class="text-body-2">
                      <div>
                        Priority: {{ event.details?.priority || "N/A" }}
                      </div>
                      <div>
                        Customer: {{ event.details?.customer || "N/A" }}
                      </div>
                      <div>
                        Start: {{ formatDate(event.details.startDate) }}
                      </div>
                      <div>End: {{ formatDate(event.details.endDate) }}</div>
                    </div>
                  </v-card>
                </v-tooltip>
              </div>
            </template>
          </v-calendar>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { fetchCalendarProjects } from "@/endpoints/projectEndpoint.js";
import { format } from "date-fns";

const selectedDate = ref([new Date().toISOString().substr(0, 10)]);
const events = ref([]);
const calendar = ref(null);
const loading = ref(false);
const error = ref(null);
const calendarType = ref("month");

const getPriorityColor = (priority) => {
  const colors = {
    Low: "blue",
    Medium: "orange",
    High: "red",
    Critical: "deep-purple-darken-2",
  };
  return colors[priority] || "grey";
};

const formatDate = (date) => {
  return format(new Date(date), "MMM dd, yyyy");
};

const fetchProjectEvents = async () => {
  loading.value = true;
  error.value = null;

  try {
    const calendarEvents = await fetchCalendarProjects();
    events.value = calendarEvents;
  } catch (err) {
    error.value = "Failed to load project timeline. Please try again later.";
    console.error("Error fetching project events:", err);
    events.value = [];
  } finally {
    loading.value = false;
  }
};

const showEvent = (event) => {
  // You could emit this event to handle project details view
  console.log("Event clicked:", event);
};

onMounted(async () => {
  await fetchProjectEvents();
});
</script>

<style scoped>
.v-event {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  border-radius: 4px;
  padding: 2px 4px;
  font-size: 12px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
}

.v-event:hover {
  opacity: 0.9;
  transform: translateY(-1px);
}

:deep(.v-calendar-weekly__day--current) {
  background-color: rgb(var(--v-theme-primary), 0.1) !important;
  border: 1px solid rgb(var(--v-theme-primary)) !important;
}

:deep(.v-calendar-weekly__day--current .v-calendar-weekly__day-label) {
  background-color: rgb(var(--v-theme-primary));
  color: white;
  border-radius: 50%;
  width: 32px;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
