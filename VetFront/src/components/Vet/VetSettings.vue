<template>
  <div style="text-align: center">
    <DataTable
      :value="dayoffs"
      tableStyle="min-width: 50rem"
      paginator
      :rows="5"
    >
      <Column field="date" header="Dayoff date"></Column>
      <Column field="" header="">
        <template #body="slotProps">
          <Button @click="remove(slotProps.data.dayOffId)">Remove</Button>
        </template></Column
      >
    </DataTable>
    <div style="display: inline-grid; margin-top: 50px">
      <Calendar
        v-model="dayOffDate"
        placeholder="Choose date of dayoff"
        showIcon
        :minDate="today"
      ></Calendar>
      <div>
        <Button @click="addNewDayOff">Add new dayoff</Button>
      </div>
    </div>
  </div>
  <div style="text-align: center; margin-top: 150px">
    <div>
      <Calendar v-model="from" placeholder="from" timeOnly />
    </div>
    <div>
      <Calendar v-model="to" placeholder="to" timeOnly />
    </div>
    <Button @click="updateWorkingHours">Update working hours</Button>
  </div>
</template>

<script>
import Button from "primevue/button";
import { post, get } from "../../services/http-service";
import Calendar from "primevue/calendar";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import InputNumber from "primevue/inputnumber";

export default {
  data() {
    return {
      dayOffDate: null,
      dayoffs: [],
      to: null,
      from: null,
      selectedTime: null,
      blockedMinutes: {
        beforeShowDay: this.disableMinutes,
      },
      today: new Date(),
    };
  },
  components: {
    Calendar,
    Button,
    Column,
    DataTable,
    InputNumber,
  },
  methods: {
    async addNewDayOff() {
      const date = this.format(this.dayOffDate);
      await post(`doctor/dayoff/add/${date}`);
      this.dayoffs = await get(`doctor/dayoffs`);
    },

    async remove(event) {
      await post(`doctor/dayoff/remove/${event}`);
      this.dayoffs = await get(`doctor/dayoffs`);
    },

    async updateWorkingHours() {
      if (this.from < this.to) {
        await post(
          `updateWorkingHours/${this.formatTime(this.from)}/${this.formatTime(
            this.to
          )}`
        );
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
    },
    formatTime(date, timeSeparator = ":") {
      if (!date) return "";
      const hours = ("0" + date.getHours()).slice(-2);
      const minutes = ("0" + date.getMinutes()).slice(-2);

      return `${hours}${timeSeparator}${minutes}`;
    },
  },
  async created() {
    this.dayoffs = await get(`doctor/dayoffs`);
    const workingHours = await get(`doctor/workingHours`);
    this.to = workingHours.to;
    this.from = workingHours.from;
  },
};
</script>
