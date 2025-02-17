//// filepath: /d:/Github/Data/Frontend_Vue/src/pages/Roles.vue
<script setup>
import { ref } from "vue";
import CrudComponent from "@/components/CrudComponent.vue";
import { createRole, updateRole, deleteRole } from "@/services/apiService.js";
import { useRoles } from "@/composables/useRoles.js";

const formFields = [{ label: "Role", key: "roleName", type: "text" }];
const headers = [
  { text: "Id", value: "id" },
  { text: "Role", value: "roleName" },
  { text: "Manage", value: "actions", sortable: false },
];

const snackbar = ref({
  show: false,
  message: "",
  color: "error",
});

const { roles, loading, error, loadRoles } = useRoles();

async function save(item) {
  try {
    if (item.id) {
      // Update existing role
      await updateRole(item);
      snackbar.value = {
        show: true,
        message: "Role updated successfully!",
        color: "success",
      };
    } else {
      // Create new role
      await createRole(item);
      snackbar.value = {
        show: true,
        message: "Role created successfully!",
        color: "success",
      };
    }
    // Reload roles after a change
    loadRoles();
  } catch (err) {
    if (err.message === "Role already exists") {
      snackbar.value = {
        show: true,
        message: "Role already exists",
        color: "error",
      };
      return;
    }
    console.error("Error:", err);
  }
}

async function deleteItemConfirm(id) {
  try {
    await deleteRole(id);
    snackbar.value = {
      show: true,
      message: "Role deleted successfully!",
      color: "success",
    };
    // Reload roles after deletion
    loadRoles();
  } catch (err) {
    console.error("Error:", err);
  }
}
</script>

<template>
  <div
    v-motion
    :initial="{ opacity: 0, y: 100 }"
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
    <div>
      <CrudComponent
        max-width="800px"
        title="Roles"
        :items="roles"
        :formFields="formFields"
        :headers="headers"
        :loading="loading"
        :save="save"
        :deleteItemConfirm="deleteItemConfirm"
        :snackbar="snackbar"
        @edit-item=""
        @delete-item=""
      />
      <div v-if="error" class="error">Error: {{ error.message }}</div>
    </div>
  </div>
</template>
