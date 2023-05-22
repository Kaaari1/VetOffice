<template>
  <DataTable
    v-if="isAdmin"
    :value="users"
    tableStyle="min-width: 50rem"
    paginator
    :rows="5"
  >
    <Column field="email" header="User email"></Column>
    <Column field="name" header="Name"></Column>
    <Column field="surname" header="Surname"></Column>
    <Column field="role" header="Role">
      <template #body="slotProps">
        <Dropdown
          v-model="roleValue[slotProps.data.email]"
          :options="roles"
          optionLabel="roleName"
          optionValue="roleId"
          placeholder="role"
          class="w-full md:w-14rem"
        />
        <Button
          @click="
            updateRole(
              roleValue[slotProps.data.email],
              roleUser[slotProps.data.email]
            )
          "
          >Update role</Button
        >
        <!-- <Button @click="remove(slotProps.data.visitId)">Remove</Button>
          <Button @click="edit(slotProps.data.visitId)">Edit</Button> -->
      </template></Column
    >
  </DataTable>
</template>

<script>
import Button from "primevue/button";
import store from "../store/index";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Dropdown from "primevue/dropdown";
import { get, post } from "../services/http-service";

export default {
  data() {
    return {
      roleUser: [],
      roleValue: [],
      users: [],
      roles: [],
    };
  },
  components: {
    Button,
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
    async updateRole(roleId, userId) {
      await post(`roles/${roleId}/${userId}`);
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
    if (this.isAdmin) {
      this.roles = await get("roles");
      this.users = await get("users");
      this.users.forEach((user) => {
        this.roleUser[user.email] = user.userId;
        this.roleValue[user.email] = user.roleId;
      });
    }
    const xd = get(`has-access/1`);
  },
};
</script>

<style scoped></style>
