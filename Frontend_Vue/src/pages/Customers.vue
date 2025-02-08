<script setup>
import { ref, onMounted } from "vue";
import CrudComponent from "@/components/CrudComponent.vue";

const api = "https://localhost:7170/api/customers";

// Fetch and transform customers
const customers = ref([]);
async function fetchCustomers() {
  try {
    const response = await fetch(api);
    if (!response.ok) throw new Error("Fetch failed");
    let data = await response.json();
    data = data.map((customer) => ({
      Comapny: customer.companyName,
      "First Name": customer.customerContactPerson
        ? `${customer.customerContactPerson.firstName} `
        : "",
      "Last Name": customer.customerContactPerson
        ? `${customer.customerContactPerson.lastName} `
        : "",
      Email: customer.customerContactPerson.email
        ? `${customer.customerContactPerson.email} `
        : "",
      Phone: customer.customerContactPerson
        ? `${customer.customerContactPerson.phone} `
        : "",
    }));
    customers.value = data;
  } catch (error) {
    console.error("Error fetching customers:", error);
  }
}

onMounted(fetchCustomers);

async function save(item) {
  try {
    let response;
    if (item.id) {
      // update existing customer
      response = await fetch(api, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
      });
      if (response.status === 409) return;
    } else {
      // create new customer
      response = await fetch(api, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
      });
      if (response.status === 409) return;
    }
    fetchCustomers(); // refresh after save
  } catch (error) {
    console.error("Error:", error);
  }
}

async function deleteItemConfirm(id) {
  try {
    const response = await fetch(`${api}/${id}`, { method: "DELETE" });
    if (!response.ok) throw new Error("Delete failed");
    fetchCustomers(); // refresh after deletion
  } catch (error) {
    console.error("Error:", error);
  }
}
</script>

<template>
  <div>
    <CrudComponent
      max-width="80%"
      :api="api"
      title="Roles"
      :formFields="[
        { text: 'Company Name', key: 'companyName', type: 'text' },
        { text: 'Contact Person', key: 'customerContactPerson', type: 'text' },
        { text: 'Actions', value: 'actions', sortable: false },
      ]"
      :headers="[
        { text: 'Id', value: 'id' },
        { text: 'Company Name', value: 'companyName' },
        { text: 'Contact Person', value: 'contactPersonName', sortable: false },
        { text: 'Actions', value: 'actions', sortable: false },
      ]"
      :save="save"
      :deleteItemConfirm="deleteItemConfirm"
      :snackbar="{}"
      :items="customers"
      @refresh="fetchCustomers"
    />
  </div>
</template>
