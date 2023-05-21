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
    <InputText type="text" v-model="registerMail" placeholder="E-mail" />
    <InputText
      type="password"
      v-model="registerPassword"
      placeholder="Password"
    />
    <Button @click="register">Register</Button>
    <p style="color: red" v-if="registerError" class="error">
      {{ registerError }}
    </p>
  </div>
</template>

<script>
import store from "../../store/index";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import httpService from "../../services/http-service";

export default {
  data() {
    return {
      eMail: "",
      password: "",
      error: "",
      registerMail: "",
      registerPassword: "",
      registerError: "",
    };
  },
  components: {
    Button,
    InputText,
  },
  methods: {
    async login() {
      const token = await httpService.post(
        `login/${this.eMail}/${this.password}`
      );
      if (token.data) {
        await store.dispatch("login", token.data);
      }
      this.$router.push("/");
    },
    async register() {
      const token = await httpService.get(`test`);
      console.log(token);
    },
  },

  computed: {
    isAuthenticated() {
      return localStorage.authenticated;
    },
  },
};
</script>
