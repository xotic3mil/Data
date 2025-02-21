<template>
  <div
    v-motion
    :initial="{
      opacity: 0,
      y: 100,
    }"
    :enter="{
      opacity: 1,
      y: 0,
      transition: {
        type: 'spring',
        stiffness: '100',
        damping: '20',
        mass: 0.5,
        delay: 200,
      },
    }"
  >
    <v-container>
      <v-card flat class="elevation-5 my-10 mx-10">
        <template v-if="isLoading">
          <!-- Skeleton loader for table data -->
          <v-skeleton-loader type="table" class="mx-4 my-4"></v-skeleton-loader>
        </template>

        <template v-else>
          <v-data-table
            density="default"
            :key="projects.length"
            v-model:selected="expanded"
            :headers="headers"
            :items="projects"
            :title="`Projects`"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            item-value="id"
            no-data-text="Please add new project(s)"
            show-expand
          >
            <template v-slot:item.status="{ item }">
              <v-chip :color="getStatusColor(item.status)" dark>
                {{ item.status }}
              </v-chip>
            </template>

            <template v-slot:item.priority="{ item }">
              <v-chip :color="getpriorityColor(item.priority)" dark>
                {{ item.priority }}
              </v-chip>
            </template>

            <template v-slot:item.description="{ item }">
              {{
                item.description.length > 50
                  ? item.description.substring(0, 50) + "..."
                  : item.description
              }}
            </template>

            <template v-slot:item.customers="{ item }">
              {{ item.customers.companyName }}
            </template>

            <template v-slot:item.employee="{ item }">
              {{ item.employee.fullName }}
            </template>

            <template v-slot:expanded-row="{ columns, item }">
              <tr>
                <td :colspan="columns.length" class="pa-10">
                  <v-card outlined class="elevation-5 rounded-lg">
                    <v-card-title>
                      <div
                        class="d-flex justify-space-between align-center"
                        style="width: 100%"
                      >
                        <span class="headline">{{ item.name }}</span>
                        <span class="subtitle-2"
                          >Project-ID: {{ item.projectNumber }}</span
                        >
                      </div>
                    </v-card-title>

                    <v-divider></v-divider>

                    <v-card-text>
                      <v-container>
                        <!-- Project Details Section -->
                        <v-row>
                          <v-col cols="12" md="6">
                            <v-list>
                              <v-list-item>
                                <v-list-item-title>Status</v-list-item-title>

                                <v-list-item-action>
                                  <v-chip
                                    :color="getStatusColor(item.status)"
                                    dark
                                    >{{ item.status }}</v-chip
                                  >
                                </v-list-item-action>
                              </v-list-item>
                              <v-list-item>
                                <v-list-item-title class="font-weight-bold"
                                  >Description
                                </v-list-item-title>
                                <div class="full-text">
                                  {{ item.description }}
                                </div>
                              </v-list-item>
                              <v-list-item>
                                <v-list-item-title class="font-weight-bold"
                                  >Dates</v-list-item-title
                                >
                                <v-list-item-subtitle>
                                  Start Date: {{ item.startDate }}
                                </v-list-item-subtitle>
                                <v-list-item-subtitle>
                                  End Date: {{ item.endDate }}
                                </v-list-item-subtitle>
                              </v-list-item>
                              <v-list-item>
                                <v-list-item-title class="font-weight-bold"
                                  >Priority</v-list-item-title
                                >
                                <v-list-item-subtitle>
                                  <v-chip
                                    :color="getpriorityColor(item.priority)"
                                    dark
                                  >
                                    {{ item.priority }}
                                  </v-chip>
                                </v-list-item-subtitle>
                              </v-list-item>
                            </v-list>
                          </v-col>

                          <v-col cols="12" md="6">
                            <v-list two-line>
                              <v-list-item v-if="item.customers">
                                <v-list-item-title class="font-weight-bold"
                                  >Customer</v-list-item-title
                                >
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1">mdi-domain</v-icon>
                                  {{ item.customers.companyName }}
                                </v-list-item-subtitle>
                              </v-list-item>
                              <v-list-item
                                v-if="
                                  item.customers &&
                                  item.customers.customerContactPerson
                                "
                              >
                                <v-list-item-title class="font-weight-bold"
                                  >Contact Person</v-list-item-title
                                >
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1"
                                    >mdi-account</v-icon
                                  >
                                  {{
                                    item.customers.customerContactPerson
                                      .firstName
                                  }}
                                  {{
                                    item.customers.customerContactPerson
                                      .lastName
                                  }}
                                </v-list-item-subtitle>
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1">mdi-email</v-icon>
                                  {{
                                    item.customers.customerContactPerson.email
                                  }}
                                </v-list-item-subtitle>
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1">mdi-phone</v-icon>
                                  {{
                                    item.customers.customerContactPerson.phone
                                  }}
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
                                  <v-icon small class="mr-1"
                                    >mdi-account</v-icon
                                  >
                                  {{ item.employee.firstName }}
                                  {{ item.employee.lastName }}
                                </v-list-item-subtitle>
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1">mdi-email</v-icon>
                                  {{ item.employee.email }}
                                </v-list-item-subtitle>
                                <v-list-item-subtitle>
                                  <v-icon small class="mr-1">mdi-phone</v-icon>
                                  {{ item.employee.phone }}
                                </v-list-item-subtitle>
                              </v-list-item>
                            </v-list>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card-text>
                  </v-card>
                </td>
              </tr>
            </template>
            <template v-slot:top>
              <v-toolbar flat>
                <v-toolbar-title>Projects</v-toolbar-title>
                <v-spacer />
                <v-text-field
                  v-model="searchTerm"
                  label="Search"
                  prepend-inner-icon="mdi-magnify"
                  variant="solo"
                  density="compact"
                  hide-details
                  class="mr-16"
                ></v-text-field>

                <!-- New/Edit dialog -->
                <v-dialog v-model="dialog" max-width="1000px">
                  <template v-slot:activator="{ props }">
                    <div v-motion-slide-bottom>
                      <v-btn
                        color="blue-lighten-1"
                        v-bind="props"
                        variant="elevated"
                        class="mr-4"
                      >
                        New Project
                      </v-btn>
                    </div>
                  </template>

                  <v-card>
                    <v-card-title>
                      <span class="text-h5 d-flex justify-center mt-5">{{
                        formTitle
                      }}</span>
                    </v-card-title>

                    <v-card-text>
                      <v-container>
                        <v-row>
                          <v-col cols="12" sm="12">
                            <v-text-field
                              append-inner-icon="mdi-text"
                              variant="solo-filled"
                              v-model="editedItem.name"
                              label="Project Name *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="12">
                            <v-textarea
                              append-inner-icon="mdi-text"
                              variant="solo-filled"
                              v-model="editedItem.description"
                              label="Description *"
                            ></v-textarea>
                          </v-col>
                          <v-col cols="12" sm="12">
                            <v-select
                              variant="solo-filled"
                              v-model="editedItem.serviceId"
                              :items="service"
                              item-value="id"
                              item-title="serviceName"
                              label="Service *"
                            ></v-select>
                            <v-btn
                              size="small"
                              prepend-icon="mdi-plus"
                              variant="text"
                              text
                              color="blue-darken-1"
                              @click="newServiceVisible = !newServiceVisible"
                            >
                              Service
                            </v-btn>
                          </v-col>
                          <v-col cols="12" sm="12" v-if="newServiceVisible">
                            <v-card class="elevation-10 pa-5">
                              <v-form ref="newServiceForm">
                                <v-row>
                                  <v-col cols="12">
                                    <title
                                      class="d-flex align-center justify-center mb-3"
                                    >
                                      New Service
                                    </title>
                                  </v-col>
                                  <v-col cols="12" sm="12">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newService.serviceName"
                                      label="Service Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="12">
                                    <v-textarea
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newService.serviceDescription"
                                      label="Service Description *"
                                      required
                                    ></v-textarea>
                                  </v-col>
                                  <v-col cols="12" sm="12">
                                    <v-select
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      :items="filteredProductEmployees"
                                      v-model="newService.employeeId"
                                      item-value="id"
                                      item-title="fullName"
                                      label="Product Manager*"
                                      no-data-text="No Product Managers available"
                                      required
                                    ></v-select>
                                  </v-col>
                                  <v-col cols="12" sm="3">
                                    <v-text-field
                                      append-inner-icon="mdi-currency-usd"
                                      variant="solo-filled"
                                      v-model="newService.startupPrice"
                                      item-value="id"
                                      label="Start Up Fee *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="3">
                                    <v-text-field
                                      append-inner-icon="mdi-currency-usd"
                                      variant="solo-filled"
                                      v-model="newService.price"
                                      item-value="id"
                                      label="Monthly Cost *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="3">
                                    <v-select
                                      variant="solo-filled"
                                      v-model="newService.unitId"
                                      :items="unit"
                                      item-value="id"
                                      item-title="unit"
                                      label="Unit *"
                                      required
                                    ></v-select>
                                  </v-col>
                                  <v-col cols="12" sm="3">
                                    <v-select
                                      variant="solo-filled"
                                      v-model="newService.currencyId"
                                      :items="currency"
                                      item-value="id"
                                      item-title="currency"
                                      label="Currency *"
                                      required
                                    ></v-select>
                                  </v-col>
                                  <!-- add additional fields as needed -->
                                </v-row>
                              </v-form>

                              <v-btn
                                size="small"
                                prepend-icon="mdi-close"
                                color="red-darken-1"
                                variant="text"
                                @click="newServiceVisible = !newServiceVisible"
                              >
                                Cancel
                              </v-btn>
                              <v-btn
                                size="small"
                                prepend-icon="mdi-check"
                                class="ml-2"
                                color="success"
                                variant="text"
                                @click="saveService"
                              >
                                Save
                              </v-btn>
                            </v-card>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              append-inner-icon="mdi-calendar"
                              variant="solo-filled"
                              type="date"
                              v-model="editedItem.startDate"
                              label="Contract Start Date *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              append-inner-icon="mdi-calendar"
                              variant="solo-filled"
                              type="date"
                              v-model="editedItem.endDate"
                              label="Contract End Date *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-select
                              variant="solo-filled"
                              v-model="editedItem.priority"
                              :items="priority"
                              item-title="name"
                              item-value="name"
                              label="Priority *"
                            ></v-select>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-select
                              variant="solo-filled"
                              v-model="editedItem.statusId"
                              :items="status"
                              item-value="id"
                              item-title="status"
                              label="Status *"
                            ></v-select>
                          </v-col>
                          <!--- Customer & New Customer-->
                          <v-col cols="12" sm="12">
                            <v-select
                              variant="solo-filled"
                              v-model="editedItem.customerId"
                              :items="customer"
                              item-value="id"
                              item-title="companyName"
                              label="Customer *"
                            ></v-select>
                            <v-btn
                              size="small"
                              prepend-icon="mdi-plus"
                              variant="text"
                              text
                              color="blue-darken-1"
                              @click="newCustomerVisible = !newCustomerVisible"
                            >
                              Customer
                            </v-btn>

                            <v-btn
                              size="small"
                              prepend-icon="mdi-plus"
                              class="ml-2"
                              variant="text"
                              text
                              color="blue-darken-1"
                              @click="
                                newCustomerContactVisible =
                                  !newCustomerContactVisible
                              "
                            >
                              Customer Contact
                            </v-btn>
                          </v-col>
                          <v-col cols="12" sm="12" v-if="newCustomerVisible">
                            <v-card class="elevation-10 pa-5">
                              <v-form ref="newCustomerForm">
                                <v-row>
                                  <v-col cols="12">
                                    <title
                                      class="d-flex align-center justify-center mb-3"
                                    >
                                      New Customer
                                    </title>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newCustomer.companyName"
                                      label="Customer Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-select
                                      append-inner-icon="mdi-account"
                                      variant="solo-filled"
                                      v-model="
                                        newCustomer.customerContactPersonId
                                      "
                                      :items="customerContactPerson"
                                      item-value="id"
                                      :item-title="
                                        (item) =>
                                          `${item.firstName} ${item.lastName} - ${item.email}`
                                      "
                                      label="Customer Contact *"
                                      required
                                    ></v-select>
                                  </v-col>
                                  <!-- add additional fields as needed -->
                                </v-row>
                              </v-form>

                              <v-btn
                                size="small"
                                prepend-icon="mdi-close"
                                color="red-darken-1"
                                variant="text"
                                @click="
                                  newCustomerVisible = !newCustomerVisible
                                "
                              >
                                Cancel
                              </v-btn>
                              <v-btn
                                size="small"
                                prepend-icon="mdi-check"
                                class="ml-2"
                                color="success"
                                variant="text"
                                @click="saveCustomer"
                              >
                                Save
                              </v-btn>
                            </v-card>
                          </v-col>
                          <!--- Customer & New Customer -->

                          <!-- Customer Contact Person -->
                          <v-col
                            cols="12"
                            sm="12"
                            v-if="newCustomerContactVisible"
                          >
                            <v-card class="elevation-10 pa-5">
                              <v-form ref="newCustomerContactForm">
                                <v-row>
                                  <v-col cols="12">
                                    <title
                                      class="d-flex align-center justify-center mb-3"
                                    >
                                      Customer Contact
                                    </title>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.firstName"
                                      label="Contact First Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.lastName"
                                      label="Contact Last Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-email"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.email"
                                      label="Contact E-mail *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-phone"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.phone"
                                      label="Contact Phone *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                </v-row>
                              </v-form>

                              <v-btn
                                size="small"
                                prepend-icon="mdi-close"
                                color="red-darken-1"
                                variant="text"
                                @click="
                                  newCustomerContactVisible =
                                    !newCustomerContactVisible
                                "
                              >
                                Cancel
                              </v-btn>
                              <v-btn
                                size="small"
                                prepend-icon="mdi-check"
                                class="ml-2"
                                color="success"
                                variant="text"
                                @click="saveCustomerContact"
                              >
                                Save
                              </v-btn>
                            </v-card>
                          </v-col>
                          <!--- Customer Contact Person -->

                          <v-col cols="12" sm="12">
                            <v-select
                              variant="solo-filled"
                              v-model="editedItem.employeeId"
                              :items="filteredEmployees"
                              item-value="id"
                              item-title="fullName"
                              label="Project Manager *"
                            ></v-select>
                          </v-col>
                        </v-row>
                        <small class="text-caption text-medium-emphasis"
                          >* indicates required field</small
                        >
                      </v-container>
                    </v-card-text>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        prepend-icon="mdi-close"
                        color="red-darken-1"
                        variant="text"
                        @click="close"
                      >
                        Cancel
                      </v-btn>
                      <v-btn
                        prepend-icon="mdi-check"
                        color="success"
                        variant="text"
                        @click="save"
                      >
                        Save
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>

                <!-- Delete confirmation dialog -->
                <v-dialog v-model="dialogDelete" max-width="250px">
                  <v-card>
                    <v-card-title class="text-h5">Delete Item?</v-card-title>
                    <v-card-text>
                      Are you sure you want to delete this project?
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="red-darken-1"
                        variant="text"
                        @click="closeDelete"
                        >Cancel</v-btn
                      >
                      <v-btn
                        color="blue-lighten-1"
                        variant="text"
                        @click="deleteItemConfirm"
                        >OK</v-btn
                      >
                      <v-spacer></v-spacer>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-toolbar>
            </template>

            <template v-slot:item.actions="{ item }">
              <v-btn
                class="me-3"
                color="blue-lighten-1"
                size="x-small"
                @click="editItem(item)"
              >
                Edit
              </v-btn>
              <v-btn
                size="x-small"
                color="red-lighten-1"
                @click="deleteItem(item)"
              >
                Delete
              </v-btn>
            </template>
          </v-data-table>
        </template>
      </v-card>
      <v-snackbar
        v-model="snackbar.show"
        :color="snackbar.color"
        :timeout="2000"
      >
        {{ snackbar.message }}

        <template v-slot:actions>
          <v-btn variant="text" @click="snackbar.show = false"> Close </v-btn>
        </template>
      </v-snackbar>
    </v-container>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watchEffect } from "vue";
