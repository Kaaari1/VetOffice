<template>
  <DataTable
    :value="pets"
    tableStyle="min-width: 50rem"
    paginator
    :rows="5"
  >
    <Column field="name" header="Animal Name"></Column>
    <Column field="animalType" header="Pet type"></Column>
    <Column field="birthDate" header="Date of birth"> </Column>
  </DataTable>
  <Button @click="addNewPet">Add new pet</Button>
</template>

<script>
import { get } from "../../services/http-service";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Button from "primevue/button";
import Calendar from "primevue/calendar";
export default {
  data() {
    return {
      pets: null,
      date: null,
      modelValue: [],
    };
  },
  methods: {
    addNewPet() {
      this.$router.push("/addNewPet");
    },
  },
  components: { Column, DataTable, Button, Calendar },
  async created() {
    this.pets = await get(`animals`);
    console.log(this.pets);
  },
};
</script>

<style scoped></style>
