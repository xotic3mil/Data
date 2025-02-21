<template>
  <v-app-bar color="blue-grey-lighten-1" dark>
    <v-toolbar-title class="font-weight-thin" style="font-size: xx-large"
      >ProcrastiPlan
    </v-toolbar-title>
    <template v-slot:append> </template>
  </v-app-bar>

  <v-navigation-drawer v-model="drawer" :width="250" floating permanent>
    <v-list nav class="layout">
      <v-list-item
        class="mt-3"
        prepend-icon="mdi-view-dashboard"
        link
        title="Dashboard"
        to="/"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-source-repository-multiple"
        link
        title="Projects"
        to="/Projects"
      ></v-list-item>

      <v-list-item
        prepend-icon="mdi-toolbox-outline"
        link
        title="Services"
        to="/Services"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-account-group"
        link
        title="Customers"
        to="/Customers"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-account-circle-outline"
        link
        title="Employees"
        to="/Employees"
      ></v-list-item>
      <v-list-item
        prepend-icon=" mdi-swap-horizontal-hidden"
        link
        title="Roles"
        to="/Roles"
      ></v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  data: () => ({
    drawer: true,
    group: null,
  }),

  watch: {
    group() {
      // this.drawer = false;
    },
  },
};
</script>

<style scoped>
.v-navigation-drawer {
  z-index: 1006 !important;
}

.v-app-bar {
  z-index: 1005 !important;
}
</style>
