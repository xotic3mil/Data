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
            v-model:selected="expanded"
            :headers="headers"
            :items="employees"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            item-value="id"
            :items-per-page="30"
            no-data-text="Please add new employee(s)"
          >
            <template v-slot:top>
              <v-toolbar flat>
                <v-toolbar-title>Employees</v-toolbar-title>
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
                <v-dialog v-model="dialog" max-width="500px">
                  <template v-slot:activator="{ props }">
                    <v-btn
                      color="blue-lighten-1"
                      v-bind="props"
                      variant="elevated"
                      class="mr-4"
                    >
                      New Employee
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
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.firstName"
                              label="First Name *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.lastName"
                              label="Last Name *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.email"
                              label="Email *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.phone"
                              label="Phone *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="12">
                            <v-text-field
                              type="date"
                              v-model="editedItem.contractStartDate"
                              label="Contract Start Date *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="12">
                            <v-select
                              v-model="editedItem.roleId"
                              :items="roles"
                              item-value="id"
                              item-title="roleName"
                              label="Role *"
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
                        color="red-lighten-1"
                        variant="text"
                        @click="close"
                      >
                        Cancel
                      </v-btn>
                      <v-btn
                        color="blue-lighten-1"
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
                      Are you sure you want to delete this employee?
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
import { ref, computed, onMounted, watch } from "vue";
import { fetchRoles } from "@/endpoints/rolesEndpoint.js";
import {
  fetchEmployees as fetchEmployeesApi,
  createEmployee,
  updateEmployee,
  deleteEmployee,
} from "@/endpoints/employeeEndpoint.js";

const expanded = ref(false);
const isLoading = ref(true);
const dialog = ref(false);
const dialogDelete = ref(false);
const searchTerm = ref("");
const roles = ref([]);
const employees = ref([]);
const editedIndex = ref(-1);
const delay = ref(5000);
const timeout = ref(null);

const headers = [
  { title: "ID", key: "id" },
  { title: "First Name", key: "firstName" },
  { title: "Last Name", key: "lastName" },
  { title: "Email", key: "email" },
  { title: "Phone", key: "phone" },
  { title: "Contract Start Date", key: "contractStartDate" },
  { title: "Role", key: "roles.roleName" },
  { title: "Manage", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  firstName: "",
  lastName: "",
  email: "",
  contractStartDate: null,
  phone: "",
  roleId: null,
});

const defaultItem = {
  id: null,
  firstName: "",
  lastName: "",
  email: "",
  contractStartDate: null,
  phone: "",
  roleId: null,
};

const snackbar = ref({
  show: false,
  message: "",
  color: "error",
});

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Employee" : "Edit Employee";
});

// #region API calls

async function fetchEmployees() {
  try {
    const currentSearch = searchTerm.value; // Store current search term
    employees.value = await fetchEmployeesApi(currentSearch);
    delay.value = 5000;
    isLoading.value = false;
    snackbar.value.show = false;
  } catch (error) {
    console.error("Error fetching employees:", error);
    snackbar.value = {
      show: true,
      message: `Failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    setTimeout(() => fetchEmployees(), delay.value);
    delay.value += 5000;
  }
}

async function save(item) {
  try {
    if (item.contractStartDate === "") {
      item.contractStartDate = null;
    }

    if (editedIndex.value > -1) {
      await updateEmployee(editedItem.value);
      snackbar.value = {
        show: true,
        message: "Employee updated successfully!",
        color: "success",
      };
    } else {
      await createEmployee(editedItem.value);
      snackbar.value = {
        show: true,
        message: "Employee created successfully!",
        color: "success",
      };
    }

    await fetchEmployees();
    close();
  } catch (error) {
    console.error("Error:", error);
    snackbar.value = {
      show: true,
      message: error.message,
      color: "error",
    };
  }
}

async function deleteItemConfirm() {
  try {
    await deleteEmployee(editedItem.value.id);
    snackbar.value = {
      show: true,
      message: "Employee was deleted successfully!",
      color: "success",
    };
    await fetchEmployees();
    closeDelete();
  } catch (error) {
    console.error("Error:", error);
    snackbar.value = {
      show: true,
      message: error.message,
      color: "error",
    };
  }
}

// Replace fetchRoles function
async function loadRoles() {
  try {
    roles.value = await fetchRoles();
  } catch (error) {
    console.error("Error fetching roles:", error);
  }
}

// #endregion

// #region handler functions
function editItem(item) {
  editedIndex.value = employees.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = employees.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
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

// #endregion

watch(searchTerm, () => {
  fetchEmployees();
});

onMounted(async () => {
  try {
    isLoading.value = true;
    await loadRoles();
    if (!searchTerm.value) {
      employees.value = await fetchEmployeesApi("");
    }
  } catch (error) {
    console.error("Error in initial load:", error);
  } finally {
    isLoading.value = false;
  }
});
</script>
