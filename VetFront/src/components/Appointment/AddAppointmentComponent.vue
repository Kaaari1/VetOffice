<template>
  <HeaderComponent />
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
      <Calendar
        v-model="date"
        showButtonBar
        placeholder="Date"
        :minDate="today"
      />
      <Dropdown
        v-model="dateTime"
        :options="dateTimes"
        :placeholder="dateTime"
        :disabled="isPetDocAndDateSelected"
      />
      <div>
        <Button @click="addAppointment">Add appointment</Button>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "primevue/button";
import { get, post } from "../../services/http-service";
import Dropdown from "primevue/dropdown";
import Calendar from "primevue/calendar";
import HeaderComponent from "./components/HeaderComponent.vue";

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
      today: new Date(),
    };
  },
  components: {
    Button,
    Dropdown,
    Calendar,
    HeaderComponent,
  },
  methods: {
    async addAppointment() {
      if (this.dateTime && !this.isPetDocAndDateSelected) {
        await post(
          `addAppointment/${this.pet}/${this.doctor}/${this.format(
            this.date
          )}/${this.dateTime}`
        );
        this.$router.push("/yourAppointments");
      }
    },
    format(
      date,
      includeTime,
      dateSeparator = "-",
      timeSeparator = ":",
      dateTimeSeparator = " ",
      reversed = true,
      getMonthName = false
    ) {
      if (!date) return "";
      try {
        const day = ("0" + date.getDate()).slice(-2);
        const month = getMonthName
          ? I18n.t(monthNames[date.getMonth()])
          : ("0" + (date.getMonth() + 1)).slice(-2);
        const year = date.getFullYear();
        let formattedDate;
        if (reversed) {
          formattedDate = `${year}${dateSeparator}${month}${dateSeparator}${day}`;
        } else {
          formattedDate = `${day}${dateSeparator}${month}${dateSeparator}${year}`;
        }

        if (includeTime) {
          formattedDate += dateTimeSeparator + formatTime(date, timeSeparator);
        }
        return formattedDate;
      } catch {
        return date;
      }
    },
    formatTime(date, timeSeparator = ":") {
      if (!date) return "";
      const hours = ("0" + date.getHours()).slice(-2);
      const minutes = ("0" + date.getMinutes()).slice(-2);

      return `${hours}${timeSeparator}${minutes}`;
    },
  },
  computed: {
    isPetDocAndDateSelected() {
      return this.pet === null || this.doctor === null || this.date === null;
    },
  },
  watch: {
    async doctor() {
      this.dateTime = null;
      if (!this.isPetDocAndDateSelected) {
        this.dateTimes = await get(
          `doctor/${this.doctor}/${this.format(this.date)}`
        );
      }
    },
    async isPetDocAndDateSelected(newValue) {
      if (!newValue) {
        console.log(this.doctor);
        this.dateTimes = await get(
          `doctor/hours/${this.doctor}/${this.format(this.date)}`
        );
      }
    },
  },
  async created() {
    this.pets = await get(`animals`);
    this.doctors = await get(`doctors`);
  },
};
</script>
