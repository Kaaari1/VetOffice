<template>
  <DataTable :value="data" tableStyle="min-width: 50rem" paginator :rows="5">
    <Column field="name" header="Animal Name"></Column>
    <Column field="doctor" header="Doctor"></Column>
    <Column field="date" header="Date"></Column>
    <Column field="quantity" header="">
      <template #body="slotProps">
        <Button @click="remove(slotProps.data.visitId)">Remove</Button>
        <Button @click="edit(slotProps.data.visitId)">Edit</Button>
      </template></Column
    >
  </DataTable>
</template>

<script>
import { get, post } from "../../services/http-service";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Button from "primevue/button";
export default {
  data() {
    return {
      data: null,
    };
  },
  components: { Column, DataTable, Button },
  methods: {
    remove(visitId) {
      console.log(visitId);
    },
    edit(visitId) {
      console.log(visitId);
    },
  },
  async created() {
    this.data = await get(`appointments/${localStorage.getItem("userId")}`);
  },
};
</script>

<style scoped></style>
