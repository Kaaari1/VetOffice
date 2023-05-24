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
          <Button @click="remove(slotProps.data.dayoffId)">Remove</Button>
        </template></Column
      >
    </DataTable>
    <div style="display: inline-grid; margin-top: 50px">
      <Calendar
        v-model="dayOffDate"
        placeholder="Choose date of dayoff"
        showIcon
      ></Calendar>
      <div>
        <Button @click="addNewDayOff">Add new dayoff</Button>
      </div>
    </div>
  </div>
  <div style="text-align: center; margin-top: 150px">
    <div>
      <InputNumber
        v-model="from"
        placeholder="from"
        showButtons
        min="0"
        max="24"
        :maxFractionDigits="0"
        :useGrouping="false"
      ></InputNumber>
    </div>
    <div>
      <InputNumber
        v-model="to"
        placeholder="to"
        showButtons
        min="0"
        max="24"
        :maxFractionDigits="0"
        :useGrouping="false"
      ></InputNumber>
    </div>
    <Button @click="updateWorkingHours">Update working hours</Button>
  </div>
</template>

<script>
import Button from "primevue/button";
import { post } from "../../services/http-service";
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
      await post(`addNewDayoff/${this.dayOffDate}`);
      this.dayoffs = await get(`dayoffs`);
    },
    async updateWorkingHours() {
      if (this.from < this.to) {
        await post(`updateWorkingHours/${this.from}/${this.to}`);
      }
    },
  },
  async created() {
    this.dayoffs = await get(`dayoffs`);
  },
};
</script>
