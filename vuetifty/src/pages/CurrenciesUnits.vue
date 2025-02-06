<template>
  <v-container>
    <CrudComponent
      :headers="headers"
      :items="currencies"
      :title="'Currencies'"
      :form-fields="formFields"
      :dialog="dialog"
      :dialog-delete="dialogDelete"
      :edited-item="editedItem"
      :default-item="defaultItem"
      :save="save"
      :delete-item-confirm="confirmDelete"
    />

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" :timeout="2000">
      {{ snackbar.message }}

      <template v-slot:actions>
        <v-btn variant="text" @click="snackbar.show = false"> Close </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import CrudComponent from "@/components/CrudComponent.vue";

// Reactive references
const dialog = ref(false);
const dialogDelete = ref(false);
const currencies = ref([]);
const editedItem = ref({ id: null, currency: "" });
const defaultItem = { id: null, currency: "" };

const snackbar = ref({
  show: false,
  color: "success",
  message: "",
});

const headers = [
  { title: "Id", key: "id" },
  { title: "Currency", key: "currency" },
  { title: "Actions", key: "actions", sortable: false },
];

const formFields = [{ key: "currency", label: "Currency", type: "text" }];

async function fetchCurrencies() {
  try {
    const response = await fetch("https://localhost:7170/api/currencies");
    if (!response.ok) throw new Error("Failed to fetch currencies");
    const data = await response.json();
    currencies.value = data.map((currency) => ({
      id: currency.id,
      currency: currency.currency,
    }));
  } catch (error) {
    console.error("Error:", error);
  } finally {
  }
}

async function confirmDelete(id) {
  try {
    const response = await fetch(
      `https://localhost:7170/api/currencies/${id}`,
      {
        method: "DELETE",
      }
    );
    if (!response.ok) throw new Error("Delete failed");
    await fetchCurrencies();
    snackbar.value = {
      show: true,
      message: "Currency deleted successfully",
      color: "success",
    };
  } catch (error) {
    console.error("Error in delete:", error);
  }

  dialogDelete.value = false;
  editedItem.value = { ...defaultItem };
}

async function save(item) {
  // Perform fetch call for saving (POST/PUT)
  try {
    const method = item.id ? "PUT" : "POST";
    const response = await fetch("https://localhost:7170/api/currencies", {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(item),
    });
    if (!response.ok) throw new Error("Save failed");
    await fetchCurrencies();
    snackbar.value = {
      show: true,
      message: `Currency ${item.id ? "updated" : "created"} successfully`,
      color: "success",
    };
  } catch (error) {
    console.error("Error in save:", error);
  }
}

onMounted(() => {
  fetchCurrencies();
});
</script>
