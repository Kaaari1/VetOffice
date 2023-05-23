<template>
  <div style="text-align: center">
    <h2>Appointment data</h2>
    <div style="display: inline-grid">
      <Dropdown
        v-model="pet"
        :options="pets"
        optionLabel="name"
        optionValue="petId"
        placeholder="Pet"
      />
      <Dropdown
        v-model="doctor"
        :options="doctors"
        optionLabel="name"
        optionValue="doctorId"
        placeholder="Doctor"
      />
      <Calendar v-model="date" showButtonBar placeholder="Date" />
      <Dropdown
        v-model="dateTime"
        :options="dateTimes"
        optionLabel="time"
        optionValue="time"
        placeholder="Time"
        :disabled="isPetDocAndDateSelected"
      />
      <div>
        <Button @click="addNewAppointment">Add new appointment</Button>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "primevue/button";
import { get, post } from "../../services/http-service";
import Dropdown from "primevue/dropdown";
import Calendar from "primevue/calendar";

export default {
  data() {
    return {
      pets: [],
      doctors: [],
      dateTimes: [],
      pet: null,
      doctor: null,
      dateTime: null,
      date: null,
    };
  },
  components: {
    Button,
    Dropdown,
    Calendar,
  },
  methods: {
    async addNewAppointment() {
      await post(
        `addNewAppointment/${this.pet}/${this.doctor}/${this.dateTime}/${this.date}`
      );
      this.$router.push("/yourAppointments");
    },
  },
  async created() {
    this.pets = await get(`pets`);
    this.doctors = await get(`doctors`);
  },
  computed: {
    isPetDocAndDateSelected() {
      return this.pet === null || this.doctor === null || this.date === null;
    },
  },
  watch: {
    // whenever question changes, this function will run
    doctor() {
      this.dateTime = null;
    },
    async isPetDocAndDateSelected(newValue) {
      if (newValue) {
        this.dateTimes = await get(`doctor/${doctor}`);
      }
    },
  },
};
</script>
