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
            v-model:selected="expanded"
            :headers="headers"
            :items="employees"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            item-value="id"
            show-expand
          >
            <template v-slot:expanded-row="{ columns, item }">
              <tr>
                <td :colspan="columns.length" class="pa-5">
                  More info about {{ item.firstName }} Lorem ipsum dolor sit,
                  amet consectetur adipisicing elit. Temporibus illum ea et
                  officia, repudiandae mollitia! Nesciunt veritatis
                  necessitatibus explicabo. Autem repellat expedita atque
                  blanditiis minus culpa iure commodi omnis molestias! Lorem
                  ipsum dolor sit, amet consectetur adipisicing elit. Temporibus
                  illum ea et officia, repudiandae mollitia! Nesciunt veritatis
                  necessitatibus explicabo. Autem repellat expedita atque
                  blanditiis minus culpa iure commodi omnis molestias!
                </td>
              </tr>
            </template>
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

const expanded = ref(false);
const isLoading = ref(true);
const dialog = ref(false);
const dialogDelete = ref(false);
const searchTerm = ref("");
const roles = ref([]);
const employees = ref([]);
const editedIndex = ref(-1);
const delay = ref(5000);

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

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Employee" : "Edit Employee";
});

async function fetchEmployees() {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/employees?search=${searchTerm.value}`
    );
    if (!response.ok) throw new Error("Failed to fetch employees");

    const data = await response.json();
    employees.value = data.map((emp) => ({
      ...emp,
      roleName: emp.roles ? emp.roles.roleName : "Unknown",
    }));

    // Reset delay and hide the snackbar on success
    delay.value = 5000;
    isLoading.value = false;
    snackbar.value.show = false;
  } catch (error) {
    console.error("Error fetching employees:", error);
    snackbar.value = {
      show: true,
      message: `failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    // Retry after delay and increase delay for consecutive failures
    setTimeout(fetchEmployees, delay.value);
    delay.value += 5000;
  }
}

async function fetchRoles() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/roles");
    if (!response.ok) throw new Error("Failed to fetch roles");
    const data = await response.json();
    roles.value = data;
  } catch (error) {
    console.error("Error fetching roles:", error);
  }
}

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

async function deleteItemConfirm() {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/employees/${editedItem.value.id}`,
      {
        method: "DELETE",
      }
    );
    snackbar.value = {
      show: true,
      message: "Employee was deleted successfully!",
      color: "success",
    };

    if (!response.ok) throw new Error("Delete failed");

    await fetchEmployees();
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

async function save(item) {
  try {
    if (item.contractStartDate === "") {
      item.contractStartDate = null;
    }
    if (editedIndex.value > -1) {
      // Update existing
      const response = await fetch("http://192.168.1.6:5000/api/employees", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(editedItem.value),
      });

      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Email already exists",
          color: "error",
        };
        return;
      }

      if (!response.ok) throw new Error("Update failed");
      snackbar.value = {
        show: true,
        message: "Employee updated successfully!",
        color: "success",
      };
    } else {
      // Create new
      const response = await fetch("http://192.168.1.6:5000/api/employees", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(editedItem.value),
      });

      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Email already exists",
          color: "error",
        };
        return;
      }

      if (!response.ok) throw new Error("Create failed");

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
  }
}

watch(searchTerm, () => {
  fetchEmployees();
});

onMounted(async () => {
  await fetchRoles();
  await fetchEmployees();
});
</script>
