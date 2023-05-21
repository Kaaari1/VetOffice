import { createStore } from "vuex";
import httpService from "../services/http-service";

const store = createStore({
  state: {
    isAuthenticated: false,
  },
  actions: {
    login(skip, data) {
      httpService.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${data.token}`;
      localStorage.setItem("role", data.role);
    },
    logout() {
      localStorage.setItem("role", null);
      httpService.defaults.headers.common["Authorization"] = null;
    },
  },
});

export default store;
