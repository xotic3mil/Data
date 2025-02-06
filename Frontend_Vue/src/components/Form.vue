<template>
  <form @submit.prevent="submit">
    <template v-for="field in props.fields" :key="field.name">
      <v-text-field
        v-if="field.type === 'text'"
        v-model="formFields[field.name].value.value"
        :error-messages="formFields[field.name].errorMessage.value"
        :label="field.label"
      />
      <v-select
        v-else-if="field.type === 'select'"
        v-model="formFields[field.name].value.value"
        :error-messages="formFields[field.name].errorMessage.value"
        :items="props.roles"
        :label="field.label"
        item-title="roleName"
        item-value="id"
      />
    </template>

    <v-btn class="me-4" type="submit" :loading="loading">Submit</v-btn>
    <v-btn @click="handleReset">Clear</v-btn>
  </form>

  <v-snackbar v-model="snackbar.show" :color="snackbar.color" :timeout="3000">
    {{ snackbar.message }}
    <template v-slot:actions>
      <v-btn variant="text" @click="snackbar.show = false"> Close </v-btn>
    </template>
  </v-snackbar>
</template>

<script setup>
import { ref, reactive } from "vue";
import { useField, useForm } from "vee-validate";

const emit = defineEmits(["employee-created", "refresh-data"]);
const props = defineProps({
  fields: {
    type: Array,
    required: true,
  },
  roles: {
    type: Array,
    required: true,
  },
  apiEndpoint: {
    type: String,
    required: true,
  },
  onRefresh: {
    type: Function,
    required: true,
  },
});

const snackbar = reactive({
  show: false,
  message: "",
  color: "success",
});

const loading = ref(false);

const validationSchema = props.fields.reduce((acc, field) => {
  acc[field.name] = field.validation;
  return acc;
}, {});

const { handleSubmit, handleReset } = useForm({ validationSchema });

const formFields = props.fields.reduce((acc, field) => {
  acc[field.name] = useField(field.name);
  return acc;
}, {});

const submit = handleSubmit(async (values) => {
  try {
    loading.value = true;
    const response = await fetch(props.apiEndpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        ...values,
        roleId: values.roleId,
      }),
    });

    if (!response.ok) {
      if (response.status === 409) {
        throw new Error("Email is already registered");
      }
      throw new Error("Failed to submit form");
    }

    emit("employee-created");
    props.onRefresh();
    // Show success message
    snackbar.color = "success";
    snackbar.message = "Success!";
    snackbar.show = true;
    handleReset();
  } catch (error) {
    // Show error message
    snackbar.color = "error";
    snackbar.message = error.message;
    snackbar.show = true;
  } finally {
    loading.value = false;
  }
});
</script>
