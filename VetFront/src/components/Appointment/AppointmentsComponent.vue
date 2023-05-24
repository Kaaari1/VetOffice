<template>
  <DataTable
    :value="appointments"
    tableStyle="min-width: 50rem"
    paginator
    :rows="5"
  >
    <Column field="animalName" header="Animal Name"></Column>
    <Column v-if="isVet" field="name" header="Owner name"></Column>
    <Column v-if="isUser" field="doctor" header="Doctor"></Column>
    <Column field="date" header="Date"> </Column>
    <Column field="quantity" header="">
      <template #body="slotProps">
        <Button @click="remove(slotProps.data.visitId)">Remove</Button>
        <Button @click="edit(slotProps.data.visitId)">Update</Button>
      </template></Column
    >
  </DataTable>
  <div>
    <Button v-if="isUser" @click="addNewAppointment">Add new appointment</Button>
  </div>
</template>

<script>
import { get, post } from "../../services/http-service";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Button from "primevue/button";
import Calendar from "primevue/calendar";
export default {
  data() {
    return {
      appointments: null,
      date: null,
      modelValue: [],
    };
  },
  components: { Column, DataTable, Button, Calendar },
  methods: {
    remove(visitId) {
      post(`appointments/remove/${visitId}`);
    },
    edit(visitId) {
      this.$router.push({
        name: "appointment",
        params: { visitId: visitId },
      });
    },
    addNewAppointment() {
      this.$router.push("/addNewAppointment");
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
    if (localStorage.getItem("role") === "Vet") {
      this.appointments = await get(`appointments/vet`);
    } else {
      this.appointments = await get(`appointments`);
    }
    this.appointments.forEach((appointment) => {
      this.modelValue[appointment.visitId] = appointment.date;
    });
  },
};
</script>

<style scoped></style>
