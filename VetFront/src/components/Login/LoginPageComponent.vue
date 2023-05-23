<template>
  <div style="text-align: center">
    <h2>Login</h2>
    <InputText type="text" v-model="eMail" placeholder="E-mail" />
    <InputText type="password" v-model="password" placeholder="Password" />
    <Button @click="login">Login</Button>
    <p style="color: red" v-if="error" class="error">{{ error }}</p>
  </div>
  <div style="text-align: center">
    <h2>Register</h2>
    <div style="display: inline-grid">
      <InputText type="email" v-model="registerMail" placeholder="E-mail" />
      <InputText
        type="password"
        v-model="registerPassword"
        placeholder="Password"
      />
      <InputText type="text" v-model="registerName" placeholder="Name" />
      <InputText type="text" v-model="registerSurname" placeholder="Surname" />
      <InputNumber
        v-model="registerPhone"
        placeholder="Phone number"
        :min="100000000"
        :max="999999999"
      />
      <div>
        <Button @click="register">Register</Button>
      </div>
    </div>
  </div>
</template>

<script>
import store from "../../store/index";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import { get, post } from "../../services/http-service";

export default {
  data() {
    return {
      eMail: "",
      password: "",
      error: "",
      registerMail: "",
      registerPassword: "",
      registerPhone: null,
      registerName: "",
      registerSurname: "",
    };
  },
  components: {
    Button,
    InputText,
    InputNumber,
  },
  methods: {
    async login() {
      const token = await post(`login/${this.eMail}/${this.password}`);
      if (token.token) {
        await store.dispatch("login", token);
        this.$router.push("/");
      }
    },
    async register() {
      const token = await post(`register/${this.eMail}/${this.password}`);
      if (token.token) {
        await store.dispatch("login", token);
        this.$router.push("/");
      }
    },
  },
};
</script>