import {
  fetchProjects as fetchProjectsApi,
  updateProject,
  updateProjectPayload,
  createProject,
  createProjectPayload,
  deleteProject,
} from "@/endpoints/projectEndpoint.js";

import {
  fetchStatuses,
  fetchCustomerContact,
  fetchEmployees,
  fetchServices,
  fetchCurrencies,
  fetchUnits,
  fetchCustomers,
} from "@/endpoints/apiService";

const expanded = ref(false);
const isLoading = ref(true);
const dialog = ref(false);
const dialogDelete = ref(false);
const customerContactPerson = ref([]);
const searchTerm = ref("");
const status = ref([]);
const customer = ref([]);
const unit = ref([]);
const currency = ref([]);
const newCustomerVisible = ref(false);
const newServiceVisible = ref(false);
const newCustomerContactVisible = ref(false);
const service = ref([]);
const employee = ref([]);
const projects = ref([]);
const editedIndex = ref(-1);
const delay = ref(5000);

const priority = ref([
  { id: 1, name: "Low" },
  { id: 2, name: "Medium" },
  { id: 3, name: "High" },
  { id: 4, name: "Critical" },
]);

const selectedRoleId = ref(3);
const filteredEmployees = computed(() => {
  return employee.value.filter(
    (emp) => emp.roles && Number(emp.roles.id) === Number(selectedRoleId.value)
  );
});

