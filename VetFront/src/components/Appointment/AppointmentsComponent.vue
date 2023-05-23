<template>
  <DataTable
    :value="appointments"
    tableStyle="min-width: 50rem"
    paginator
    :rows="5"
  >
    <Column field="name" header="Animal Name"></Column>
    <Column field="doctor" header="Doctor"></Column>
    <Column field="date" header="Date"> </Column>
    <Column field="quantity" header="">
      <template #body="slotProps">
        <Button @click="remove(slotProps.data.visitId)">Remove</Button>
        <Button @click="edit(slotProps.data.visitId)">Update</Button>
      </template></Column
    >
  </DataTable>
  <div>
    <Button @click="addNewAppointment">Add new appointment</Button>
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
  async created() {
    this.appointments = await get(
      `appointments/${localStorage.getItem("userId")}`
    );
    this.appointments.forEach((appointment) => {
      this.modelValue[appointment.visitId] = appointment.date;
    });
  },
};
</script>

<style scoped></style>
