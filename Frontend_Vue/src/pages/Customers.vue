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
          <!-- Skeleton loader for table data -->
          <v-skeleton-loader type="table" class="mx-4 my-4"></v-skeleton-loader>
        </template>
        <template v-else>
          <v-data-table
            v-model:selected="expanded"
            :headers="headers"
            :items="customers"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            item-value="id"
            no-data-text="Please add new customer(s)"
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
                <v-toolbar-title>Customers</v-toolbar-title>
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
                <v-dialog v-model="dialog" max-width="700px">
                  <template v-slot:activator="{ props }">
                    <v-btn
                      color="blue-lighten-1"
                      v-bind="props"
                      variant="elevated"
                      class="mr-4"
                    >
                      New Customer
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
                          <v-col cols="12" sm="12">
                            <v-text-field
                              v-model="editedItem.companyName"
                              label="Customer *"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="12">
                            <v-select
                              v-model="editedItem.customerContactPersonId"
                              :items="contactperson"
                              item-value="id"
                              item-title="fullName"
                              label="Customer Contact *"
                              required
                            ></v-select>
                            <v-btn
                              size="small"
                              prepend-icon="mdi-plus"
                              variant="text"
                              text
                              color="blue-darken-1"
                              @click="
                                newCustomerContactVisible =
                                  !newCustomerContactVisible
                              "
                            >
                              Contact Person
                            </v-btn>
                          </v-col>

                          <v-col
                            cols="12"
                            sm="12"
                            v-if="newCustomerContactVisible"
                          >
                            <v-card class="elevation-10 pa-5">
                              <v-form ref="newCustomerContactForm">
                                <v-row>
                                  <v-col cols="12">
                                    <title
                                      class="d-flex align-center justify-center mb-3"
                                    >
                                      Customer Contact
                                    </title>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.firstName"
                                      label="Contact First Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-text"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.lastName"
                                      label="Contact Last Name *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-email"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.email"
                                      label="Contact E-mail *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                  <v-col cols="12" sm="6">
                                    <v-text-field
                                      append-inner-icon="mdi-phone"
                                      variant="solo-filled"
                                      v-model="newCustomerContact.phone"
                                      label="Contact Phone *"
                                      required
                                    ></v-text-field>
                                  </v-col>
                                </v-row>
                              </v-form>

                              <v-btn
                                size="small"
                                prepend-icon="mdi-close"
                                color="red-darken-1"
                                variant="text"
                                @click="
                                  newCustomerContactVisible =
                                    !newCustomerContactVisible
                                "
                              >
                                Cancel
                              </v-btn>
                              <v-btn
                                size="small"
                                prepend-icon="mdi-check"
                                class="ml-2"
                                color="success"
                                variant="text"
                                @click="saveCustomerContact"
                              >
                                Save
                              </v-btn>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                      <small class="text-caption text-medium-emphasis ml-7"
                        >* indicates required field</small
                      >
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
                      Are you sure you want to delete this Customer?
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
const contactperson = ref([]);
const customers = ref([]);
const editedIndex = ref(-1);
const delay = ref(5000);
const newCustomerContactVisible = ref(false);

const newCustomerContact = ref({
  firstName: "",
  lastName: "",
  email: "",
  phone: "",
});

const headers = [
  { title: "ID", key: "id" },
  { title: "Customer", key: "companyName" },
  { title: "Contact Person", key: "customerContactPersonName" },
  { title: "Manage", key: "actions", sortable: false },
];

const editedItem = ref({
  id: null,
  Customer: "",
  customerContactPersonId: null,
});

const defaultItem = {
  id: null,
  Customer: "",
  customerContactPersonId: null,
};

const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Customer" : "Edit Customer";
});

async function fetchCustomers() {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/customers?search=${searchTerm.value}`
    );
    if (!response.ok) throw new Error("Failed to fetch customers");
    const data = await response.json();
    customers.value = data.map((cust) => ({
      ...cust,
      customerContactPersonName: cust.customerContactPerson
        ? `${cust.customerContactPerson.firstName} ${cust.customerContactPerson.lastName}`
        : "Unknown",
    }));

    // Reset delay and hide the snackbar on success
    delay.value = 5000;
    isLoading.value = false;
    snackbar.value.show = false;
  } catch (error) {
    console.error("Error fetching customers:", error);
    snackbar.value = {
      show: true,
      message: `failed connection to system - Retrying in ${
        delay.value / 1000
      } seconds...`,
      color: "error",
    };
    // Retry after delay and increase delay for consecutive failures
    setTimeout(fetchCustomers, delay.value);
    delay.value += 5000;
  }
}

async function fetchCustomerContact() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/contactperson");
    if (!response.ok) throw new Error("Failed to fetch contactperson");
    const data = await response.json();
    contactperson.value = data.map((person) => ({
      ...person,
      fullName: `${person.firstName} ${person.lastName}`,
    }));
  } catch (error) {
    console.error("Error fetching contactperson:", error);
  }
}

async function saveCustomerContact() {
  try {
    const customerContactPayload = {
      firstName: newCustomerContact.value.firstName,
      lastName: newCustomerContact.value.lastName,
      email: newCustomerContact.value.email,
      phone: newCustomerContact.value.phone,
    };

    const response = await fetch("http://192.168.1.6:5000/api/contactperson/", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(customerContactPayload),
    });

    if (!response.ok) {
      throw new Error("Failed to save customer contact");
    }

    // Process the response as needed
    const result = await response.json();
    console.log("Customer contact saved:", result);

    // Optionally, show a success message (for example via a snackbar)
    // Reset the form state if needed:
    newCustomerContact.value = {
      firstName: "",
      lastName: "",
      email: "",
      phone: "",
    };
    newCustomerContactVisible.value = false;
    await fetchCustomerContact();
    await fetchCustomers();
  } catch (error) {
    console.error("Error saving customer contact:", error);
    // Optionally, show an error message
  }
}

function editItem(item) {
  editedIndex.value = customers.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
}

function deleteItem(item) {
  editedIndex.value = customers.value.indexOf(item);
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
}

async function deleteItemConfirm() {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/customers/${editedItem.value.id}`,
      {
        method: "DELETE",
      }
    );
    snackbar.value = {
      show: true,
      message: "Customer was deleted successfully!",
      color: "success",
    };

    if (!response.ok) throw new Error("Delete failed");

    await fetchCustomers();
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
      const response = await fetch("http://192.168.1.6:5000/api/customers", {
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
        message: "Customer updated successfully!",
        color: "success",
      };
    } else {
      // Create new
      const response = await fetch("http://192.168.1.6:5000/api/customers", {
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
        message: "Customer created successfully!",
        color: "success",
      };
    }
    await fetchCustomers();
    close();
  } catch (error) {
    console.error("Error:", error);
  }
}

watch(searchTerm, () => {
  fetchCustomers();
});

onMounted(async () => {
  await fetchCustomerContact();
  await fetchCustomers();
});
</script>
