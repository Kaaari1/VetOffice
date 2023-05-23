<template>
  <TabMenu
    v-if="isUser"
    :model="userItems"
    @tab-change="handleTabChange"
  ></TabMenu>
  <TabMenu
    v-if="isVet"
    :model="vetItems"
    @tab-change="handleTabChange"
  ></TabMenu>
  <TabMenu
    v-if="isAdmin"
    :model="adminItems"
    @tab-change="handleTabChange"
  ></TabMenu>
</template>

<script>
import store from "../store/index";
import TabMenu from "primevue/tabmenu";

export default {
  data() {
    return {
      userItems: [
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
      vetItems: [
        {
          label: "Logout",
          to: "/login",
        },
        {
          label: "Day offs",
          to: "/dayOffs",
        },
        {
          label: "Appointments",
          to: "/appointments",
        },
      ],
      adminItems: [
        {
          label: "Logout",
          to: "/login",
        },
      ],
      roleModel: [],
      users: [],
      roles: [],
    };
  },
  components: {
    TabMenu,
  },
  methods: {
    async logout() {
      store.dispatch("logout");
    },

    handleTabChange(event) {
      if (event.index == 0) {
        this.logout();
      }
    },
  },
  computed: {
    isAdmin() {
      return localStorage.role === "Admin";
    },
    isVet() {
      return localStorage.role === "Vet";
    },
    isUser() {
      return localStorage.role === "User";
    },
  },
  watch: {
    localStorage: {
      handler(newValue, oldValue) {
        console.log(newValue);
      },
      deep: true,
    },
  },
};
</script>

<style scoped></style>
