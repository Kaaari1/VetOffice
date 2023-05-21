<template>
  <TabMenu :model="items" @tab-change="handleTabChange"></TabMenu>
</template>

<script>
import Button from "primevue/button";
import store from "../store/index";
import TabMenu from "primevue/tabmenu";

export default {
  data() {
    return {
      items: [
        {
          label: "Logout",
          to: "/login",
        },
        {
          label: "Your Pets",
          to: "/yourPets",
        },
        {
          label: "Your appointments",
          to: "/yourAppointments",
        },
      ],
    };
  },
  components: {
    Button,
    TabMenu,
  },
  methods: {
    async logout() {
      store.dispatch("logout");
    },

    handleTabChange(event) {
      if ((event.index = 0)) {
        this.logout();
      }
    },
  },
};
</script>

<style scoped></style>