const selectedRoleOwnerId = ref(11);
const filteredProductEmployees = computed(() => {
  return employee.value.filter(
    (emp) =>
      emp.roles && Number(emp.roles.id) === Number(selectedRoleOwnerId.value)
  );
});

const headers = [
  { title: "ID", key: "id" },
  { title: "Status", key: "status" },
  { title: "Name", key: "name" },
  { title: "Description", key: "description" },
  { title: "StartDate", key: "startDate" },
  { title: "EndDate", key: "endDate" },
  { title: "Priority", key: "priority" },
  { title: "Service", key: "service" },
  { title: "Customer", key: "customers" },
  { title: "Project Manager", key: "employee" },
  { title: "Manage", key: "actions", sortable: false },
];

const newCustomerContact = ref({
  firstName: "",
  lastName: "",
  email: "",
  phone: "",
});

const newCustomer = ref({
  companyName: "",
  customerContactPersonId: null,
});

const newService = ref({
  serviceName: "",
  serviceDescription: "",
  startupPrice: "",
  price: "",
  unitId: null,
  currencyId: null,
  employeeId: null,
});

const editedItem = ref({
  id: null,
  name: "",
  description: "",
  startDate: null,
  endDate: null,
  priority: "",
  statusId: null,
  serviceId: null,
  customerId: null,
  customerName: "",
  customerContactPersonId: null,
  employeeId: null,
});

