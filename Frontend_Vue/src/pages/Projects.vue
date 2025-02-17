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
      <v-card flat class="elevation">
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
                                      item-title="fullName"
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
  fetchStatuses,
  fetchCustomerContact,
  fetchEmployees,
  fetchServices,
  fetchCurrencies,
  fetchUnits,
  fetchCustomers,
} from "@/services/apiService";

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
]);

const selectedRoleId = ref(1);
const filteredEmployees = computed(() => {
  return employee.value.filter(
    (emp) => emp.roles && Number(emp.roles.id) === Number(selectedRoleId.value)
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
  price: "",
  unitId: null,
  currencyId: null,
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
    const response = await fetch(
      `http://192.168.1.6:5000/api/projects?search=${searchTerm.value}`
    );
    if (!response.ok) throw new Error("Failed to fetch projects");

    const data = await response.json();
    console.log("Fetched projects:", data);
    projects.value = data.map((proj) => {
      const cust = customer.value.find((c) => c.id === proj.customerId);
      const emp = employee.value.find((e) => e.id === proj.employeeId);
      return {
        ...proj,
        status: status.value.find((s) => s.id === proj.statusId)?.status || "",
        service:
          service.value.find((s) => s.id === proj.serviceId)?.serviceName || "",
        serviceCurrency: service.currency || "",
        serviceUnit: service.unit || "",
        // Keep the entire customer object so we can access nested properties
        customers: cust || {},
        employee: emp || {},
      };
    });
    delay.value = 5000;
    isLoading.value = false;
  } catch (error) {
    console.error("Error fetching projects:", error);
    snackbar.value = {
      show: true,
      message: `failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    // Retry after delay and increase delay for consecutive failures
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
    const response = await fetch(
      `http://192.168.1.6:5000/api/projects/${editedItem.value.id}`,
      {
        method: "DELETE",
      }
    );
    snackbar.value = {
      show: true,
      message: "Project was deleted successfully!",
      color: "success",
    };

    if (!response.ok) throw new Error("Delete failed");

    await fetchProjects();
    closeDelete();
  } catch (error) {
    console.error("Error:", error);
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
      price: newService.value.price,
      unitId: newService.value.unitId,
      currencyId: newService.value.currencyId,
    };

    const response = await fetch("http://192.168.1.6:5000/api/services", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(servicePayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save service ");
    }

    const result = await response.json();
    console.log("service saved:", result);

    // Optionally, show a success message (for example via a snackbar)
    // Reset the form state if needed:
    newService.value = {
      serviceName: "",
      price: "",
      unitId: null,
      currencyId: null,
    };
    newServiceVisible.value = false;
    await fetchServices();
  } catch (error) {
    console.error("Error saving customer:", error);
    // Optionally, show an error message
  }
}

async function saveCustomer() {
  try {
    const customerPayload = {
      companyName: newCustomer.value.companyName,
      customerContactPersonId: newCustomer.value.customerContactPersonId,
    };

    const response = await fetch("http://192.168.1.6:5000/api/customers", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customerPayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save customer ");
    }

    const result = await response.json();
    console.log("Customer saved:", result);

    // Optionally, show a success message (for example via a snackbar)
    // Reset the form state if needed:
    newCustomer.value = {
      companyName: "",
      customerContactPersonId: null,
    };
    newCustomerVisible.value = false;
    await fetchCustomers();
  } catch (error) {
    console.error("Error saving customer:", error);
    // Optionally, show an error message
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

    const response = await fetch("http://192.168.1.6:5000/api/contactperson/", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customerContactPayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save customer contact");
    }

    // Process the response as needed
    const result = await response.json();
    console.log("Customer contact saved:", result);

    // Optionally, show a success message (for example via a snackbar)
    // Reset the form state if needed:
    newCustomerContact.value = {
      firstName: "",
      lastName: "",
      email: "",
      phone: "",
    };
    newCustomerContactVisible.value = false;
    await fetchCustomerContact();
  } catch (error) {
    console.error("Error saving customer contact:", error);
    // Optionally, show an error message
  }
}

async function save() {
  try {
    const payload = {
      id: editedItem.value.id,
      projectNumber: editedItem.value.projectNumber,
      name: editedItem.value.name,
      description: editedItem.value.description,
      startDate: editedItem.value.startDate,
      endDate: editedItem.value.endDate,
      priority: editedItem.value.priority,
      statusId: editedItem.value.statusId,
      serviceId: editedItem.value.serviceId,
      customerId: editedItem.value.customerId,
      employeeId: editedItem.value.employeeId,
      Status:
        status.value.find((s) => s.id === editedItem.value.statusId) || {},
      Service:
        service.value.find((s) => s.id === editedItem.value.serviceId) || {},
      Customers:
        customer.value.find((c) => c.id === editedItem.value.customerId) || {},
      Employee:
        employee.value.find((e) => e.id === editedItem.value.employeeId) || {},
    };

    let response;
    if (editedIndex.value > -1) {
      // Update existing project
      response = await fetch("http://192.168.1.6:5000/api/projects", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Project already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Update failed");
      snackbar.value = {
        show: true,
        message: "Project updated successfully!",
        color: "success",
      };
    } else {
      // Create new project
      response = await fetch("http://192.168.1.6:5000/api/projects", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Project already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Create failed");
      snackbar.value = {
        show: true,
        message: "Project created successfully!",
        color: "success",
      };
    }
    await fetchProjects();
    close();
  } catch (error) {
    console.error("Error:", error);
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
</style>
