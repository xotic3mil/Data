<script setup>
import { ref, onMounted } from "vue";
import CrudComponent from "@/components/CrudComponent.vue";

const api = "https://localhost:7170/api/roles";

// Define the form fields to be used by CrudComponent.
// Note: "id" is created as read-only (disabled) and "roles" is your role name field.
const formFields = [{ label: "Role", key: "roleName", type: "text" }];
const headers = [
  { text: "Id", value: "id" },
  { text: "Role", value: "roleName" },
  { text: "Actions", value: "actions", sortable: false },
];

const snackbar = ref({
  show: false,
  message: "",
  color: "error",
});

// The save and deleteItemConfirm functions remain the same.
async function save(item) {
  try {
    let response;
    if (item.id) {
      // Update existing role
      response = await fetch(api, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
      });
      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Role already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Update failed");
      snackbar.value = {
        show: true,
        message: "Role updated successfully!",
        color: "success",
      };
    } else {
      // Create new role
      response = await fetch(api, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
      });
      if (response.status === 409) {
        snackbar.value = {
          show: true,
          message: "Role already exists",
          color: "error",
        };
        return;
      }
      if (!response.ok) throw new Error("Creation failed");
      snackbar.value = {
        show: true,
        message: "Role created successfully!",
        color: "success",
      };
    }
  } catch (error) {
    console.error("Error:", error);
  }
}

async function deleteItemConfirm(id) {
  try {
    const response = await fetch(`${api}/${id}`, {
      method: "DELETE",
    });
    if (!response.ok) throw new Error("Delete failed");
    snackbar.value = {
      show: true,
      message: "Role deleted successfully!",
      color: "success",
    };
  } catch (error) {
    console.error("Error:", error);
  }
}
</script>

<template>
  <div>
    <CrudComponent
      max-width="800px"
      :api="api"
      title="Roles"
      :formFields="formFields"
      :headers="headers"
      :save="save"
      :deleteItemConfirm="deleteItemConfirm"
      :snackbar="snackbar"
      @edit-item=""
      @delete-item=""
    />
  </div>
</template>
