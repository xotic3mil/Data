<template>
  <v-app-bar color="biru" dark>
    <v-app-bar-nav-icon
      variant="text"
      @click.stop="drawer = !drawer"
    ></v-app-bar-nav-icon>

    <v-toolbar-title>ProcrastiPlan</v-toolbar-title>

    <v-spacer></v-spacer>

    <template v-if="$vuetify.display.mdAndUp">
      <v-btn icon="mdi-magnify" variant="text"></v-btn>

      <v-btn icon="mdi-filter" variant="text"></v-btn>
    </template>

    <v-btn icon="mdi-dots-vertical" variant="text"></v-btn>
  </v-app-bar>

  <v-navigation-drawer
    v-model="drawer"
    :location="$vuetify.display.mobile ? 'bottom' : undefined"
    temporary
    :width="230"
  >
    <v-list-item
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
