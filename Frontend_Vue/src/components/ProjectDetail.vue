<!-- filepath: /d:/Github/Data/Frontend_Vue/src/components/ProjectDetails.vue -->
<template>
  <v-card outlined class="elevation-5 rounded-lg">
    <v-card-title>
      <div
        class="d-flex justify-space-between align-center"
        style="width: 100%"
      >
        <span class="headline">{{ project.name }}</span>
        <span class="subtitle-2">Project-ID: {{ project.projectNumber }}</span>
      </div>
    </v-card-title>

    <v-divider></v-divider>

    <v-card-text>
      <v-row>
        <v-col cols="12" md="6">
          <!-- Left side fields -->
          <v-list two-line>
            <v-list-item>
              <v-list-item-title class="font-weight-bold"
                >Dates</v-list-item-title
              >
              <v-list-item-subtitle>
                {{ project.startDate }} - {{ project.endDate }}
              </v-list-item-subtitle>
            </v-list-item>
            <v-list-item>
              <v-list-item-title class="font-weight-bold"
                >Priority</v-list-item-title
              >
              <v-list-item-subtitle>{{
                project.priority
              }}</v-list-item-subtitle>
            </v-list-item>
          </v-list>
        </v-col>

        <v-col cols="12" md="6">
          <!-- Right side fields -->
          <v-list two-line>
            <v-list-item v-if="project.customers">
              <v-list-item-title class="font-weight-bold"
                >Customer</v-list-item-title
              >
              <v-list-item-subtitle>{{
                project.customers.companyName
              }}</v-list-item-subtitle>
            </v-list-item>
            <v-list-item
              v-if="
                project.customers && project.customers.customerContactPerson
              "
            >
              <v-list-item-title class="font-weight-bold"
                >Contact Person</v-list-item-title
              >
              <v-list-item-subtitle>
                {{ project.customers.customerContactPerson.fullName }}
              </v-list-item-subtitle>
            </v-list-item>
          </v-list>
        </v-col>
      </v-row>

      <v-divider></v-divider>

      <!-- Project Manager Section -->
      <v-row class="mt-3">
        <v-col cols="12">
          <v-list>
            <v-list-item>
              <v-list-item-title class="font-weight-bold"
                >Project Manager</v-list-item-title
              >
              <v-list-item-subtitle>
                <v-icon small class="mr-1">mdi-email</v-icon>
                {{ project.employee.email }}
              </v-list-item-subtitle>
              <v-list-item-subtitle>
                <v-icon small class="mr-1">mdi-phone</v-icon>
                {{ project.employee.phone }}
              </v-list-item-subtitle>
            </v-list-item>
          </v-list>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script setup>
import { defineProps } from "vue";

const props = defineProps({
  project: {
    type: Object,
    required: true,
  },
});
</script>
