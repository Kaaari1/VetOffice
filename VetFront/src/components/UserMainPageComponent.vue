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
  <DataTable
    v-if="isAdmin"
    :value="data"
    tableStyle="min-width: 50rem"
    paginator
    :rows="5"
  >
    <Column field="User email" header="email"></Column>
    <Column field="Name" header="name"></Column>
    <Column field="Surname" header="surname"></Column>
    <Column field="Role" header="role">
      <template #body="slotProps">
        <Dropdown
          v-model="roleModel[slotProps.data.email]"
          :options="roles"
          optionLabel="roleName"
          optionValue="roleId"
          placeholder="Role"
          class="w-full md:w-14rem"
        />
        <Button @click="remove(slotProps.data.visitId)">Remove</Button>
        <Button @click="edit(slotProps.data.visitId)">Edit</Button>
      </template></Column
    >
  </DataTable>
</template>

<script>
import Button from "primevue/button";
import store from "../store/index";
import TabMenu from "primevue/tabmenu";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Dropdown from "primevue/dropdown";

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
      roleModel: [],
      users: [],
      roles: [],
    };
  },
  components: {
    Button,
    TabMenu,
    DataTable,
    Column,
    Dropdown,
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
  async created() {
    if (this.isAdmin()) {
      //need to add api
      this.roles = //...
        this.users = //...
          users.forEach((user) => {
            this.roleModel[user.email] = {
              name: user.roleId,
              roleName: user.roleName,
            };
          });
    }
  },
};
</script>

<style scoped></style>