const defaultItem = {
  id: null,
  name: "",
  description: "",
  startDate: null,
  endDate: null,
  priority: "",
  statusId: null,
  serviceId: null,
  customerId: null,
  employeeId: null,
};

const statusColorMapping = {
  New: "blue",
  Open: "green",
  "In Progress": "orange",
  Pending: "amber",
  "On Hold": "purple",
  Resolved: "teal",
  Closed: "grey",
  Reopened: "indigo",
  Cancelled: "red",
  Archived: "black",
};

const priorityColorMapping = {
  Low: "green",
  Medium: "orange",
  High: "red",
  Critical: "deep-purple-darken-2",
};

function getpriorityColor(status) {
  return priorityColorMapping[status.trim()] || "default";
}

function getStatusColor(status) {
  return statusColorMapping[status.trim()] || "default";
}

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Projects" : "Edit Project";
});

async function fetchProjects() {
  try {
    projects.value = await fetchProjectsApi(
      searchTerm.value,
      status.value,
      customer.value,
      employee.value,
      service.value
    );
    delay.value = 5000;
    isLoading.value = false;
  } catch (error) {
    console.error("Error fetching projects:", error);
    snackbar.value = {
      show: true,
      message: `Failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    setTimeout(fetchProjects, delay.value);
    delay.value += 5000;
  }
}

function editItem(item) {
  editedIndex.value = projects.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = projects.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
}

async function deleteItemConfirm() {
  try {
    await deleteProject(editedItem.value.id);

    snackbar.value = {
      show: true,
      message: "Project was deleted successfully!",
      color: "error",
    };

    await fetchProjects();
    closeDelete();
  } catch (error) {
    console.error("Error:", error);
    snackbar.value = {
      show: true,
      message: "Delete failed: " + error.message,
      color: "error",
    };
  }
}

function close() {
  dialog.value = false;
  editedItem.value = Object.assign({}, defaultItem);
  editedIndex.value = -1;
}

function closeDelete() {
  dialogDelete.value = false;
  editedItem.value = Object.assign({}, defaultItem);
  editedIndex.value = -1;
}

const snackbar = ref({
  show: false,
  message: "",
  color: "error",
});

async function saveService() {
  try {
    const servicePayload = {
      serviceName: newService.value.serviceName,
      serviceDescription: newService.value.serviceDescription,
      startupPrice: newService.value.startupPrice,
      price: newService.value.price,
      unitId: newService.value.unitId,
      currencyId: newService.value.currencyId,
      employeeId: newService.value.employeeId,
    };

    const response = await fetch("http://192.168.1.6:5000/api/services", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(servicePayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save service");
    }

    const savedService = await response.json();
    console.log("Service saved:", savedService);

    // Add the new service to the local array
    service.value.push(savedService);

    // Show success message
    snackbar.value = {
      show: true,
      message: "Service created successfully!",
      color: "success",
    };

    // Reset the form
    newService.value = {
      serviceName: "",
      serviceDescription: "",
      startupPrice: "",
      price: "",
      unitId: null,
      currencyId: null,
      employeeId: null,
    };
    newServiceVisible.value = false;
  } catch (error) {
    console.error("Error saving service:", error);
    snackbar.value = {
      show: true,
      message: "Failed to save service: " + error.message,
      color: "error",
    };
  }
}

async function saveCustomer() {
  try {
    const customerPayload = {
      companyName: newCustomer.value.companyName,
      customerContactPersonId: Number(
        newCustomer.value.customerContactPersonId
      ),
    };

    const response = await fetch("http://192.168.1.6:5000/api/customers", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customerPayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save customer");
    }

    const savedCustomer = await response.json();
    console.log("Customer saved:", savedCustomer);

    // Add the new customer to the local array
    customer.value.push(savedCustomer);

    // Show success message
    snackbar.value = {
      show: true,
      message: "Customer created successfully!",
      color: "success",
    };

    // Reset the form
    newCustomer.value = {
      companyName: "",
      customerContactPersonId: null,
    };
    newCustomerVisible.value = false;
  } catch (error) {
    console.error("Error saving customer:", error);
    snackbar.value = {
      show: true,
      message: "Failed to save customer: " + error.message,
      color: "error",
    };
  }
}

async function saveCustomerContact() {
  try {
    const customerContactPayload = {
      firstName: newCustomerContact.value.firstName,
      lastName: newCustomerContact.value.lastName,
      email: newCustomerContact.value.email,
      phone: newCustomerContact.value.phone,
    };

    console.log("Sending customer contact payload:", customerContactPayload);

    const response = await fetch("http://192.168.1.6:5000/api/contactperson", {
      // Removed trailing slash
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customerContactPayload),
    });

    const responseText = await response.text();
    console.log("Response Status:", response.status);
    console.log("Response Body:", responseText);

    if (!response.ok) {
      throw new Error(
        `Failed to save customer contact: ${response.status} ${responseText}`
      );
    }

    // Parse the response if it's JSON
    const result = response.headers
      .get("content-type")
      ?.includes("application/json")
      ? JSON.parse(responseText)
      : responseText;

    console.log("Customer contact saved:", result);

    // Add the new contact to the local array
    if (result.id) {
      customerContactPerson.value.push(result);
    }

    // Show success message
    snackbar.value = {
      show: true,
      message: "Customer contact created successfully!",
      color: "success",
    };

    // Reset the form
    newCustomerContact.value = {
      firstName: "",
      lastName: "",
      email: "",
      phone: "",
    };
    newCustomerContactVisible.value = false;

    // Refresh the contact list
    await fetchCustomerContact();
  } catch (error) {
    console.error("Error saving customer contact:", error);
    snackbar.value = {
      show: true,
      message: "Failed to save customer contact: " + error.message,
      color: "error",
    };
  }
}

async function save() {
  try {
    if (editedIndex.value > -1) {
      // Update existing project
      const payload = updateProjectPayload(
        editedItem.value,
        status.value,
        customer.value,
        employee.value,
        service.value
      );
      await updateProject(payload);
    } else {
      // Create new project
      const payload = createProjectPayload(editedItem.value);
      await createProject(payload);
    }
    snackbar.value = {
      show: true,
      message:
        editedIndex.value > -1
          ? "Project updated successfully!"
          : "Project created successfully!",
      color: "success",
    };
    await fetchProjects();
    close();
  } catch (error) {
    console.error("Error:", error);
    snackbar.value = {
      show: true,
      message: "Operation failed: " + error.message,
      color: "error",
    };
  }
}

watchEffect(() => {
  fetchProjects();
});

onMounted(async () => {
  try {
    const statuses = await fetchStatuses();
    const services = await fetchServices();
    const customers = await fetchCustomers();
    const employees = await fetchEmployees();
    const customerContacts = await fetchCustomerContact();
    const units = await fetchUnits();
    const currencies = await fetchCurrencies();

    status.value = statuses;
    service.value = services;
    customer.value = customers;
    employee.value = employees;
    customerContactPerson.value = customerContacts;
    unit.value = units;
    currency.value = currencies;

    await fetchProjects();
  } catch (error) {
    console.error("Error loading initial data:", error);
  }
});

defineExpose({
  fetchProjects,
});
</script>

<style scoped>
::v-deep .full-text {
  white-space: normal;
  overflow: visible;
  text-overflow: unset;
  word-break: break-word;
  max-width: 60%;
}
.stat-card-skeleton {
  background: linear-gradient(90deg, #2c2c2c 25%, #3c3c3c 50%, #2c2c2c 75%);
  background-size: 200% 100%;
  animation: shimmer 1.5s infinite;
}

@keyframes shimmer {
  0% {
    background-position: 200% 0;
  }
  100% {
    background-position: -200% 0;
  }
}
</style>
