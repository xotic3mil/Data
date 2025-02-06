<template>
  <v-card class="mx-auto text-center" max-width="1200" dark>
    <v-card-text>
      <v-sheet color="rgba(0, 0, 0, .12)">
        <v-sparkline
          :model-value="value"
          color="rgba(255, 255, 255, 255)"
          height="60"
          padding="20"
        >
          <template v-slot:label="item"> ${{ item.value }} </template>
        </v-sparkline>
      </v-sheet>
    </v-card-text>

    <v-card-text>
      <div class="text-h4 font-weight-thin">Projects Last 12 Months</div>
    </v-card-text>

    <v-divider></v-divider>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    value: [10, 446, 675, 510, 590, 610, 760],
  }),
};
</script>
