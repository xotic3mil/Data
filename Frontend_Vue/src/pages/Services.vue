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
        <v-data-table
          v-model:selected="expanded"
          :headers="headers"
          :items="services"
          :sort-by="[{ key: 'id', order: 'asc' }]"
          item-value="id"
          show-expand
          no-data-text="Please add new service(s)."
        >
          <template v-slot:item.serviceDescription="{ item }">
            {{
              item.serviceDescription.length > 50
                ? item.serviceDescription.substring(0, 50) + "..."
                : item.serviceDescription
            }}
          </template>

          <template v-slot:item.employee="{ item }">
            {{ item.employee.firstName }} {{ item.employee.lastName }}
          </template>

          <template v-slot:top>
            <v-toolbar flat>
              <v-toolbar-title>Services</v-toolbar-title>
              <v-spacer />

              <!-- New/Edit dialog -->
              <v-dialog v-model="dialog" max-width="700px">
                <template v-slot:activator="{ props }">
                  <v-btn
                    color="blue-lighten-1"
                    v-bind="props"
                    variant="elevated"
                    class="mr-4"
                  >
                    New Service
                  </v-btn>
                </template>

                <v-card>
                  <v-card-title>
                    <span class="text-h5 d-flex justify-center">{{
                      formTitle
                    }}</span>
                  </v-card-title>

                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12" sm="12">
                          <v-text-field
                            v-model="editedItem.serviceName"
                            label="Service Name *"
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="12">
                          <v-textarea
                            v-model="editedItem.serviceDescription"
                            label="Service Description *"
                          ></v-textarea>
                        </v-col>
                        <v-col cols="12" sm="12">
                          <v-select
                            v-model="editedItem.employeeId"
                            :items="filteredEmployees"
                            item-value="id"
                            item-title="fullName"
                            label="Product Manager *"
                            required
                            no-data-text="No Product Managers available"
                          ></v-select>
                        </v-col>
                        <v-col cols="12" sm="12">
                          <v-text-field
                            v-model="editedItem.startupPrice"
                            label="Start-Up Fee *"
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-text-field
                            v-model="editedItem.price"
                            label="Monthly Cost *"
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="3">
                          <v-select
                            v-model="editedItem.unitId"
                            :items="units"
                            item-value="id"
                            item-title="unit"
                            label="Unit *"
                          ></v-select>
                        </v-col>
                        <v-col cols="12" sm="3">
                          <v-select
                            v-model="editedItem.currencyId"
                            :items="currencies"
                            item-value="id"
                            item-title="currency"
                            label="Currency *"
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
                    Are you sure you want to delete this service?
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                      color="red-lighten-1"
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

          <template v-slot:expanded-row="{ columns, item }">
            <tr>
              <td :colspan="columns.length" class="pa-10">
                <v-card outlined class="elevation-5 rounded-lg">
                  <v-card-title>
                    <div
                      class="d-flex justify-space-between align-center"
                      style="width: 100%"
                    >
                      <span class="headline">{{ item.serviceName }}</span>
                    </div>
                  </v-card-title>

                  <v-divider></v-divider>

                  <v-card-text>
                    <v-container>
                      <!-- Project Details Section -->
                      <v-row>
                        <v-col cols="12" md="12">
                          <v-list>
                            <v-list-item>
                              <v-list-item-action> </v-list-item-action>
                            </v-list-item>
                            <v-list-item>
                              <v-list-item-title class="font-weight-bold"
                                >Description
                              </v-list-item-title>
                              <div
                                class="full-text"
                                v-html="
                                  renderDescription(item.serviceDescription)
                                "
                              ></div>
                            </v-list-item>
                          </v-list>
                        </v-col>

                        <v-col cols="12" md="6">
                          <v-list two-line> </v-list>
                        </v-col>
                      </v-row>

                      <v-divider></v-divider>
                    </v-container>
                  </v-card-text>
                </v-card>
              </td>
            </tr>
          </template>

          <template v-slot:item.actions="{ item }">
            <v-btn
              class="me-2"
              color="blue-darken-1"
              size="x-small"
              @click="editItem(item)"
            >
              Edit
            </v-btn>
            <v-btn
              size="x-small"
              color="red-darken-1"
              @click="deleteItem(item)"
            >
              Delete
            </v-btn>
          </template>
        </v-data-table>
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
import { renderDescription } from "@/services/markdownService.js";
import { fetchEmployees } from "@/services/apiService.js";

const expanded = ref(false);
const dialog = ref(false);
const dialogDelete = ref(false);
const services = ref([]);
const units = ref([]);
const employees = ref([]);
const currencies = ref([]);
const editedIndex = ref(-1);

