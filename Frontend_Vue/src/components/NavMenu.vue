<template>
  <v-app-bar color="biru" dark>
    <v-app-bar-nav-icon
      variant="tonal"
      @click.stop="drawer = !drawer"
    ></v-app-bar-nav-icon>

    <v-toolbar-title>ProcrastiPlan</v-toolbar-title>
  </v-app-bar>

  <v-navigation-drawer
    v-model="drawer"
    :location="$vuetify.display.mobile ? 'bottom' : undefined"
    temporary
    :width="250"
  >
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
    <v-list-item
      prepend-icon="mdi-currency-usd"
      link
      title="Currencies & Units"
      to="/CurrenciesUnits"
    ></v-list-item>
  </v-navigation-drawer>
</template>

<script>
export default {
  data: () => ({
    drawer: false,
    group: null,
  }),

  watch: {
    group() {
      this.drawer = false;
    },
  },
};
</script>
