<script setup>
import { ref, computed, onMounted, watch } from "vue";
import {
  fetchRoles,
  createRole,
  updateRole,
  deleteRole,
} from "@/endpoints/rolesEndpoint.js";

const expanded = ref(false);
const isLoading = ref(true);
const dialog = ref(false);
const dialogDelete = ref(false);
const searchTerm = ref("");
const roles = ref([]);
const editedIndex = ref(-1);
const delay = ref(5000);

const headers = [
  { title: "ID", key: "id" },
  { title: "Role Name", key: "roleName" },
  { title: "Manage", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  roleName: "",
});

const defaultItem = {
  id: null,
  roleName: "",
};

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Role" : "Edit Role";
});

async function loadRoles() {
  try {
    const data = await fetchRoles();
    roles.value = data;
    delay.value = 5000;
    isLoading.value = false;
    snackbar.value.show = false;
  } catch (error) {
    console.error("Error fetching roles:", error);
    snackbar.value = {
      show: true,
      message: `Failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    setTimeout(loadRoles, delay.value);
    delay.value += 5000;
  }
}

function editItem(item) {
  editedIndex.value = roles.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = roles.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
}

async function deleteItemConfirm() {
  try {
    await deleteRole(editedItem.value.id);
    snackbar.value = {
      show: true,
      message: "Role was deleted successfully!",
      color: "success",
    };
    await loadRoles();
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
    if (editedIndex.value > -1) {
      await updateRole(editedItem.value);
      snackbar.value = {
        show: true,
        message: "Role updated successfully!",
        color: "success",
      };
    } else {
      await createRole(editedItem.value);
      snackbar.value = {
        show: true,
        message: "Role created successfully!",
        color: "success",
      };
    }
    await loadRoles();
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

watch(searchTerm, () => {
  loadRoles();
});

onMounted(async () => {
  await loadRoles();
});
</script>

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
          <v-skeleton-loader type="table" class="mx-4 my-4"></v-skeleton-loader>
        </template>
        <template v-else>
          <v-data-table
            v-model:selected="expanded"
            :headers="headers"
            :items="roles"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            item-value="id"
            no-data-text="Please add new role(s)"
          >
            <template v-slot:top>
              <v-toolbar flat>
                <v-toolbar-title>Roles</v-toolbar-title>
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
                      New Role
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
                          <v-col cols="12">
                            <v-text-field
                              v-model="editedItem.roleName"
                              label="Role Name *"
                            ></v-text-field>
                          </v-col>
                        </v-row>
                      </v-container>
                      <small class="text-caption text-medium-emphasis"
                        >* indicates required field</small
                      >
                    </v-card-text>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn color="red-lighten-1" variant="text" @click="close"
                        >Cancel</v-btn
                      >
                      <v-btn color="blue-lighten-1" variant="text" @click="save"
                        >Save</v-btn
                      >
                    </v-card-actions>
                  </v-card>
                </v-dialog>

                <!-- Delete confirmation dialog -->
                <v-dialog v-model="dialogDelete" max-width="250px">
                  <v-card>
                    <v-card-title class="text-h5">Delete Role?</v-card-title>
                    <v-card-text
                      >Are you sure you want to delete this role?</v-card-text
                    >
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
          <v-btn variant="text" @click="snackbar.show = false">Close</v-btn>
        </template>
      </v-snackbar>
    </v-container>
  </div>
</template>
