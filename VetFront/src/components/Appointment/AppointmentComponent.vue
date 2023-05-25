<template>
  <div style="text-align: center">
    <h2>Appointment data</h2>
    <div style="display: inline-grid">
      <Dropdown
        v-model="pet"
        :options="pets"
        optionLabel="petName"
        optionValue="petId"
        placeholder="Pet"
        :disabled="true"
      />
      <Dropdown
        v-model="doctor"
        :options="doctors"
        optionLabel="doctorName"
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
        <Button @click="updateAppointment">Update appointment</Button>
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
      today: new Date(),
    };
  },
  components: {
    Button,
    Dropdown,
    Calendar,
  },
  methods: {
    async updateAppointment() {
      if (this.dateTime && !this.isPetDocAndDateSelected) {
        await post(
          `appointment/update/${this.$route.params.visitId}/${
            this.doctor
          }/${this.format(this.date)}/${this.dateTime}`
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
        this.dateTimes = await get(
          `doctor/hours/${this.doctor}/${this.format(this.date)}`
        );
      }
    },
  },
  async created() {
    if (this.$route.params.visitId) {
      const hasAccess = await get(`has-access/${this.$route.params.visitId}`);
      const appointment = await get(
        `appointment/${this.$route.params.visitId}`
      );
      this.pet = appointment.petId;
      this.pets = appointment.pets;
      this.doctor = appointment.doctorId;
      this.doctors = appointment.doctors;
      this.dateTime = appointment.dateTime;
      console.log(this.dateTime);
      this.date = appointment.date;
      if (!hasAccess) {
        this.$router.push("/");
      }
    }
  },
};
</script>
