<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="currencies"
      item-value="id"
      :sort-by="[{ key: 'id', order: 'asc' }]"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Currencies</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-dialog v-model="dialog" max-width="500px">
            <template v-slot:activator="{ props }">
              <v-btn color="primary" dark class="mb-2" v-bind="props">
                New Currency
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="text-h5">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12" sm="6" md="6">
                      <v-text-field
                        v-model="editedItem.currency"
                        label="Currency Name"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue-darken-1" variant="text" @click="close">
                  Cancel
                </v-btn>
                <v-btn color="blue-darken-1" variant="text" @click="save">
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5"
                >Are you sure you want to delete this item?</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue-darken-1" variant="text" @click="closeDelete"
                  >Cancel</v-btn
                >
                <v-btn
                  color="blue-darken-1"
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
        <v-icon size="small" class="me-2" @click="editItem(item)">
          mdi-pencil
        </v-icon>
        <v-icon size="small" @click="deleteItem(item)"> mdi-delete </v-icon>
      </template>
    </v-data-table>
    <v-spacer class="ma-16" />

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" :timeout="2000">
      {{ snackbar.message }}
      <template v-slot:actions>
        <v-btn variant="text" @click="snackbar.show = false">Close</v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";

const dialog = ref(false);
const dialogDelete = ref(false);
const loading = ref(false);
const currencies = ref([]);
const editedIndex = ref(-1);

const headers = [
  { title: "Id", key: "id" },
  { title: "Currency", key: "currency" },
  { title: "Actions", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  name: "",
});

const defaultItem = {
  id: null,
  name: "",
};

const snackbar = ref({
  show: false,
  color: "success",
  message: "",
});

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Currency" : "Edit Currency";
});

async function fetchCurrencies() {
  try {
    loading.value = true;
    const response = await fetch(`https://localhost:7170/api/currencies`);
    if (!response.ok) throw new Error("Failed to fetch currencies");
    const data = await response.json();
    currencies.value = data.map((currency) => ({
      currency: currency.currency,
      id: currency.id,
    }));
  } catch (error) {
    console.error("Error:", error);
    showSnackbar("Failed to load currencies", "error");
  } finally {
    loading.value = false;
  }
}

function editItem(item) {
  editedIndex.value = currencies.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = currencies.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
}

async function deleteItemConfirm() {
  try {
    const response = await fetch(
      `https://localhost:7170/api/currencies/${editedItem.value.id}`,
      {
        method: "DELETE",
      }
    );
    if (!response.ok) throw new Error("Delete failed");
    await fetchCurrencies();
    showSnackbar("Currency deleted successfully", "success");
  } catch (error) {
    console.error("Error:", error);
    showSnackbar("Failed to delete currency", "error");
  }
  closeDelete();
}

async function save() {
  try {
    const method = editedIndex.value === -1 ? "POST" : "PUT";
    const response = await fetch("https://localhost:7170/api/currencies", {
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(editedItem.value),
    });

    if (!response.ok) {
      if (response.status === 409) {
        showSnackbar("Currency already exists", "error");
        return;
      }
      throw new Error("Save failed");
    }

    await fetchCurrencies();
    showSnackbar(
      `Currency ${
        editedIndex.value === -1 ? "created" : "updated"
      } successfully`,
      "success"
    );
    close();
  } catch (error) {
    console.error("Error:", error);
    showSnackbar("Failed to save currency", "error");
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

function showSnackbar(message, color) {
  snackbar.value = {
    show: true,
    message,
    color,
  };
}

onMounted(async () => {
  await fetchCurrencies();
});
</script>