const selectedRoleId = ref(11);
const filteredEmployees = computed(() => {
  return employees.value.filter(
    (emp) => emp.roles && Number(emp.roles.id) === Number(selectedRoleId.value)
  );
});

const headers = [
  { title: "ID", key: "id" },
  { title: "Service Name", key: "serviceName" },
  { title: "Description", key: "serviceDescription" },
  { title: "Product Manager", key: "employee" },
  { title: "Start Up Fee", key: "formattedStartupPrice" },
  { title: "Price", key: "formattedPrice" },
  { title: "Unit", key: "unit" },
  { title: "Currency", key: "currency" },
  { title: "Manage", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  serviceName: "",
  serviceDescription: "",
  employeeId: null,
  price: "",
  unitId: null,
  currencyId: null,
});

const defaultItem = {
  id: null,
  serviceName: "",
  serviceDescription: "",
  employeeId: null,
  price: "",
  unitId: null,
  currencyId: null,
};

const formTitle = computed(() =>
  editedItem.value.id ? "Edit Service" : "New Service"
);

async function fetchServices() {
  try {
    const response = await fetch(`http://192.168.1.6:5000/api/services?`);
    if (!response.ok) throw new Error("Failed to fetch services");
    const rawServices = await response.json();
    // Map each service object to include the actual unit and currency values
    services.value = rawServices.map((service) => {
      const emp = employees.value.find((e) => e.id === service.employeeId);
      const unitObj = units.value.find((u) => u.id === service.unitId);
      const currencyObj = currencies.value.find(
        (c) => c.id === service.currencyId
      );
      return {
        ...service,
        unit: unitObj ? unitObj.unit : "",
        employees: emp || {},
        currency: currencyObj ? currencyObj.currency : "",

        formattedPrice: new Intl.NumberFormat("sv-SE", {
          style: "currency",
          currency: "SEK",
          minimumFractionDigits: 2,
          maximumFractionDigits: 2,
        }).format(service.price),
        formattedStartupPrice: new Intl.NumberFormat("sv-SE", {
          style: "currency",
          currency: "SEK",
          minimumFractionDigits: 2,
          maximumFractionDigits: 2,
        }).format(service.startupPrice),
      };
    });
  } catch (error) {
    console.error("Error fetching services:", error);
  }
}

async function fetchUnits() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/units");
    if (!response.ok) throw new Error("Failed to fetch units");
    units.value = await response.json();
  } catch (error) {
    console.error(error);
  }
}

async function fetchCurrencies() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/currencies");
    if (!response.ok) throw new Error("Failed to fetch currencies");
    currencies.value = await response.json();
  } catch (error) {
    console.error(error);
  }
}

function editItem(item) {
  editedIndex.value = services.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = services.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
}

async function deleteItemConfirm() {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/services/${editedItem.value.id}`,
      {
        method: "DELETE",
      }
    );
    snackbar.value = {
      show: true,
      message: "Service was deleted successfully!",
      color: "success",
    };

    if (!response.ok) throw new Error("Delete failed");

    await fetchServices();
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

async function save() {
  try {
    const payload = {
      ...editedItem.value,
      startupPrice: Number(editedItem.value.startupPrice),
      price: Number(editedItem.value.price),
      unitId: Number(editedItem.value.unitId),
      currencyId: Number(editedItem.value.currencyId),
      employeeId: Number(editedItem.value.employeeId),
    };

    // Look up nested objects
    const unitObj = units.value.find((u) => u.id === payload.unitId);
    const currencyObj = currencies.value.find(
      (c) => c.id === payload.currencyId
    );
    const employeeObj = employees.value.find(
      (e) => e.id === payload.employeeId
    );
    payload.units = unitObj ? { ...unitObj } : null;
    payload.currencies = currencyObj ? { ...currencyObj } : null;
    payload.employee = employeeObj ? { ...employeeObj } : null;

    if (editedIndex.value > -1) {

      const response = await fetch("http://192.168.1.6:5000/api/services", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Service already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Update failed");

      snackbar.value = {
        show: true,
        message: "Service updated successfully!",
        color: "success",
      };
    } else {
      // Create a new service
      const response = await fetch("http://192.168.1.6:5000/api/services", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Service already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Create failed");

      snackbar.value = {
        show: true,
        message: "Service created successfully!",
        color: "success",
      };
    }
    await fetchServices();
    close();
  } catch (error) {
    console.error("Error:", error);
  }
}

watchEffect(() => {
  fetchServices();
});

onMounted(async () => {
  await fetchUnits();
  employees.value = await fetchEmployees();
  await fetchCurrencies();
  await fetchServices();
});

defineExpose({
  fetchServices,
});
</script>
