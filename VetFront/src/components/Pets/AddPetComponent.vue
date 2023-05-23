<template>
  <div style="text-align: center">
    <h2>Pet data</h2>
    <div style="display: inline-grid">
      <InputText type="text" v-model="petName" placeholder="Name" />
      <InputText
        type="date"
        v-model="dateOfBirth"
        placeholder="Date of birth"
      />
      <InputText type="text" v-model="petType" placeholder="Type" />
      <div>
        <Button @click="addNewPet">Add new pet</Button>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import { post } from "../../services/http-service";

export default {
  data() {
    return {
      petName: "",
      dateOfBirth: "",
      petType: "",
    };
  },
  components: {
    Button,
    InputText,
    InputNumber,
  },
  methods: {
    async addNewPet() {
      await post(
        `addNewPet/${this.petName}/${this.petType}/${this.dateOfBirth}`
      );
      this.$router.push("/yourPets");
    },
  },
};
</script>
