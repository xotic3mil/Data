<template>
  <v-container>
    <v-card flat class="elevation">
      <v-data-table
        v-model:selected="selected"
        :headers="headers"
        :items="services"
        :sort-by="[{ key: 'id', order: 'asc' }]"
        item-value="id"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-spacer />

            <!-- New/Edit dialog -->
            <v-dialog v-model="dialog" max-width="500px">
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
                      <v-col cols="12" sm="6">
                        <v-text-field
                          v-model="editedItem.serviceName"
                          label="Service Name *"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6">
                        <v-text-field
                          v-model="editedItem.price"
                          label="Price *"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6">
                        <v-select
                          v-model="editedItem.unitId"
                          :items="units"
                          item-value="id"
                          item-title="unit"
                          label="Unit *"
                        ></v-select>
                      </v-col>
                      <v-col cols="12" sm="6">
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
                  <v-btn color="red-lighten-1" variant="text" @click="close">
                    Cancel
                  </v-btn>
                  <v-btn color="blue-lighten-1" variant="text" @click="save">
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

        <template v-slot:item.actions="{ item }">
          <v-icon
            class="me-2"
            color="blue-lighten-1"
            size="small"
            @click="editItem(item)"
          >
            mdi-pencil
          </v-icon>
          <v-icon size="small" color="red-lighten-1" @click="deleteItem(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
    </v-card>
    <v-snackbar v-model="snackbar.show" :color="snackbar.color" :timeout="2000">
      {{ snackbar.message }}

      <template v-slot:actions>
        <v-btn variant="text" @click="snackbar.show = false"> Close </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script setup>
import { ref, computed, onMounted, watchEffect } from "vue";

const dialog = ref(false);
const dialogDelete = ref(false);
const selected = ref([]);
const services = ref([]);
const units = ref([]);
const currencies = ref([]);
const editedIndex = ref(-1);

const headers = [
  { title: "ID", key: "id" },
  { title: "Service Name", key: "serviceName" },
  { title: "Unit", key: "unit" },
  { title: "Currency", key: "currency" },
  { title: "Price", key: "price" },
  { title: "Actions", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  serviceName: "",
  price: "",
  unitId: null,
  currencyId: null,
});
const defaultItem = {
  id: null,
  serviceName: "",
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
      const unitObj = units.value.find((u) => u.id === service.unitId);
      const currencyObj = currencies.value.find(
        (c) => c.id === service.currencyId
      );
      return {
        ...service,
        unit: unitObj ? unitObj.unit : "", // e.g. "SEK"
        currency: currencyObj ? currencyObj.currency : "", // e.g. "JPY"
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
    if (editedIndex.value > -1) {
      // Update existing
      const response = await fetch("http://192.168.1.6:5000/api/services", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(editedItem.value),
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
      // Create new
      const response = await fetch("http://192.168.1.6:5000/api/services", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(editedItem.value),
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
  await fetchCurrencies();
  await fetchServices();
});

defineExpose({
  fetchServices,
});
</script>
