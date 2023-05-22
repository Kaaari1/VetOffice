<template>
  <span>to do</span>
</template>

<script>
import { get } from "../../services/http-service";

export default {
  data() {
    return {
      visitId: null,
    };
  },
  components: {},
  methods: {},
  async created() {
    if (this.$route.params.visitId) {
      const hasAccess = await get(`has-access/${this.$route.params.visitId}`);
      if (!hasAccess) {
        this.$router.push("/");
      }
    }
  },
};
</script>

<style scoped></style>
