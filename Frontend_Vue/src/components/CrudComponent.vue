<script setup>
// filepath: /c:/Users/Emil3/Documents/EC Utbildning/vuetifty/src/components/CrudComponent.vue
import { ref, defineProps, defineEmits } from "vue";

const props = defineProps({
  api: { type: String, required: true },
  items: { type: Array, default: () => [] },
  title: { type: String, default: "CRUD" },
  formFields: { type: Array, default: () => [] },
  save: { type: Function, required: true },
  deleteItemConfirm: { type: Function, required: true },
});

const emits = defineEmits(["refresh"]);

const isLoading = ref(false);
const fetchDelay = ref(5000);
const internalItems = ref([]);

const snackbar = ref({
  show: false,
  message: "",
  color: "error",
});

// Existing dialog/edit state
const internalDialog = ref(false);
const internalDialogDelete = ref(false);
const internalEditedItem = ref({});
const internalEditedIndex = ref(-1);

// NEW: Use the passed defaultItem if provided, otherwise build one from formFields.
const internalDefaultItem = ref(
  (() => {
    const obj = {};
    props.formFields.forEach((field) => {
      obj[field.key] = "";
    });
    obj.id = null;
    return obj;
  })()
);

function handleNewItem() {
  internalEditedIndex.value = -1;
  internalEditedItem.value = { ...internalDefaultItem.value };
  internalDialog.value = true;
}

function handleEdit(item) {
  internalEditedIndex.value = internalItems.value.indexOf(item);
  internalEditedItem.value = { ...item };
  internalDialog.value = true;
}

function handleDelete(item) {
  internalEditedIndex.value = internalItems.value.indexOf(item);
  internalEditedItem.value = { ...item };
  internalDialogDelete.value = true;
}

async function saveItem() {
  try {
    await props.save(internalEditedItem.value);
    internalDialog.value = false;
    fetchItems("refresh");
  } catch (error) {
    console.error("Error in save:", error);
  }
}

async function confirmDelete() {
  try {
    await props.deleteItemConfirm(internalEditedItem.value.id);
    internalDialogDelete.value = false;
    fetchItems("refresh");
  } catch (error) {
    console.error("Error in delete:", error);
  }
}

function closeDialog() {
  internalDialog.value = false;
}

function closeDeleteDialog() {
  internalDialogDelete.value = false;
}
</script>

<template>
  <v-container>
    <template v-if="isLoading">
      <v-skeleton-loader type="table" class="mx-4 my-4"></v-skeleton-loader>
    </template>
    <template v-else>
      <v-data-table
        :headers="props.headers"
        :items="props.items"
        item-value="id"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-card-title>{{ props.title }}</v-card-title>
            <v-spacer></v-spacer>
            <v-btn
              color="blue-lighten-1"
              variant="elevated"
              class="mr-4"
              @click="handleNewItem"
            >
              New {{ props.title }}
            </v-btn>
            <!-- New/Edit Dialog -->
            <v-dialog v-model="internalDialog" max-width="500px">
              <v-card>
                <v-card-title>
                  <span class="text-h5"
                    >{{ internalEditedItem.id ? "Edit" : "New" }}
                    {{ props.title }}</span
                  >
                </v-card-title>
                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col
                        v-for="field in props.formFields"
                        :key="field.key"
                        cols="12"
                        sm="6"
                      >
                        <v-text-field
                          v-if="field.type === 'text' || field.type === 'date'"
                          v-model="internalEditedItem[field.key]"
                          :label="field.label"
                          :type="field.type"
                        />
                        <v-select
                          v-else-if="field.type === 'select'"
                          v-model="internalEditedItem[field.key]"
                          :items="field.items"
                          :item-title="field.itemTitle"
                          :item-value="field.itemValue"
                          :label="field.label"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>
                <v-card-actions>
                  <v-spacer />
                  <v-btn color="error" @click="closeDialog">Cancel</v-btn>
                  <v-btn color="primary" @click="saveItem">Save</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <!-- Delete Confirmation Dialog -->
            <v-dialog v-model="internalDialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-h5">
                  Are you sure you want to delete this {{ props.title }}?
                </v-card-title>
                <v-card-actions>
                  <v-spacer />
                  <v-btn color="error" @click="closeDeleteDialog">Cancel</v-btn>
                  <v-btn color="primary" @click="confirmDelete">Delete</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon color="blue" class="me-2" icon small @click="handleEdit(item)"
            >mdi-pencil</v-icon
          >
          <v-icon color="red" icon small @click="handleDelete(item)"
            >mdi-delete</v-icon
          >
        </template>
      </v-data-table>
    </template>
  </v-container>
</template>
